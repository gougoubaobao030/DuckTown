using UnityEngine;

namespace DuckTown3.UI
{
    public interface IUIProgressBar
    {
        void Show();
        void Hide();
        void UpdateProgress(float percent);
    }
}
