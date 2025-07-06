# RPG-Demo 

一个基于Unity引擎开发的2D横版动作RPG游戏Demo

## 项目概述

本项目是一个2D动作游戏原型，包含了现代银河恶魔城游戏的核心机制：
- 状态机驱动的角色控制系统
- 战斗系统和敌人AI
## 主要特性
###  角色控制系统
- **状态机架构**：基于有限状态机的玩家控制系统
- **移动机制**：包含行走、跳跃、冲刺、墙壁滑行和墙跳
- **战斗系统**：主攻击和反击系统
- **物理交互**：碰撞检测、地面检测、墙壁检测
###  敌人系统
- **骷髅敌人**：基础敌人AI实现
- **状态管理**：敌人行为状态机
- **攻击机制**：敌人攻击检测
## 项目结构

```
Assets/
├── Animation/          # 动画控制器和动画文件
│   ├── Controllers/    # Animator Controllers
│   ├── Player/        # 玩家动画
│   └── Skeleton/      # 骷髅敌人动画
├── Graphics/          # 美术资源
│   ├── Main Character/ # 主角贴图
│   ├── Enemies/       # 敌人贴图
│   ├── FX/            # 特效素材
│   ├── UI/            # 界面素材
│   └── Surroundings/  # 环境贴图
├── Materials/         # 材质文件
├── Scenes/           # 游戏场景
│   └── SampleScene.unity
├── scripts/          # 核心代码
│   ├── Player/       # 玩家系统
│   ├── Enemy/        # 敌人系统
│   ├── Entity.cs     # 实体基类
│   ├── EntityFX.cs   # 特效系统
│   └── ParallaxBackGround.cs # 视差背景
└── Tile Palette/     # 地图编辑工具
```

## 核心系统说明

### 玩家状态系统
- `PlayerStateMachine.cs` - 状态机核心
- `PlayerState.cs` - 状态基类
- `PlayerIdleState.cs` - 空闲状态
- `PlayerMoveState.cs` - 移动状态
- `PlayerJumpState.cs` - 跳跃状态
- `PlayerDashState.cs` - 冲刺状态
- `PlayerWallSlideState.cs` - 墙壁滑行
- `PlayerWallJumpState.cs` - 墙跳
- `PlayerPrimaryAttack.cs` - 攻击
- `PlayerCounterAttack.cs` - 反击

**控制说明**
- WASD/方向键：移动
- 空格键：跳跃
- 鼠标左键：攻击
- Shift：冲刺
- 特殊技能：待定义

## 目前进度
![alt text](3fc7dba81f9febfa640aa632396c561.png)
![alt text](5d4d1cbbb8d5eae41f2bbd74f88a64b.png)
## 未来计划

- 更多敌人类型和AI行为
- 武器和装备系统
- 技能树和角色成长
- 关卡设计和地图系统
- 音效和背景音乐
- 存档系统
- 更多特效和画面优化

