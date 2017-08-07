using System;
using System.Collections.Generic;
using System.Threading;

namespace Game_OneToMore
{
	//静态类，提供游戏的逻辑，直接给Main函数调用
	public static class GameLogic
	{
		//声明Monster列表
		private static List<Monster> monsterList;

		//声明Skill列表
		private static List<Skill> skillList;
		private static Player player;//玩家角色

		//技能记时
		private static int[] countArray = {0, 0, 0, 0};

		//判断英雄是否有效攻击
		private static bool isEffective;
		//关卡计数
		private static int gamePass = 1;


		public static void GameStart(){
			//声明技能
			skillList = new List<Skill> ();
			skillList.Add(new Skill ("普攻", "造成攻击力伤害", 0));
			skillList.Add(new Skill ("三刀流", "对3个目标进行普攻", 2, 20));
			skillList.Add(new Skill ("致命一击", "对单个目标造成4倍普攻伤害", 3, 30));
			skillList.Add(new Skill ("荆棘之舞", "对所有目标造成普攻伤害", 4, 40));
			skillList.Add(new Skill ("死神魔咒", "对单个目标直接10倍伤害", 8, 50));

			//获取Monster列表
			MonsterSet ms = MonsterSet.GetInstance ();//获取到MonsterSet的实例
			monsterList = ms.getMonsterList (); //获取到List<Monster>实例
			ms.NextGame ();//生成第一关的List<Monster>列表


			Console.WriteLine ("*************************欢迎来到召唤师峡谷********************* ");
			UserHelp();
			SelectPlayer ();//选择英雄
			Console.WriteLine ("*************************第" + gamePass +"关********************* ");

			//输出当前英雄信息
			Console.WriteLine (player);
			//进入商店购物或者进入背包
			EquipmentLogic.ShopOrBag(player);

			while (true) {
				SelectSkill ();//player选择技能并攻击
				if (isEffective) {
					Console.WriteLine ("-------------------------------------------");
					Thread.Sleep (1000);
					MonsterAttack ();//monster攻击
				}

				if (player.isDead ()) {
					Console.WriteLine (">>>>>>>>>>>>>>>你已死亡，游戏结束！<<<<<<<<<<<<<<<<<");
					break;
				} else if (monsterList.Count == 0) {
					Console.WriteLine ("*****************敌方团灭，胜利！******************");

					bool isOut = false;//判断是否退出游戏
					while (true) {
						Console.WriteLine ("是否继续下一关（ Y/N ）：");
						string isContinue = Console.ReadLine ();
						if (isContinue.Equals ("Y")) {
							ms.NextGame ();
							Console.WriteLine ("*************************第" + (++gamePass) +"关********************* ");
							//技能冷却归零
							for (int i = 0; i < countArray.Length; ++i) {
								countArray[i] = 0;
							}
							//输出当前英雄信息
							Console.WriteLine (player);
							EquipmentLogic.ShopOrBag(player);
							break;
						} else if (isContinue.Equals ("N")) {
							isOut = true;
							break;
						} else {
							Console.WriteLine ("输入错误，请重新输入：");
						}

					}
					if (isOut) {
						break;
					}
				}

			}
		}
		//帮助手册
		private static void UserHelp() {
			string str = "亲爱的召唤师，欢迎来到新手训练营。\n" +
				"进入游戏您将拥有一定的金钱，您可以在商店中挥霍您的金钱，购买您的英雄所需的装备。\n" +
				"另外，您可以进入锻造炉升级背包中的装备，当然，您还可以将两件相同属性的装备进行熔合，但您的药品不能进行升级或熔合。\n" +
				"在战斗中，您的英雄拥有普攻和4个技能，技能释放需要冷却时间和蓝量，请注意控制您的蓝量。当然，您可以在战斗中通过药品随时补给您的血量和蓝量。\n" +
				"通过击杀敌军，您可以获得相应的金钱。每一关都会增加难度，当然，您得保持存活，祝您好运。每一关开始前，您都有进入商店购买装备和进入锻造炉升级您的英雄装备的机会。\n" +
				"碾碎他们，" +
				"或者被他们碾碎吧！\n";
			Console.WriteLine("1.进入游戏  2.游戏帮助  请输入：");
			while (true)
			{
				switch (Console.ReadLine())
				{
					case "1":
						return;

					case "2":
						foreach (char c in str) {
							Random rand = new Random();
							int sTime = rand.Next(100,400);
							Console.Write(c);
							Thread.Sleep(sTime);
						}
						Console.WriteLine("（进入游戏请输入1）");
						break;

					default:
						Console.WriteLine("输入错误，请重新输入：");
						break;
				}
			}

		}

