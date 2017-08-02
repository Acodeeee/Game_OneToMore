using System;
using System.Collections.Generic;

namespace Game_OneToMore
{
	//存储equipment的List类，单例模式
	public class EquipmentSet
	{
		private readonly List<Equipment> equipmentList;
		public int Count{get{ return equipmentList.Count;}}

		private EquipmentSet ()
		{
			equipmentList = new List<Equipment> ();

			//初始化装备
			//string name,int price, int attack, int hp, int proOfCrit
			equipmentList.Add (new Equipment("长剑", 350, 10, 0, 0));
			equipmentList.Add (new Equipment("多兰之盾", 400, 0, 150, 0));
			equipmentList.Add (new Equipment("多兰之刃", 450, 8, 80, 0));
			equipmentList.Add (new Equipment("死刑宣告", 800, 15, 0, 10));
			equipmentList.Add (new Equipment("灵巧披风", 800, 0, 0, 20));
			equipmentList.Add (new Equipment("燃烧宝石", 800, 0, 200, 10));
			equipmentList.Add (new Equipment("巨人腰带", 1000, 0, 380, 0));
			equipmentList.Add (new Equipment("提亚马特", 1200, 20, 200, 0));
			equipmentList.Add (new Equipment("暴风之剑", 1300, 40, 0, 0));
			equipmentList.Add (new Equipment("斯特拉克的挑战护手", 2600, 50, 400, 0));
			equipmentList.Add (new Equipment("狂徒铠甲", 2850, 0, 800, 0));
			equipmentList.Add (new Equipment("无尽之刃", 3400, 70, 0, 20));
		}

		private static class Handle{
			public static EquipmentSet Instance = new EquipmentSet();
		}

		public static EquipmentSet GetInstance(){
			return Handle.Instance;
		}

		//遍历List
		public void Show(out int select){
			Console.WriteLine ("******************************商店******************************");
			foreach(Equipment e in equipmentList){
				Console.WriteLine (e);
			}
			Console.WriteLine ("***************************************************************");
			Console.WriteLine ("请输入ID进行购买(0退出商店)：");
			//如果输入不为数字则将selece赋值为0
			try{
				select = int.Parse( Console.ReadLine ());
			}catch{
				select = -1;
			}
		}
		//根据ID返回Equipment的实例
		public Equipment GetEquipmentById(int id){
			foreach (Equipment e in equipmentList) {
				if (e.ID == id) {
					return e;
				}
			}
			return null;
		}

	}
}

