### 2025年11月4日
- 开始搞对话系统
- 想必sodata
- manager
- ui的
- mvc是必不可少的

智障给的架构图
/DuckTown3
 ┣ /Dialogue
 ┃ ┣ DialogueData.cs          ← Model（配置层）
 ┃ ┣ DialogueLine.cs
 ┃ ┣ DialogueManager.cs       ← Controller（流程层）
 ┃ ┣ DialogueTrigger.cs       ← 输入触发层
 ┃ ┣ DialogueConditionChecker.cs
 ┃ ┣ DialogueEventHub.cs      ← 事件总线
 ┃ ┗ UI_DialoguePanel.cs      ← View（UI层）
看着图个乐

***大方向问题***
困于对话数据建模
明天问下deepseek有什么高见

### 2025年11月5日
总之放了只蓝鸭鸭当npc，最难的一步应该已经做好了

太大了
这次就是要试着从最小步骤开始
就是对话数据
然后不同npc显示不同内容
从console输出开始，也不管特喵的ui了

### 2025年11月7日
决定重新写一个UI脚本控制对话
下面是智障回答的为什么是事件响应 而不用UI注入

### 2025年11月10日
重新写
写好了事件？
要适当的invoke事件

done: 去统计一下之前那个老的instance都用在哪些地方了
用了两个地方
done: 1. 开始交互的时候showpanel
done: 2. 离开一定距离的时候closePanel 这个先不管
 - 改成用manager调度
done: 找好弄好就可以把新的挂上去跑起来试试了。
done: 不对，还有关闭对话按钮.... 这个要先弄 代码已经写了
done: 记得要去清空button按钮上的OnClick
todo: 对话end还没有写。

todo: 注意现在写的都是只能显示一行的, 接下来要改成多行

### 2025年11月20日
**需要解决问题**
awake  与 enable...
**学习到**
不同gameobj awake 和enable的执行时间是不同的
**解决方法**
todo: 待定
todo: 目前准备尝试的方法
保持enable disable不变
单例使用懒加载，解决顺序问题

### 2025年11月21日
**问题**
单例懒加载后，enable，disable注册开关本身这个似乎行不通
脚本似乎不能直接控制view，上面要个逻辑层似乎更好
因为监听器被关掉了....
**解决方法**
ui分离逻辑和显示

done: 要弄成中文显示
大多数游戏都是动态字体啊...选择动态字体，干脆省事。
done: 找到存放字体文件夹
    unity内部搜索文件夹找到了

### 2025年11月24日
***想法***
分成两种任务，对话结束时给任务
强制任务。一定要接。

todo: 现在开始准备完成多行显示：
**思路**
dialogueMgr控制index 给UI调用
UI按下next的时候，调用这个

***突然自己想到***
为什么不写一个动态的data，
也就是DialogueDataInstance 然后通过manager传给UI...
- 是不是坑还不好说

done: 1.动态Instance
2. 在manager生成Instance。
3. UI订阅Event
4. done: 做一个next按钮
***小问题***
 - 为什么点下之后高亮就没有了呢
***解决***
因为unity设定按下有选择，有导航系统
但我们通常不用导航系统，所以把导航改成none。
或者可以用代码强行取消选中状态。 
5. done: 把next图标在最后改成√
问题： 怎么加载资源
回答： 【serizilfield】private sprite endSprite
6. done:检查对话end时候的处理... 交互按钮在交互时还在出现
   - ***解决***因为有多种关闭机制
   - 关闭面板的时候，远离时又关了一次，导致很多奇怪问题 原来代码外面套了个parent。
   - 把gameobj.selfactive 判定 改成实际控制的dialoguePanelRoot.activeSelf。
7. todo: 个别字体没有显示   --  不管了
8. todo: 在对话结束时再给任务
9. todo: 任务不同状态时，对话内容不同
进入一个很复杂的状况：
 - todo: 那任务状态谁来改呢 比如先不管了 要不先不管了
 - 先让任务状态不同，对话不同这件事先实现

那个npc 对 dialogue 对 任务的映射表会出什么问题吗，写了其他数据里是不是不用写了
1.  异常遥远的todo: 任务中心下的规则表

### 2025年11月29日
所以我们先把要说的对话写出来吧
11. 似乎要开始改造任务管理器和任务数据了

### 2025年12月2日
大框架想了一亿年后，觉得自己想太多了。
- 先把对话结束invoke的功能写出来
- 发现只要把已经有的改一下位置...
done： 不同状态的不同对话也先挂上去吧...
别管那么多。

开发内容： 增加对话类型标记

done: bug 任务栏刷新两个, 提交没有消失 因为另一个npc也挂了一个...
  - 暂时把另一个npc的任务拿掉了
  - done: 另一个npc只是对话呢 
    - done: 做个默认对话吧 
    - done: 临时写了个没有任务就返回默认对话的内容
done：还有个临时的奖励系统
done: bug submmit没有改状态 -- 暂时用对话结束发事件顶着
-- 把任务信号把发送任务消息移动到了questManger内部

todo: 找个蘑菇图标
