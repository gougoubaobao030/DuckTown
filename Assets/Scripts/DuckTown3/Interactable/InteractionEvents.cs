using System;
using UnityEngine;

public static class InteractionEvents
{ 
    public static event Action OnInteractionStarted;
    public static event Action OnInteractionEnded;

    public static void TriggerInteractionStarted()
    { 
        OnInteractionStarted?.Invoke();
    }

    public static void TriggerInteractionEnded()
    {
        OnInteractionEnded?.Invoke();
    }
}

