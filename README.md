# 🦆 DuckTown Adventure

---

## English

**A 3D RPG game demo**, currently in development.  
The goal is to grow this into a mid-sized, complete game project with scalable systems, modular architecture, and smooth gameplay.

- Unity Version: `6000.0.36f1`
- Scene Entry: `Assets/Scenes/DuckTown3.unity`
- Status: In Progress 🚧
- Target: A modular action RPG with an interactive world

---

## 中文（Chinese）

**一个正在开发中的 3D RPG 游戏 Demo**。  
目标是逐步打造一个完整的中型项目，注重系统架构、可扩展性与流畅的游戏体验。

- Unity 版本：`6000.0.36f1`
- 主场景入口：`Assets/Scenes/DuckTown3.unity`
- 当前状态：开发中 🚧
- 项目目标：构建一个模块化的动作 RPG 世界


## 日本語（Japanese）

**開発中の 3D RPG ゲームデモです。**  
目標は、中規模の本格的なゲームプロジェクトとして完成させること。  
システムの拡張性とアーキテクチャの優雅さを重視しています。

- Unity バージョン：`6000.0.36f1`
- シーン入口：`Assets/Scenes/DuckTown3.unity`
- 状況：開発中 🚧
- 目標：モジュール構造のアクションRPGの構築

---

# 🐤 项目特点


项目以“在复现经典rpg基本功能的基础上，以真实可用的系统架构”为目标，涵盖了技能系统、状态机、商店系统、背包系统、金币系统等多个模块。
适合展示基本需求制作能力，系统架构能力与完整开发流程能力。
以及作者对游戏制作的热情和真心

---

## 🎯 Why This Project | 为什么做这个项目

希望独立实现一个中型项目，亲手构建面向未来可扩展的游戏核心架构。

不同于纯展示型的教学 Demo，本项目在系统划分、模块通信、数据结构设计方面进行了大量实践，目标是达到“能体验公司实战项目难度、八成规范”。同时，尝试站在未来团队协作与功能拓展的角度思考，设计合理的系统边界与职责。



---

## 🎮 Features | 核心功能特色（基本功能与技术）

- 🧠 **状态机控制系统**
  - 支持 Idle / Move / Jump / Fall / Dash / Dodge 状态
  - 状态切换基于输入与条件检测，结构清晰、拓展方便
  - 采用工厂模式生产状态，策略模式控制空中策略地面策略

- 🔥 **技能系统（数据驱动）**
  - SkillData 与 SkillInstance 分离，实现配置与行为解耦
  - 支持范围技、蓄力技、目标检测、冷却时间、预留动画事件联动接口
  - 灵活采用多种碰撞检测方式

- 🧺 **背包系统**
  - 包含物品栏、装备栏、仓库系统
  - 支持物品拖拽、分类显示、动态同步 UI
  - 实装ui对象池，减少GC消耗

- 🛒 **商店系统**
  - 商品配置支持 ScriptableObject 数据驱动
  - 与金币系统、背包系统事件通信解耦

- 💰 **金币系统**
  - 所有系统可通过事件消费/获取金币
  - 与 UI 实时同步

- 🧩 **模块通信架构**
  - 使用事件驱动（EventSystem）通信，模块独立、解耦

- **特效对象池系统**
  - 统一管理角色状态特效，技能特效（开发中）临时对象的生命周期
  - 避免频繁 `new` / `destroy`，显著降低 GC 压力，提高帧率稳定性

- **Debug Box**
  -模仿专业开发制作dev tool
  -用于绘制gizmo，ui面板显示数据，大幅度减轻调试压力
---

## 🧱 System Architecture | 系统架构图

- 本项目遵循模块化架构设计，系统间通信尽量使用事件广播 / 接口隔离，避免直接依赖。
- 所有大小模块采用依赖注入，观察者模式解耦
- 灵活运用策略模式，工厂模式实现系统的可维护性和可扩展性
- 所有开发遵循solid原则

如下图所示：

> 📌 **（架构图预留）**
>
> 路径：`Docs/architecture.png`
>
> 插图命令：
> ```markdown
> ![系统架构图](./Docs/architecture.png)
> ```

---


## 📦 Project Structure

```plaintext
MyUnityProject/
├── Assets/
│   ├── Scripts/         # 所有 C# 脚本（如状态机、控制器、技能系统等）
│   ├── Prefabs/         # 角色、道具、UI 等预制体
│   ├── Scenes/          # 场景文件（.unity）
│   └── ...              # 其他资源：材质、动画、模型等
│
├── ProjectSettings/     # Unity 项目设置（图层、输入、构建设置等）
├── Packages/            # Unity 包管理器依赖（如 Input System、URP 等）
├── README.md            # 项目说明文件（三语）
├── .gitignore           # Git 忽略配置（不上传缓存、构建等）
└── .git/                # Git 版本控制数据（初始化后自动生成）
```
---

## **遇到的困难和解决方式**
- 脱离教程，100%原创代码
- 角色物理运动惯性模拟问题
- 架构设计的的平衡，如何设计优雅又没有过度设计
- 等等杂项

---

## **Coming Soon**
- 任务模块
- 对话系统
- 存档系统
- AI 行为树
---

## ⚠️ Notes

- Some third-party assets are used for prototyping and are **not covered by the MIT license**.
- License: None / All rights reserved.  
  If you wish to reuse parts of this project, please contact the author.

## ✅ 2025/06/26 更新日志

- 整合交互系统与状态机系统，支持一过性交互（如拾取）与状态型交互（如对话）
- 新增命令模式架构，支持交互逻辑解耦
- 状态切换流程优化，支持交互中移动退出、自动跳回 Idle
- UI 提示逻辑封装、状态同步机制完成
