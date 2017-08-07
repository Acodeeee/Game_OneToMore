using System;
using System.Collections.Generic;
using System.Collections;

namespace Game_OneToMore
{
	public class Bag
	{
//		private readonly List<Weapon> weaponList;
//		private readonly List<Clothes> clothsList;
//		private readonly List<Decorate> decorateList;
//		private readonly List<Medicine> medicineList;
//
//		public ArrayList equipmentList;

		public readonly List<Equipment> equipmentList;


		//背包总容量
		private const int MaxSize = 6;

		public int Size{ get; private set;}

		private int Next_Id = 1;//作为下一件装备的ID
		public Bag ()
		{
			Size = 0;
			equipmentList = new List<Equipment> ();
		}

		//使用药品
		public void UseMedicine(Medicine m) {
			if (m.Count == 1)
			{
				this.RemoveEquipmentById(m.ID);
				Console.WriteLine("使用 " + m.Name + " 成功！");
			}
			else if (m.Count > 1)
			{
				--m.Count;
			}
			else { 
				Console.WriteLine("使用药品失败！");
			}
		}

		//装备加入背包,给装备重新分配一个内存地址
		public bool AddEquipment(Equipment e){
			//背包容量足够 或者 Type为MEDICINE则可以进行购买
			if (Size < MaxSize || e.Type == EquipmentType.MEDICINE) {
				foreach (Equipment item in equipmentList) {
					if (item.Name == e.Name && e.Type != EquipmentType.MEDICINE) {
						Console.WriteLine ("背包已存在 " + e.Name);
						return false;
					}
				}

				//匹配装备的类型，克隆商店的装备存储在Bag中
				switch (e.Type) {
				case EquipmentType.WEAPON:
						equipmentList.Add ((e as Weapon).Clone (Next_Id++) as Weapon);
					break;

				case EquipmentType.CLOTHES:
					equipmentList.Add ((e as Clothes).Clone (Next_Id++) as Clothes);
					break;

				case EquipmentType.DECORATE:
					equipmentList.Add ((e as Decorate).Clone(Next_Id++) as Decorate);
					break;

				case EquipmentType.MEDICINE:
					//如果已经存在
					foreach (Equipment item in equipmentList) {
						if (item.Name == e.Name) {
							(item as Medicine).Count++;//药品计数加1
							Console.WriteLine(e.Name + " 已存在，计数加1");
							return true;
						}
					}
						//背包容量有剩余才可以添加
						if (Size < MaxSize)
						{
							equipmentList.Add((e as Medicine).Clone(Next_Id++) as Medicine);
						}
						else {
							Console.WriteLine ("背包已满，无法继续购买装备");
							return false;
						}
					break;

				default:
					Console.WriteLine ("不存在的商品类型！");
					break;
				}

				++Size;
				return true;
			}

			Console.WriteLine ("背包已满，无法继续购买装备");
			return false;

		}

		//装备移除背包
		public bool RemoveEquipmentById(int id){
			bool isFind = false;//标识是否找到装备
			int tempID = id;

			Equipment tempE = null;
			foreach (Equipment e in equipmentList) {
				if (e.ID == id && !isFind ) {
					Console.WriteLine ("背包移除 " + e.Name + " 成功");
					tempE = e;
					isFind = true;
					Next_Id--;
					Size--;
				}
				//移除的装备后的装备ID都往前移
				else if (isFind) {
					e.ID = tempID++;
				}
			}
			if (isFind) {
				equipmentList.Remove(tempE);//进行移除操作
				return true;
			}
			Console.WriteLine ("背包中不存在ID为 {0} 的装备",id);
			return false;
		}
		//展示背包
		private void ShowBag(){
			if (equipmentList.Count == 0) {
				Console.WriteLine ("背包为空");
				return;
			}
			Console.WriteLine ("************************* 英雄背包 ************************");
			foreach (Equipment e in equipmentList) {
				//找到e的类型，然后将e向下转型为其类型
				switch (e.Type) {
				case EquipmentType.WEAPON:
					Console.WriteLine (e as Weapon);
					break;

				case EquipmentType.CLOTHES:
					Console.WriteLine (e as Clothes);
					break;

				case EquipmentType.DECORATE:
					Console.WriteLine (e as Decorate);
					break;

				case EquipmentType.MEDICINE:
					Console.WriteLine (e as Medicine + "  数量：" + (e as Medicine).Count);
					break;
				}
			}
				
		}

		#region 按不同属性排序

