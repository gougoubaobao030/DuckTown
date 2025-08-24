using System;
using UnityEngine;

public class DuckStatusEvents : IDuckStatusEvent
{
    public event Action OnDodgeStarted;
    public event Action OnDodgeEnded;

    public void OnDodgeStart()
    {
        OnDodgeStarted?.Invoke();
    }

    public void OnDodgeEnd()
    {
        OnDodgeEnded?.Invoke();
    }

}
