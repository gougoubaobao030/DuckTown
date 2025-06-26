# DevNotes.md

## [2025-06-21] 技能冷却UI不同步问题

### 问题现象：
- 火球术释放后，技能UI的冷却圈没动，但技能实际进入了CD状态。

### 调查过程：
- 检查了SkillManager -> SkillCooldownHandler，逻辑正常
- UIController中事件订阅时机晚于广播，导致没接收到冷却开始事件

### 解决方案：
- 调整UI初始化顺序，让其订阅事件早于技能系统加载
- 使用EventBus缓存事件，保证UI晚来也能收到（参考Unity Addressables事件模型）

### 教训：
- 模块加载顺序很重要，事件系统必须支持懒订阅/缓存逻辑
