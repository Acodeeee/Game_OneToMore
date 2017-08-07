using System;

namespace Game_OneToMore
{
	public class Decorate : Equipment
	{
		public int HP{ get; set;}
		public int MP{ get; set;}

		public Decorate (EquipmentType type, string name, int price, int hp, int mp):base(type, name, price){
			MP = mp;
			HP = hp;
		}

		public override Equipment Clone (int id)
		{
			Decorate newD = new Decorate (this.Type, this.Name, this.Price, this.HP, this.MP);
			newD.ID = id;
			return newD;
		}

		public override string ToString ()
		{
			return string.Format ("{0}： ID: {1}  价格：{2}  生命值{3}  蓝值：{4}  等级：{5}", Name, ID, Price.ToString().PadRight(4), HP.ToString().PadRight(4), MP.ToString().PadRight(4), Rank);
		}
	}
}

