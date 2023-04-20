# unity-ui-toolkit-coin-label
A coin label component completely uı toolkit based, it has animation when add and subtract coins 🤑

<img src="https://raw.githubusercontent.com/enessayaci/unity-ui-toolkit-coin-label/main/Assets/UIToolkitCoinLabel/Readme/presentation.gif" width="300">

# Usage

<ol>
    <li>Open the scene, create a new UI Document object by Right Click > UI Toolkit > UI Document </li>
    <li>Select Panel Settings and Source Asset (uxml file) of UIDocument component on the inspector (Demo Panel Settings and Demo.uxml for my case)</li>
    <li>Attach CoinLabelController.cs to the UI Document object. This script will create 10(default of mine) coin sprite and place in VisualElement#CoinArea(a visual element named as CoinLabel in Demo.uxml in my case).</li>
    <li>Open Demo.uss, copy all its content and paste somewhere of your uss file.</li>
    <li>Open DemoGameManager.cs and copy the methods(AddCoin,SubtractCoin), copy the variables and events. In brief, copy all its content, paste them to your own GameManager script(or equivalent script of yours). </li>
    <li>Call DemoGameManager.AddCoin(5) where you want to gain 5 coins. DemoGameManager.SubtractCoin(5) to remove 5 coins</li>
    
</ol>

