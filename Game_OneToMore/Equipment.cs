using System;

namespace Game_OneToMore
{
	public enum EquipmentType{
		WEAPON,
		CLOTHES,
		DECORATE,
		MEDICINE
	}
	//装备类
	public abstract class Equipment
	{
		public int ID{ get; set;}

		public EquipmentType Type{ get; set;}

		//装备等级
		public int Rank{ get; set;}

		public string Name{ get; set;}

		//价格
		public int Price{ get; set;}


		//给不同类型装备赋值
		private static int NowID_W = 1000;
		private static int NowID_C = 2000;
		private static int NowID_D = 3000;
		private static int NowID_M = 4000;



		protected Equipment (EquipmentType type, string name, int price)
		{
			switch (type) {
			case EquipmentType.WEAPON:
				ID = ++NowID_W;
				break;

			case EquipmentType.CLOTHES:
				ID = ++NowID_C;
				break;

			case EquipmentType.DECORATE:
				ID = ++NowID_D;
				break;

			case EquipmentType.MEDICINE:
				ID = ++NowID_M;
				break;
			}
			Rank = 1;
			Type = type;
			Name = name;
			Price = price;
		}



		//克隆当前的实例返回，用于加入背包操作
		public abstract Equipment Clone (int id);
			
	}
}

