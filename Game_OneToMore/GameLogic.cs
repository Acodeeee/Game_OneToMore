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
		//关卡技数
		private static int gamePass = 1;


		public static void GameStart(){
			//声明技能
			skillList = new List<Skill> ();
			skillList.Add(new Skill ("普攻", "造成攻击力伤害", 0));
			skillList.Add(new Skill ("三刀流", "对3个目标进行普攻", 2));
			skillList.Add(new Skill ("致命一击", "对单个目标造成4倍普攻伤害", 3));
			skillList.Add(new Skill ("荆棘之舞", "对所有目标造成普攻伤害", 4));
			skillList.Add(new Skill ("死神魔咒", "对单个目标直接10倍伤害", 8));

			//声明Monster列表
			MonsterSet ms = MonsterSet.GetInstance ();//获取到MonsterSet的实例
			monsterList = ms.getMonsterList (); //获取到List<Monster>实例
			ms.NextGame ();//生成第一关的List<Monster>列表



			Console.WriteLine ("*************************开始游戏********************* ");
			SelectPlayer ();//选择英雄
			Console.WriteLine ("*************************第" + gamePass +"关********************* ");
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
			Player TOP = new Player ("刀妹", 100, 4000);
			Player MID = new Player ("劫", 150, 2500);
			Player JGL = new Player ("寡妇", 150, 2000);
			Player ADC = new Player ("金克丝", 200, 2000);
			Player SUP = new Player ("风女", 50 ,2000);
			while (true) {
				bool isOut;
				Console.WriteLine ("\n1）.刀妹  2）.劫  3）.寡妇  4）.金克丝  5）.风女  \n请选择英雄：");
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
			int i = 0;
			foreach (Skill s in skillList) {
				if (i == 0) {
					Console.WriteLine (ss [i++] + ")." + s.Name + "  ");
				} else {
					Console.WriteLine (ss [i] + ")." + s.Name + "---> "+ s.Describe + "(冷却时间：" + countArray [i - 1] + "s) ");
					++i;
				}
			}

			Console.WriteLine ("\n请选择你要释放的技能：");
			while (true) {
				bool isOut;
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

