using System;
using UnityEngine;

public interface IGoldSystem
{
    event Action<int> OnGoldAmoutChanged;
    int Gold { get; }

    void AddGold(int amount);
    void SpendGold(int amount);
}
