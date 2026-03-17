# RPG-Demo

基于 Unity 的 2D 横版动作 RPG Demo，当前版本已经从基础动作原型扩展为包含技能、属性、异常状态、背包和装备系统的可玩框架。

## 开发环境

- Unity: 2022.3.34f1c1
- 场景入口: `Assets/Scenes/SampleScene.unity`
- 代码目录: `Assets/Scripts`

## 当前已实现内容

### 1. 玩家与状态机

- 有限状态机驱动玩家行为。
- 已实现状态：Idle、Move、Jump、Air、Dash、WallSlide、WallJump、PrimaryAttack、CounterAttack、AimSword、CatchSword、Blackhole、Dead。
- 基础动作完整：移动、跳跃、冲刺、墙体交互、连击。

### 2. 敌人与 AI

- 已实现敌人类型：Skeleton。
- 敌人状态机：Idle、Move、Battle、Attack、Stunned、Dead。
- 支持反击窗口（可被眩晕）与攻击冷却。

### 3. 战斗与数值系统

- 物理伤害 + 法术伤害结算。
- 角色属性分层：
	- Major: strength / agility / intelligence / vitality
	- Defensive: maxHealth / armor / magicResistence / evasion
	- Offensive: damage / criticalChance / criticalPower
	- Magic: fire / ice / lightning damage
- 暴击、闪避、护甲与魔抗减伤已接入。
- 元素异常状态：Ignite、Chill、Shock。
- 受击反馈：击退、闪白、元素颜色特效。

### 4. 技能系统

- `Dash_Skill`: 冲刺技能冷却控制。
- `Clone_Skill`: 可在冲刺进出/反击成功时生成分身。
- `Sword_Skill`: 飞剑瞄准抛射，支持 Regular / Bounce / Pierce / Spin 四种模式。
- `Crystal_Skill`: 水晶放置与换位，支持多水晶逻辑与扩展参数。
- `Blackhole_Skill`: 黑洞控制、敌人冻结、热键标记与分身连击。

### 5. 物品、背包与装备

- 基于 ScriptableObject 的物品体系：`ItemData`、`ItemData_Equipment`。
- 支持拾取地面物品并入包（`ItemObject`）。
- 背包分为 Inventory（装备类）与 Stash（材料类）。
- 装备栏支持穿戴/卸下，并实时修改玩家属性。
- UI 已接入背包槽、装备槽、血条刷新。

### 6. 视觉与场景表现

- 视差背景（`ParallaxBackGround`）。
- 通用实体特效（受击闪白、点燃/冰缓/感电变色）。

## 操作说明（当前默认键位）

- `A/D` 或方向键：左右移动（Horizontal 轴）
- `Space`：跳跃 / 墙跳
- `LeftShift`：冲刺（受技能冷却限制）
- `Mouse0`：普通攻击（连击）
- `Q`：反击
- `Mouse1`（按住）：飞剑瞄准
- `Mouse1`（松开）：投掷飞剑（已有飞剑时会触发回收）
- `F`：水晶技能
- `R`：黑洞技能
- 黑洞热键：按敌人头顶提示按键，将敌人加入黑洞攻击目标

## 快速开始

1. 使用 Unity Hub 打开项目（Unity 版本建议与上文一致）。
2. 打开场景 `Assets/Scenes/SampleScene.unity`。
3. 确认场景内已配置：
	 - `PlayerManager`（引用玩家）
	 - `SkillManager`（同物体挂载各技能组件）
	 - `Inventory`（关联 UI Slot 父节点）
4. 点击 Play 进入测试。

## 代码结构（Scripts）

```text
Assets/Scripts/
├── Player/               # 玩家状态机与动作/战斗状态
├── Enemy/                # 敌人基类、状态机与 Skeleton 行为
├── Skill/                # 技能主体与控制器
│   └── Controller/
├── ItemsAndInventory/    # 物品定义、背包与装备逻辑
├── Chracter Stats/       # 属性与伤害结算（目录名保持项目现状）
├── UI/                   # 血条、背包槽、装备槽
├── Entity.cs             # 实体通用移动/碰撞/翻转
├── EntityFX.cs           # 通用受击与元素特效
└── ParallaxBackGround.cs # 视差背景
```

## 后续可迭代方向

- 增加敌人种类与更复杂 AI 行为树。
- 完善技能成长、解锁与数值平衡。
- 增加关卡流程、存档和音效系统。
- 补充测试场景与调试工具（技能参数面板、战斗日志等）。

