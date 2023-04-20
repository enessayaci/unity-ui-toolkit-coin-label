using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinLabelController : MonoBehaviour
{
    private VisualElement CoinArea; // parent of coin uı system, everything is inside this element
    private Label CoinText; // coin text reference, this is the element coin amount will shown
    private CancellationTokenSource cancellationTokenSource; // to avoid conflict when consecutive coin update requests, we will use it to reference currently running coin updater task so that we can stop previous task though it is not completed yet, it doesnt matter because newest request is what we really need
    private Stack<VisualElement> CoinSpriteStack = new Stack<VisualElement>(); // this is to keep created visual elements of coin sprites, we will use it again and again so that we dont re create and render elements again, you can think of as a simple pooling

    private void OnEnable()
    {
        // Find CoinArea
        CoinArea = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("CoinArea");

        // Find Coin Text and assign it
        CoinText = CoinArea.Q<Label>(name: "text");

        CoinText.text = DemoGameManager.coin.ToString();


        // at beginning of the game, we create 10 new coin-sprite which will be animate toward to coin label, 10 is a default value that I selected.
        // if you change 10 to another value, that amount of sprite will be created to be animated.
        // I suggest 10, 10 is ideal :)

        for (int i = 0; i < 10; i++)
        {
            // create visual element and assign it to "coin-sprite" class
            VisualElement newCointSprite = new VisualElement();
            newCointSprite.AddToClassList("coin-sprite");
            // they must have no delay initially
            newCointSprite.style.transitionDelay = new List<TimeValue> { 0f, 0f };

            // we also want them invisible default, we will make them visible and animate as we need
            newCointSprite.style.display = DisplayStyle.None;

            // add them to parent element we created
            CoinArea.Insert(0, newCointSprite);

            // add them all to a stack to use later, we will always use same 10 VisualElement
            CoinSpriteStack.Push(newCointSprite);

        }

        DemoGameManager.OnCoinUpdated += OnCoinUpdated;
    }

    public async void OnCoinUpdated(int oldCoin, int newCoin)
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }

        cancellationTokenSource = new CancellationTokenSource();

        int spriteCount = (newCoin - oldCoin > 0) ? newCoin - oldCoin : 0;
        float coinSpriteDelay = 0;
        Mathf.Clamp(spriteCount, 0, 10);
        for (int i = 0; i < spriteCount; i++)
        {
            try
            {
                VisualElement sprite = CoinSpriteStack.Pop();
                GenerateCoinSprite(sprite, coinSpriteDelay);
                coinSpriteDelay += 0.1f;
            }
            catch
            {
                break;
            }
        }

        await OnCoinUpdatedCoroutine(oldCoin, newCoin, cancellationTokenSource.Token);
    }

    private async Task OnCoinUpdatedCoroutine(int oldCoin, int newCoin, CancellationToken cancellationToken)
    {
        try
        {
            CoinText.text = oldCoin.ToString();

            int countLimit = newCoin;

            if (Mathf.Abs(newCoin - oldCoin) > 100)
            {
                countLimit = oldCoin + 100;
            }

            if (newCoin > oldCoin)
            {
                while (oldCoin < countLimit)
                {
                    await Task.Delay(20, cancellationToken);
                    oldCoin += 1;
                    CoinText.text = oldCoin.ToString();
                }
            }
            else
            {
                while (oldCoin > countLimit)
                {
                    await Task.Delay(20, cancellationToken);
                    oldCoin -= 1;
                    CoinText.text = oldCoin.ToString();
                }
            }

            CoinText.text = newCoin.ToString();
        }
        catch { }
    }

    private async Task GenerateCoinSprite(VisualElement sprite, float delay)
    {
        sprite.style.display = DisplayStyle.Flex;
        sprite.RemoveFromClassList("animate");

        sprite.style.transitionDelay = new List<TimeValue> { delay, delay };

        await Task.Yield();

        sprite.AddToClassList("animate");

        await Task.Delay((int)((delay + 0.35f) * 1000));

        sprite.RemoveFromClassList("animate");
        sprite.style.display = DisplayStyle.None;
        sprite.style.transitionDelay = new List<TimeValue> { 0, 0 };

        CoinSpriteStack.Push(sprite);
    }

}
