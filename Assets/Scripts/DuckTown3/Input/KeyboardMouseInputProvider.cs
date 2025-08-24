using DuckTown3.SkillSystemV2;
using UnityEngine;

namespace DuckTown3.TownInput
{
    public class KeyBoardMouseInputProvider : ISkillInputProvider
    {
        private static readonly KeyCode[] skillMainKeyCodes =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
        };

        private static readonly KeyCode[] skillKeyPadCodes =
        {
            KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5,
            KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.Keypad0
        };

        public bool IsSkillKeyPressed(int index)
        {
            if(index < 0 || index >= 10) return false;

            bool mainKey = Input.GetKeyDown(skillMainKeyCodes[index]);
            bool numpadKey = Input.GetKeyDown(skillKeyPadCodes[index]);

            return mainKey || numpadKey;
        }
        public bool IsConfirmKeyPressed()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool IsCancelKeyPressed()
        {
            return Input.GetMouseButtonDown(1);
        }

        public bool TryGetGroundPosition(float maxDistance, LayerMask layerMask, out Vector3 hitPointer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
            { 
                hitPointer = hit.point;
                return true;
            }

            hitPointer = Vector3.zero;
            return false;
        }
    }
}
