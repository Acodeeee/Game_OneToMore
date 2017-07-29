//
//  Equipment.cs
using System;

namespace Game_OneToMore
{
	//装备类
	public class Equipment
	{
		public string Name{ get; set;}

		//价格
		public int Price{ get; set;}

		//武器攻击力
		public int Attack{ get; set;}

		public int HP{ get; set;}

		//暴击率
		public float ProOfCrit { 
			get{ return ProOfCrit; }
			private set { 
				//暴击率在0 ～ 20%之间
				if (value > 0.0f && value < 0.2f) {
					ProOfCrit = value;
				} else {
					ProOfCrit = 0.0f;
				}
			}
		}



		public Equipment (string name,int price, int attack, int hp, int proOfCrit)
		{
			Name = name;
			Price = price;
			Attack = attack;
			HP = hp;
			ProOfCrit = proOfCrit;
		}
	}
}

