### 模块设计思路

**所有可交互物品需要放在Interaction Layer**

- 模块控制器 
    - 负责检测是否有键盘输入以及目标
        - [计划]输入可能需要转入InputManger 
        - 目标检测 
           - 基于Physics.OverlapSphereNonAlloc
           - 显著减少CG压力
           - 委托DuckDefaultInput监听 
    - 负责向其他模块传递开始Event
    - 负责gizmo检测（[计划]待转移到DevTool）
    - [计划]可能需要增加：状态判断

- 交互事件event
   - OnInteractionStart
   - OnInteractionEnd
   - [计划]后续可以实现双轨制 OnPickupSuccess and OnNpcTalkStart so on
  
- 交互interface
  - 所有可交互物品通用函数

- 拾取物品，npc，宝箱具体脚本
  - [计划]npc 预定接入对话系统 

### 用到的其他脚本
**外部脚本 Interface**
- IReceiver 
    - 用于和背包物品接口回调
    - 拾取后放入背包

**其他模块**
- UI/Interaction/交互Ui
  - [计划] 计划对接状态机锁定UI


### 功能特点
- UI提示
- 最近物体的筛选与高亮（并没有）
- 统一的Interface接口调用
- 支持可交互物品的扩展
- 支持接口回调逻辑
- 状态缓存减少UI频繁开关
- 使用OverlapSphereNonAlloc优化性能