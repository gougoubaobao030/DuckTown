using UnityEngine;

public class PoolRelease : MonoBehaviour
{
    private System.Action OnEffectRelease;

    public void InjectCallBack(System.Action callback)
    { 
        OnEffectRelease = callback;
    }

    public void PlayAndReleaseAfter(float duration)
    {
        //鸭鸭可以不断的疯狂冲刺
        CancelInvoke();
        //call delegate
        Invoke(nameof(OnEffectReleaseWrapper), duration);
    }

    private void OnEffectReleaseWrapper()
    { 
        OnEffectRelease?.Invoke();
    }

    //鸭鸭万一凉了，也就别执行了，提早回池子吧
    private void OnDisable()
    {
        CancelInvoke();
    }
}
