using UnityEngine;
using System.Collections.Generic;
using System;

public class InteractionCommandRouter
{
    private Dictionary<InteractMode, IInteractCommand> commandMap = new();
    //犯错后做个一对一预防工作。
    private Dictionary<Type, InteractMode> typeToModeMap = new();

    public void Register(InteractMode mode, IInteractCommand command)
    { 
        Type type = command.GetType();

        if (typeToModeMap.TryGetValue(type, out var existingMode))
        {
            Debug.LogWarning($"command {type.Name} is register to {existingMode}, can't be register to {mode} again");
            return;
        }

        if (commandMap.ContainsKey(mode))
        {
            Debug.LogWarning($"{mode} is alreadly registered, register is ignored");
            return;
        }

        commandMap[mode] = command;
        typeToModeMap[type] = mode;
        
    }

    public void ExcuteCommand(InteractMode mode, IInteractable interactable)
    {
        if (commandMap.TryGetValue(mode, out var value))
        {
            value.Excute(interactable);
        }
        else
        {
            Debug.LogWarning($"未注册的交互命令类型：{mode}");
        }
 
    }
}
