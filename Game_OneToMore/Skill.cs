using System;

namespace Game_OneToMore
{
	//技能类，派生至EventArgs
	public class Skill  : EventArgs
	{
		public string Name{ get; set;}
		public string Describe{ get; set;}

		//技能冷却记时,外部只读属性
		public int Count{ get; private set;}

		public Skill (string name, string describe,int count)
		{
			Name = name;
			Describe = describe;
			Count = count;
		}
	}

}

