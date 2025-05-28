using UnityEngine;

public interface IInteractable
{
    //默认就是public
    

    //按下交互后的行为
    void Interact(); //执行逻辑：打开对话框？拾取？

    string GetInteractPrompt(); //文字打开箱子 拾取卷轴，而不是清一色交互
    bool CanInteract();
}
