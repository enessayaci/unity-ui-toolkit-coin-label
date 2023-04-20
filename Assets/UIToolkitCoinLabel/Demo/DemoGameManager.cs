using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameManager : MonoBehaviour
{
    public static event Action<int, int> OnCoinUpdated = delegate { };

    #region GLOBAL VARIABLES

    public static int coin = 100;

    #endregion GLOBAL VARIABLES


    public static int GetCoin()
    {
        return coin;
    }

    public static void AddCoin(int amount)
    {
        int oldCoin = coin;
        coin += amount;
        OnCoinUpdated(oldCoin, coin);
    }

    public static void SubtractCoin(int amount)
    {
        int oldCoin = coin;
        coin -= amount;
        OnCoinUpdated(oldCoin, coin);
    }
}
