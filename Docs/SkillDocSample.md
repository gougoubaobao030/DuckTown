# 技能系统文档（Skill System）

## 组成脚本
- SkillManager.cs：技能注册与调用
- BaseSkill.cs：技能基类
- FireballSkill.cs：火球术实现
- SkillCooldownHandler.cs：管理CD
- SkillUIController.cs：UI显示与冷却同步

## 流程说明
1. 状态机调用SkillManager.Cast("fireball")
2. SkillManager实例化FireballSkill
3. FireballSkill执行位移检测与伤害计算
4. 成功命中后触发冷却系统并广播事件

## 事件/接口
- SkillUsedEvent
- public void Cast(string skillId)
