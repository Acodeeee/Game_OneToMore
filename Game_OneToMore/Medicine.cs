using System;

namespace Game_OneToMore
{
	public class Medicine : Equipment
	{
		public int HP{ get; set;}
		public int MP{ get; set;}
		public int Count{ get; set;}//给Bag使用
		public Medicine (EquipmentType type, string name, int price, int hp, int mp):base(type, name, price){
			HP = hp;
			MP = mp;
			Count = 1;
		}

		public override Equipment Clone (int id){
			Medicine newM = new Medicine (this.Type, this.Name, this.Price, this.HP, this.MP);
			newM.ID = id;
			return newM;
		}

		public override string ToString ()
		{
			return string.Format ("{0}： ID: {1}  价格：{2}  生命值：{3}  蓝值：{4}  等级：{5}", Name, ID, Price.ToString().PadRight(4), HP.ToString().PadRight(4), MP.ToString().PadRight(4), Rank);
		}
	}
}

