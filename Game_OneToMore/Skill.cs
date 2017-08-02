using System;

namespace Game_OneToMore
{
	//技能类，派生至EventArgs
	public class Skill  : EventArgs
	{
		//用于绑定一个技能释放的具体逻辑方法（SkillLogic中）
		public delegate void SkillHandler(Player p, MonsterSet ms);
		public event SkillHandler SkillEvent;

		public string Name{ get; set;}
		public string Describe{ get; set;}

		//技能冷却记时,外部只读属性
		public int Count{ get; private set;}

		internal Skill (string name, string describe,int count)
		{
			Name = name;
			Describe = describe;
			Count = count;
		}
	}

}