		//装备按ID升序排序输出，包含Attack属性的装备类型有Weapon,Clothes,Decorate,Medicine
		public void SortByID(){
			for (int i = 0; i < equipmentList.Count; i++) {
				for (int j = 1; j < equipmentList.Count - i; j++) {
					if (equipmentList [j - 1].ID > equipmentList [j].ID) {
						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}
			ShowBag ();
		}
			
		//装备按Rank升序排序输出
		public void SortByRank(){
			for (int i = 0; i < equipmentList.Count; i++) {
				for (int j = 1; j < equipmentList.Count - i; j++) {
					if (equipmentList [j - 1].Rank > equipmentList [j].Rank) {
						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}
			ShowBag ();
		}
	
		//装备按Price升序排序输出
		public void SortByPrice(){
			for (int i = 0; i < equipmentList.Count; i++) {
				for (int j = 1; j < equipmentList.Count - i; j++) {
					if (equipmentList [j - 1].Price > equipmentList [j].Price) {
						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}
			ShowBag ();
		}

		//装备按Attack升序排序输出，只有Weapon拥有Attack属性
		public void SortByAttack(){
			//统计拥有Attack属性的物品件数
			int count = 0;
			//该for循环负责将拥有Attack属性的装备置前
			for (int i=0; i<equipmentList.Count; ++i) {
				switch(equipmentList[i].Type){
				case EquipmentType.WEAPON:
					//将找到的匹配装备置前
					Equipment temp = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp;
					++count;
				
					break;
				}
			}
			for (int i = 0; i < count; ++i) {
				for (int j = 1; j < count - i; ++j) {
					if ((equipmentList [j - 1] as Weapon).Attack > (equipmentList [j] as Weapon).Attack) {
						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}

			ShowBag ();
		}

		//装备按HP升序排序输出，包含HP属性的装备类型有Clothes,Decorate,Medicine
		public void SortByHP(){
			int[] tempHPkArray = new int[MaxSize];//暂存拥有HP装备的HP数据
			int count = 0;	//统计拥有HP属性的物品件数
			//该for循环负责将拥有HP属性的装备置前
			for (int i=0; i<equipmentList.Count; ++i) {
				switch(equipmentList[i].Type){
				case EquipmentType.CLOTHES:
					//将找到的匹配装备置前
					Equipment temp1 = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp1;
					tempHPkArray [count] = (equipmentList [count] as Clothes).HP;//将HP存入
					count++;
					break;
				case EquipmentType.DECORATE:
					//将找到的匹配装备置前
					Equipment temp2 = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp2;
					tempHPkArray [count] = (equipmentList [count] as Decorate).HP;//将HP存入
					count++;
					break;
				case EquipmentType.MEDICINE:
					//将找到的匹配装备置前
					Equipment temp3 = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp3;
					tempHPkArray [count] = (equipmentList [count] as Medicine).HP;//将HP存入
					count++;
					break;
				}
			}

			for (int i = 0; i < count; ++i) {
				for (int j = 1; j < count - i; ++j) {
					if (tempHPkArray [j - 1] > tempHPkArray [j]) {
						int t = tempHPkArray [j - 1];
						tempHPkArray [j - 1] = tempHPkArray [j];
						tempHPkArray [j] = t;

						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}

			ShowBag ();
		}

		//装备按MP升序排序输出, 包含HP属性的装备类型有Decorate,Medicine
		public void SortByMP(){
			int[] tempMPArray = new int[MaxSize];//暂存拥有HP装备的HP数据
			int count = 0;	//统计拥有HP属性的物品件数
			//该for循环负责将拥有Attack属性的装备置前
			for (int i=0; i<equipmentList.Count; ++i) {
				switch(equipmentList[i].Type){
				case EquipmentType.DECORATE:
					//将找到的匹配装备置前
					Equipment temp1 = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp1;
					tempMPArray [count] = (equipmentList [count] as Decorate).MP;//将MP存入
					count++;
					break;
				case EquipmentType.MEDICINE:
					//将找到的匹配装备置前
					Equipment temp2 = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp2;
					tempMPArray [count] = (equipmentList [count] as Medicine).MP;//将MP存入
					count++;
					break;
				}
			}

			for (int i = 0; i < count; ++i) {
				for (int j = 1; j < count - i; ++j) {
					if (tempMPArray [j - 1] > tempMPArray [j]) {
						int t = tempMPArray [j - 1];
						tempMPArray [j - 1] = tempMPArray [j];
						tempMPArray [j] = t;

						Equipment temp = equipmentList [j - 1];
						equipmentList [j - 1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}

			ShowBag ();
		}

		//装备按ProOfCrit升序排序输出, 包含HP属性的装备类型有Weapon
		public void SortByProOfCrit(){
			//统计拥有ProOfCrit属性的物品件数
			int count = 0;
			//该for循环负责将拥有ProOfCrit属性的装备置前
			for (int i=0; i<equipmentList.Count; ++i) {
				switch(equipmentList[i].Type){
				case EquipmentType.WEAPON:
					//将找到的匹配装备置前
					Equipment temp = equipmentList [count];
					equipmentList [count] = equipmentList [i];
					equipmentList [i] = temp;
					count++;
					break;
				}
			}
			for (int i = 0; i < count; ++i) {
				for (int j = 1; j < count - i; ++j) {
					if ((equipmentList [j - 1] as Weapon).ProOfCrit > (equipmentList [j] as Weapon).ProOfCrit) {
						//将找到的匹配装备置前

						Equipment temp = equipmentList [j-1];
						equipmentList [j-1] = equipmentList [j];
						equipmentList [j] = temp;
					}
				}
			}

			ShowBag ();
		}
		#endregion
	}
}

