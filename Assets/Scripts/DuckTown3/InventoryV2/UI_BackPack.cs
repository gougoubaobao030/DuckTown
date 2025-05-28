using UnityEngine;

public class UI_BackPack : MonoBehaviour
{
    public void BackPackClicked()
    {
        GameManager.Instance.UIManager.ToggleInventroyPanel();
    }
}
