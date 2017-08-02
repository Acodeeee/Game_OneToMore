//
//  Equipment.cs
using System;

namespace Game_OneToMore
{
	//装备类
	public class Equipment
	{
		public int ID{ get;}

		public string Name{ get; set;}

		//价格
		public int Price{ get; set;}

		//武器攻击力
		public int Attack{ get; set;}

		//生命值
		public int HP{ get; set;}

		//暴击率
		private int proOfCrit;
		public int ProOfCrit { 
			get{ return proOfCrit; }
			private set { 
				//暴击率在0 ～ 20%之间
				if (value > 0 && value < 100) {
					proOfCrit = value;
				} else {
					proOfCrit = 0;
				}
			}
		}

		private static int NowID = 0;

		public Equipment (string name,int price, int attack, int hp, int proOfCrit)
		{
			ID = ++NowID;
			Name = name;
			Price = price;
			Attack = attack;
			HP = hp;
			ProOfCrit = proOfCrit;
		}
		public override string ToString ()
		{
			string s = "ID: " + this.ID + "  名字: " + this.Name + "  价格：" + this.Price;
			if (this.Attack != 0) {
				s += "  攻击力：" + this.Attack;
			}
			if (this.HP != 0) {
				s += "  生命值：" + this.HP;
			}
			if (proOfCrit != 0) {
				s += " 暴击率：" + this.ProOfCrit + "%";
			}
			return s;
		}
	}
}

