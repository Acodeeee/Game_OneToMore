using System;

namespace Game_OneToMore
{
	//技能类，派生至EventArgs
	public class Skill  : EventArgs
	{
		//用于绑定一个技能释放的具体逻辑方法（SkillLogic中）
		public delegate void SkillHandler(Player p, MonsterSet ms);
		//public event SkillHandler SkillEvent;

		public string Name{ get; set;}
		public string Describe{ get; set;}

		//技能消耗蓝量
		public int NeedMP{ get; set;}

		//技能冷却时间,外部只读属性
		public int Count{ get; private set;}

		internal Skill (string name, string describe, int count, int needMP = 0)
		{
			Name = name;
			Describe = describe;
			Count = count;
			NeedMP = needMP;
		}
	}

}

