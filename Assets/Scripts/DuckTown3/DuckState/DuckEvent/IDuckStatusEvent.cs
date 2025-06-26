using UnityEngine;

public interface IDuckStatusEvent
{
    //并不是说我们一定要用事件实现
    //为了可以mock，那是不是应该写个mock的东西吧

    void OnDodgeStart();
    void OnDodgeEnd();
}
