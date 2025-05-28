using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DevToolBox : MonoBehaviour
{
    private static DevToolBox instance;
    public static DevToolBox Instance => instance;

    [SerializeField] private List<MonoBehaviour> moduleSources;
    private List<IDevToolModule> modules = new List<IDevToolModule>();

    private void Awake()
    {
        if (instance != null && instance != this)
        { 
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        AutoCollectModules();
    }

    private void Update()
    {
        //统一监听，调试单例模块
    }

    private void AutoCollectModules()
    { 
        modules = moduleSources.OfType<IDevToolModule>().ToList();

        foreach (var module in moduleSources)
        {
            if (module is not IDevToolModule)
            {
                Debug.LogWarning($"[DevToolbox] {module.name} 没有实现 IDevToolModule 接口，已忽略");
            }
        }
    }

    private void OnGUI()
    {
        foreach (var module in modules)
        { 
            module.DrawGUI();
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var module in modules)
        { 
            module.DrawGizmos();
        }
    }
}
