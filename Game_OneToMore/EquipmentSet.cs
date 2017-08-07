using System;
using System.Collections.Generic;
using System.Collections;

namespace Game_OneToMore
{
	//存储equipment的List类，单例模式
	public class EquipmentSet
	{
		private readonly List<Weapon> weaponList;
		private readonly List<Clothes> clothsList;
		private readonly List<Decorate> decorateList;
		private readonly List<Medicine> medicineList;

		public ArrayList equipmentList;

		public int Count{
			get{ 
				return weaponList.Count + clothsList.Count + decorateList.Count + medicineList.Count;
			}
		}

		private EquipmentSet ()
		{
			//得到装备xml的数据
			weaponList = AnalyseXml.GetWeaponList ();
			clothsList = AnalyseXml.GetClothesList ();
			decorateList = AnalyseXml.GetDecorateList ();
			medicineList = AnalyseXml.GetMedicineList ();

			//把4种装备的List存到ArrayList中
			equipmentList = new ArrayList ();
			equipmentList.Add (weaponList);		//0
			equipmentList.Add (clothsList);		//1
			equipmentList.Add (decorateList);	//2
			equipmentList.Add (medicineList);	//3
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
			//展示Weapon
			Console.WriteLine ("************************* 武器 ************************");
			foreach(Weapon e in (equipmentList[0] as List<Weapon>)){
				Console.WriteLine (e);
			}
			//展示Clothes
			Console.WriteLine ("************************* 衣服 ************************");
			foreach(Clothes e in (equipmentList[1] as List<Clothes>)){
				Console.WriteLine (e);
			}
			//展示Decorate
			Console.WriteLine ("************************* 装饰品 ************************");
			foreach(Decorate e in (equipmentList[2] as List<Decorate>)){
				Console.WriteLine (e);
			}
			//展示Medicine
			Console.WriteLine ("************************* 药品 ************************");
			foreach(Medicine e in (equipmentList[3] as List<Medicine>)){
				Console.WriteLine (e);
			}

			Console.WriteLine ("***************************************************************");
			Console.WriteLine ("请输入ID进行购买(0退出商店)：");
			//如果输入不为数字则将selece赋值为-1
			try{
				select = int.Parse( Console.ReadLine ());
			}catch{
				select = -1;
			}
		}
		//根据ID返回Equipment的实例
		public Equipment GetEquipmentById(int id){
			//得到id所属物品在ArrayList中的下标
			int i = id/1000 - 1;

			//确定Equipment的具体类型
			switch (i) {
			case 0:
				foreach (Weapon e in weaponList) {
					if (e.ID == id) {
						return e;
					}
				}
				return null;

			case 1:
				foreach (Clothes e in clothsList) {
					if (e.ID == id) {
						return e;
					}
				}
				return null;

			case 2:
				foreach (Decorate e in decorateList) {
					if (e.ID == id) {
						return e;
					}
				}
				return null;

			case 3:
				foreach (Medicine e in medicineList) {
					if (e.ID == id) {
						return e;
					}
				}
				return null;

			default:
				return null;

			}
		}
			

	}
}