		//更新英雄属性,装备升级后增加英雄的属性
		public static void UpdateInfo(Equipment e){
				switch (e.Type) { 
				case EquipmentType.WEAPON:
					player.Attack += (e as Weapon).Attack / 2;
					break;

				case EquipmentType.CLOTHES:
					player.HP += (e as Clothes).HP / 2;
				
					break;

				case EquipmentType.DECORATE:
					player.HP += (e as Decorate).HP / 2;
					player.MP += (e as Decorate).MP / 2;

					break;

				}
		}

		//monster攻击
		private static void MonsterAttack(){
			foreach(Monster m in monsterList){
				m.E_Attack += player.F_Attack;
				m.StartAttact (skillList[0]);
				m.E_Attack -= player.F_Attack;
			}
		}

		//选择英雄
		private static void SelectPlayer(){
			//声明Player,上，中，野，AD，辅
			Player TOP = new Player ("刀锋意志", 200, 4000);
			Player MID = new Player ("影流之主", 250, 2500);
			Player JGL = new Player ("迅捷斥候", 250, 2300);
			Player ADC = new Player ("暴走萝莉", 300, 2000);
			Player SUP = new Player ("风暴之怒", 100 ,2600);
			while (true) {
				bool isOut;
				Console.WriteLine ("\n" + "1." + TOP + "2." + MID + "3." + JGL + "4." + ADC + "5." + SUP +"\n请选择英雄：");
				switch (Console.ReadLine ()) {
				case "1":
					player = TOP;
					isOut = true;
					break;

				case "2":
					player = MID;
					isOut = true;
					break;

				case "3":
					player = JGL;
					isOut = true;
					break;

				case "4":
					player = ADC;
					isOut = true;
					break;

				case "5":
					player = SUP;
					isOut = true;
					break;

				default:
					Console.WriteLine ("输入错误，请重新输入!");
					isOut = false;
					break;
				}
				if (isOut) {
					break;
				}
			}
		}

		//选择技能攻击
		private static void SelectSkill(){
			string[] ss = new string[]{ "A","Q", "W", "E", "R" };
			//输出当前英雄信息
			Console.WriteLine (player);
			int i = 0;
			//遍历技能展示
			foreach (Skill s in skillList) {
				if (i == 0) {
					Console.WriteLine (ss [i++] + ")." + s.Name + "  ");
				} else {
					Console.WriteLine (ss [i] + ")." + s.Name + "---> "+ s.Describe + "，需要蓝量：" + s.NeedMP + "( 冷却时间：" + countArray [i - 1] + "s)");
					++i;
				}
			}
			Console.WriteLine("1.使用药品：");

			Console.WriteLine ("\n请选择你要释放的技能：");
			while (true) {
				bool isOut = false;
				switch (Console.ReadLine ()) {
				case "A":
					Ordinary ();
					isOut = true;
					break;

				case "Q":
					Ordinary_Q ();
					isOut = true;
					break;

				case "W":
					Ordinary_W ();
					isOut = true;
					break;

				case "E":
					Ordinary_E ();
					isOut = true;
					break;

				case "R":
					Ordinary_R ();
					isOut = true;
					break;
				case "1":
					//使用药品
					UseMedicine();
					isOut = true;
					break;

				default:
					Console.WriteLine ("技能释放错误，请重新释放！");
					isOut = false;
					break;

				}
				if (isOut || monsterList.Count == 0) {
					break;
				}
			}
		}
		//使用药品
		private static void UseMedicine() { 
			//打印出药品
			foreach (Equipment e in player.bag.equipmentList) {
				if (e.Type == EquipmentType.MEDICINE) { 
					Console.WriteLine(e + "  数量：" + (e as Medicine).Count);
				}			
			}
			bool isOut = false;
			while (!isOut)
			{
				Console.WriteLine("请输入需要使用的药品ID（0退出)：");
				string sID = Console.ReadLine();
				foreach (Equipment e in player.bag.equipmentList)
				{
					//类型为药品，且输入ID正确就使用药品
					if (e.Type == EquipmentType.MEDICINE && sID == e.ID.ToString())
					{
						player.EatMedicine(e as Medicine);
						return;
					}
					else if (sID == 0.ToString())
					{
						isOut = true;
					}
				}
				if(!isOut) { 
					Console.WriteLine("输入错误，请重新输入！");
				}
			}
		}

		//普攻
		private static void Ordinary(){
			player.E_Attack += monsterList [0].F_Attack;
			player.StartAttact (skillList[0]);
			if (monsterList.Count > 0) {
				player.E_Attack -= monsterList [0].F_Attack;
			}

			//技能冷却记时
			if (countArray[0] > 0) {
				--countArray[0];
			}
			if (countArray[1] > 0) {
				--countArray[1];
			}
			if (countArray[2] > 0) {
				--countArray[2];
			}
			if (countArray[3] > 0) {
				--countArray[3];
			}
			isEffective = true;
		}

