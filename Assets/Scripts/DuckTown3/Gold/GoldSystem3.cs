using System;
using UnityEngine;

public class GoldSystem3 : IGoldSystem
{
    private int goldAmout = 20000;
    //这里的static有什么意义呢，没有意义啊亲
    public event Action<int> OnGoldAmoutChanged;

    //反正这就是给goldAmout写了一个getter
    public int Gold => goldAmout;

    public void AddGold(int amount)
    {
        goldAmout += amount;
        OnGoldAmoutChanged?.Invoke(goldAmout);
    }

    public void SpendGold(int amount)
    { 
        goldAmout -= amount;
        OnGoldAmoutChanged?.Invoke(goldAmout);
    }
}
