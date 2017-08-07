using System;

namespace Game_OneToMore
{
	public class Weapon : Equipment
	{
		//攻击力
		public int Attack{ get; set;}
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
		public Weapon(EquipmentType type, string name,int price, int attack, int _proOfCrit):base(type, name, price){
			Attack = attack;
			ProOfCrit = _proOfCrit;
		}
		//克隆this返回，用于添加背包操作
		public override Equipment Clone (int id)
		{
			Weapon newW = new Weapon (this.Type, this.Name, this.Price, this.Attack, this.ProOfCrit);
			newW.ID = id;
			return newW;
		}

		public override string ToString ()
		{
			return string.Format ("{0}:  ID：{1}  价格：{2}  攻击力：{3}  暴击率：{4}%  等级：{5}", Name, ID, Price.ToString().PadRight(4), Attack.ToString().PadRight(4), ProOfCrit, Rank);
		}
	}
}

