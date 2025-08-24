using UnityEngine;

namespace DuckTown3.TownInput
{
    public interface ISkillInputProvider
    {
        bool IsSkillKeyPressed(int index);
        bool IsConfirmKeyPressed();
        bool IsCancelKeyPressed();
        bool TryGetGroundPosition(float maxDistance, LayerMask layerMask, out UnityEngine.Vector3 hitPointer);
    }

}
