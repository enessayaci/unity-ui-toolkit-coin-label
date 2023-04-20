using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoTest : MonoBehaviour
{
    VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        AttachButtonsEvents();
    }

    private void AttachButtonsEvents()
    {
        Button ButtonAdd3 = root.Q<Button>("ButtonAdd3");
        ButtonAdd3.clicked += () =>
        {
            DemoGameManager.AddCoin(3);
        };

        Button ButtonAdd15 = root.Q<Button>("ButtonAdd15");
        ButtonAdd15.clicked += () =>
        {
            DemoGameManager.AddCoin(15);
        };

        Button ButtonAdd200 = root.Q<Button>("ButtonAdd200");
        ButtonAdd200.clicked += () =>
        {
            DemoGameManager.AddCoin(200);
        };

        Button ButtonSub50 = root.Q<Button>("ButtonSub50");
        ButtonSub50.clicked += () =>
        {
            DemoGameManager.SubtractCoin(50);
        };

        Button ButtonHard = root.Q<Button>("ButtonHard");
        ButtonHard.clicked += () =>
        {
            DemoGameManager.AddCoin(5);
            DemoGameManager.SubtractCoin(10);
            DemoGameManager.AddCoin(50);
            DemoGameManager.SubtractCoin(30);
        };
    }

}
