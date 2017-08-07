using System;
using System.Threading;

namespace Game_OneToMore
{
	public static class EquipmentLogic
	{
		private static Player mPlayer;//存储Palyer实例,在Shopping函数中获得

		//选择进入商店还是背包
		internal static void ShopOrBag(Player player){
			mPlayer = player;
			while(true){
				Console.WriteLine("1.进入商店  2.打开背包  0.开始游戏\n请选择：");
				bool isOut = false;
				switch (Console.ReadLine ()) {
				case "0":
					isOut = true;
					break;
				case "1":
					Shopping (player);
					break;

				case "2":
					OperateBag (player.bag);
					break;

				default:
					Console.WriteLine ("输入错误！");
					break;
				}
				if (isOut) {
					break;
				}
			}
		}

		//进入商店
		internal static void Shopping(Player player){
			Console.Write ("正在进入商店");
			for (int i = 0; i < 10; ++i) {
				Console.Write (".");
				Thread.Sleep (200);
			}
			while (true) {
				int select;//select作为Show的输出参数，表示用户的装备选择
				EquipmentSet es = EquipmentSet.GetInstance ();

				Console.WriteLine ("\n__________________________________________________________");
				Console.WriteLine (player);//展示英雄属性
				es.Show (out select);//展示装备

				//检测select的合法性
				if (select == -1) {
					Console.WriteLine ("输入错误！");
					//退出商店
				} else if (select == 0) {
					return;
				} else {
					//通过select（ID）得到Equipment的实例
					Equipment e = es.GetEquipmentById (select);
					if (e != null) {
						player.BuyEquipment (e);
					} else {
						Console.WriteLine ("没有ID为{0}的装备",select);
					}
				}
				Console.Write ("加载中");
				for (int i = 0; i < 10; ++i) {
					Console.Write (".");
					Thread.Sleep (200);
				}

			}
		}
		//进入背包
		internal static void OperateBag(Bag bag){
			//展示背包，默认按ID排序展示
			bag.SortByID ();

			while(true){
				//提供背包物品展示排序方式
				Console.WriteLine ("1.按ID排序  2.按等级排序  3.按价格排序  4.按攻击力排序  5.按生命值排序  6.按蓝值排序  7.按暴击率排序");
				Console.WriteLine ("A.进入锻造炉  0.关闭背包\n请选择：");
				bool isOut = false;
				switch (Console.ReadLine ()) {
				case "0":
					//回到上一目录
					isOut = true;
					break;
				case "1":
					bag.SortByID ();
				
					break;

				case "2":
					bag.SortByRank ();

					break;

				case "3":
					bag.SortByPrice ();

					break;

				case "4":
					bag.SortByAttack ();

					break;

				case "5":
					bag.SortByHP ();

					break;

				case "6":
					bag.SortByMP ();
				
					break;

				case "7":
					bag.SortByProOfCrit ();

					break;

				case "A":
					//进入锻造炉
					UpdateSelect(bag);
					
					break;

				default:
					Console.WriteLine ("输入错误，请重新输入：");
					isOut = false;
					break;
				}

				if (isOut) {
					break;
				}
			}

		}
		internal static void UpdateSelect(Bag bag) {
			bool isOut1 = false;
			while (!isOut1) { 
				Console.WriteLine(mPlayer);
				Console.WriteLine("********************************流浪法师的锻造炉*******************************");
				bag.SortByID();
				Console.WriteLine("1.升级装备  2.熔合装备  0.退出");
				string s = Console.ReadLine();
				//升级装备
				if (s.Equals("1"))
				{
					Console.WriteLine("请选择你需要升级的装备ID(0退出)：");
					bool isOut2 = false;
					while (!isOut2)
					{
						bool isFind = false;//判断是否在Bag中找到此ID
						int id = -1;
						try
						{
							id = int.Parse(Console.ReadLine());
						}
						catch {
							id = -1;
						}

						if (id == -1)
						{
							Console.WriteLine("输入错误，请重新输入！");
							isFind = true;
						}
						else if (id == 0) {
							break;
						}

						foreach (Equipment e in bag.equipmentList)
						{
							if (e.Type != EquipmentType.MEDICINE)
							{
								if (e.ID == id)
								{
									//扣钱，升级需要扣除原装备的价格
									if (mPlayer.Money >= e.Price)
									{
										mPlayer.Money -= e.Price;
										UpdateEquipment(e);
										isOut2 = true;
										isFind = true;
										Console.WriteLine(e.Name + " 升级成功");
									}
									else
									{
										Console.WriteLine("金钱不足，无法升级！");
										return;
									}
									break;
								}


							}
							else if(e.Type == EquipmentType.MEDICINE && e.ID == id){
								Console.WriteLine("药品不能升级！请重新输入：");
								isFind = true;
							}
						}
						if (!isFind) { 
							Console.WriteLine("未在背包中找到匹配的装备！");
						}
					}
				}
				//熔合装备
				else if (s.Equals("2"))
				{
					bag.SortByID();
					Console.WriteLine("请选择你需要熔合的两件装备ID(空格分隔，0退出)：");
					while (true)
					{
						string twoIDStr = Console.ReadLine();
						//对字符串解析,获取输入的ID
						int id1, id2, outNum;
						try
						{
							//获取输入是否为0
							outNum = int.Parse(twoIDStr);
							id1 = -1;
							id2 = -1;
						}
						catch
						{
							outNum = -1;
							try
							{
								id1 = int.Parse(twoIDStr.Substring(0, twoIDStr.IndexOf(' ')));
								id2 = int.Parse(twoIDStr.Substring(twoIDStr.IndexOf(' ') + 1));
							}
							catch
							{
								id1 = -1;
								id2 = -1;
							}
						}
						//outNum为0退出
						if (outNum == 0)
						{
							break;
						}

						//id1 == -1 || id2 == -1输入错误
						else if (id1 == -1 || id2 == -1)
						{
							Console.WriteLine("输入错误！请重新输入：");
						}
						//id1 != -1 && id2 != -1输入正确
						else if (id1 != -1 && id2 != -1) {
							Equipment e1 = null, e2 = null;
							foreach (Equipment e in bag.equipmentList) {
								if (e.ID == id1)
								{
									e1 = e;
								}
								else if (e.ID == id2)
								{
									e2 = e;
								}
							}
							if (e1 == null || e2 == null)
							{
								Console.WriteLine("背包中不存在此ID装备，请重新输入：");
								continue;
							}
							//装备类型相同进行熔合
							else if (e1.Type == e2.Type)
							{
								Equipment e = MixEquipment(e1, e2);
								if (e != null)
								{
									//移除原装备，添加新装备
									bag.RemoveEquipmentById(e1.ID);
									bag.RemoveEquipmentById(e2.ID);
									bag.AddEquipment(e);
									Console.WriteLine(e.Name + "合成成功！");
								}
								else { 
									Console.WriteLine("合成失败！");
								}
								break;
							}
							else {
								Console.WriteLine("您输入的装备类型不同，请重新输入：");
							}
						}
					}

				}
				else if (s.Equals("0"))
				{
					isOut1 = true;
				}
				else {
					Console.WriteLine("输入错误，请重新输入！");
				}
			}
		}