		//三刀流
		private static void Ordinary_Q(){
			//判断技能是否冷却完成
			if (countArray[0] > 0) {
				Console.WriteLine ("Q技能冷却中，无法释放，剩余时间：" + countArray[0]);
				isEffective = false;
				return;
			}
			//判断蓝量是否足够
			if (player.MP < skillList[1].NeedMP) {
				Console.WriteLine ("释放Q技能蓝量不足，无法释放，剩余蓝量：" + player.MP);
				isEffective = false;
				return;
			}
			//添加事件响应对象
			for (int i = 0; i < (3 < monsterList.Count ? 3 : monsterList.Count); ++i) {
				player.E_Attack += monsterList [i].F_Attack;
			}
			//执行事件
			player.StartAttact (skillList[1]);
			//移除事件响应对象
			for (int i = 0; i < (monsterList.Count < 3 ? monsterList.Count : 3); ++i) {
				player.E_Attack -= monsterList [i].F_Attack;

			}

			//减蓝量
			player.MP -= skillList [1].NeedMP;

			//技能冷却记时
			countArray[0] = skillList [1].Count;
			if (countArray[1] > 0) {
				--countArray[1];
			}
			if (countArray[2] > 0) {
				--countArray[2];
			}
			if (countArray[3] > 0) {
				--countArray[3];
			}
			isEffective = true;
		}

		//致命一击
		private static void Ordinary_W(){
			//判断技能是否冷却完成
			if (countArray[1] > 0) {
				Console.WriteLine ("W技能冷却中，无法释放，剩余时间：" + countArray[1]);
				isEffective = false;
				return;
			}
			//判断蓝量是否足够
			if (player.MP < skillList [2].NeedMP) {
				Console.WriteLine ("释放W技能蓝量不足，无法释放，剩余蓝量：" + player.MP);
				isEffective = false;
				return;
			}

			player.E_Attack += monsterList [0].F_Attack;
			player.Attack *= 3; 
			player.StartAttact (skillList[2]);
			player.Attack /= 3;
			if (monsterList.Count > 0) {
				player.E_Attack -= monsterList [0].F_Attack;
			}

			//技能冷却记时
			countArray[1] = skillList [2].Count;
			if (countArray[0] > 0) {
				--countArray[0];
			}
			if (countArray[2] > 0) {
				--countArray[2];
			}
			if (countArray[3] > 0) {
				--countArray[3];
			}
			//减蓝量
			player.MP -= skillList [2].NeedMP;

			isEffective = true;
		}

		//荆棘之舞
		private static void Ordinary_E(){
			//判断技能是否冷却完成
			if (countArray[2] > 0) {
				Console.WriteLine ("E技能冷却中，无法释放，剩余时间：" + countArray[2]);
				isEffective = false;
				return;
			}
			//判断蓝量是否足够
			if (player.MP < skillList [3].NeedMP) {
				Console.WriteLine ("释放E技能蓝量不足，无法释放，剩余蓝量：" + player.MP);
				isEffective = false;
				return;
			}

			foreach(Monster m in monsterList){
				player.E_Attack += m.F_Attack;
			}

			player.StartAttact (skillList[3]);

			foreach(Monster m in monsterList){
				player.E_Attack -= m.F_Attack;
			}

			//技能冷却记时
			countArray[2] = skillList [3].Count;
			if (countArray[0] > 0) {
				--countArray[0];
			}
			if (countArray[1] > 0) {
				--countArray[1];
			}
			if (countArray[3] > 0) {
				--countArray[3];
			}

			//减蓝量
			player.MP -= skillList [3].NeedMP;

			isEffective = true;
		}
		//死神魔咒
		private static void Ordinary_R(){
			//判断技能是否冷却完成
			if (countArray[3] > 0) {
				Console.WriteLine ("R技能冷却中，无法释放，剩余时间：" + countArray[3]);
				isEffective = false;
				return;
			}

			//判断蓝量是否足够
			if (player.MP < skillList [4].NeedMP) {
				Console.WriteLine ("释放E技能蓝量不足，无法释放，剩余蓝量：" + player.MP);
				isEffective = false;
				return;
			}

			player.E_Attack += monsterList [0].F_Attack;
			player.Attack *= 10; 

			player.StartAttact (skillList[4]);

			if (monsterList.Count > 0) {
				player.E_Attack -= monsterList [0].F_Attack;
			}
			player.Attack /= 10;

			//技能冷却记时
			countArray[3] = skillList [4].Count;
			if (countArray[0] > 0) {
				--countArray[0];
			}
			if (countArray[1] > 0) {
				--countArray[1];
			}
			if (countArray[2] > 0) {
				--countArray[2];
			}

			//减蓝量
			player.MP -= skillList [4].NeedMP;

			isEffective = true;
		}

		//Monster死亡时，移除List中的monster,该方法给Monster的F_Attack调用
		public static void RemoveMonster(Monster p){
			Console.WriteLine (p.Name + "死亡");
			player.E_Attack -= p.F_Attack;
			monsterList.Remove (p);

		}
	}
}