		//升级物品
		private static bool UpdateEquipment(Equipment e){
			//判断e的类型
			switch (e.Type) {
			case EquipmentType.WEAPON:
				Weapon w = e as Weapon;
				if (w != null) {
					//升级不加暴击率
					w.Rank++;
					w.Attack *= 2;
					w.Price *= 2;

					GameLogic.UpdateInfo(w);
					return true;
				}
				break;

			case EquipmentType.CLOTHES:
				Clothes c = e as Clothes;
				if (c != null) {
					c.Rank++;
					c.HP *= 2;
					c.Price *= 2;
					GameLogic.UpdateInfo(c);
					return true;
				}

				break;

			case EquipmentType.DECORATE:
				Decorate d = e as Decorate;
				if (d != null) {
					d.Rank++;
					d.HP *= 2;
					d.MP *= 2;
					d.Price *= 2;
					GameLogic.UpdateInfo(d);
					return true;
				}

				break;

			}
			Console.WriteLine ("升级失败！");
			return false;
		}

		//熔合装备,将合成的装备返回
		private static Equipment MixEquipment(Equipment e1, Equipment e2) {
			string name = e1.Name.Substring(0, 2) + e2.Name.Substring(2);//名字使用两件装备组合，字符串截取
			int price = e1.Price + e2.Price;//价格等于两件装备的总和

			switch (e1.Type) { 
				case EquipmentType.WEAPON:
					Weapon w1 = e1 as Weapon;
					Weapon w2 = e2 as Weapon;
					int wAttack = w1.Attack + w2.Attack;//攻击力等于两件装备的总和
					int wProOfCrit = w1.ProOfCrit + w2.ProOfCrit;//暴击率等于两件装备总和
					return new Weapon(EquipmentType.WEAPON, name, price, wAttack, wProOfCrit);

				case EquipmentType.CLOTHES:
					Clothes c1 = e1 as Clothes;
					Clothes c2 = e2 as Clothes;
					int cHP = c1.HP + c2.HP;//HP等于两件装备的总和
					return new Clothes(EquipmentType.CLOTHES, name, price, cHP);

				case EquipmentType.DECORATE:
					Decorate d1 = e1 as Decorate;
					Decorate d2 = e2 as Decorate;
					int dHP = d1.HP + d2.HP;//HP等于两件装备的总和
					int dMP = d1.MP + d2.MP;//MP等于两件装备的总和
					return new Decorate(EquipmentType.DECORATE, name, price, dHP, dMP);

				case EquipmentType.MEDICINE:
					Console.WriteLine("药品无法熔合");

					return null;

				default:
					return null;

			}
		}

	}
}

