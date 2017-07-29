using System;
using System.Collections.Generic;

namespace Game_OneToMore
{
	//静态类，提供游戏的逻辑，直接给Main函数调用
	public static class GameLogic
	{
		//声明Monster列表
		static List<Monster> monsterList;
		//声明Skill列表
		static List<Skill> skillList;
		static Player player;//玩家角色

		//技能记时
		static int[] countArray = {0, 0, 0, 0};


		public static void GameStart(){
			//声明技能
			skillList = new List<Skill> ();
			skillList.Add(new Skill ("普攻", "造成攻击力伤害", 0));
			skillList.Add(new Skill ("三刀流", "对单个目标连续攻击3次", 2));
			skillList.Add(new Skill ("致命一击", "对单个目标造成4倍普攻伤害", 3));
			skillList.Add(new Skill ("荆棘之舞", "对所以目标造成普攻伤害", 4));
			skillList.Add(new Skill ("死神魔咒", "对单个目标直接10倍伤害", 8));

			//声明Monster列表
			monsterList = new List<Monster> ();
			for (int i = 1; i <= 10; ++i) {
				monsterList.Add (new Monster ("Monster" + i, 10, 500));
			}


			Console.WriteLine ("*************************开始游戏********************* ");
			SelectPlayer ();

			while (true) {
				//选择技能
				SelectSkill ();
				if (player.isDead ()) {
					Console.WriteLine (">>>>>>>>>>>>>>>你已死亡，游戏结束！<<<<<<<<<<<<<<<<<");
					break;
				}else if(monsterList.Count == 0){
					Console.WriteLine ("*****************敌方团灭，胜利！******************");
					break;
				}

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
		}

		//三刀流
		private static void Ordinary_Q(){
			//判断技能是否冷却完成
			if (countArray[0] > 0) {
				Console.WriteLine ("Q技能冷却中，无法释放，剩余时间：" + countArray[0]);
				return;
			}
			//添加事件响应对象
			for (int i = 0; i < 3; ++i) {
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
		}

		//致命一击
		private static void Ordinary_W(){
			//判断技能是否冷却完成
			if (countArray[1] > 0) {
				Console.WriteLine ("W技能冷却中，无法释放，剩余时间：" + countArray[1]);
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
		}

		//荆棘之舞
		private static void Ordinary_E(){
			//判断技能是否冷却完成
			if (countArray[2] > 0) {
				Console.WriteLine ("E技能冷却中，无法释放，剩余时间：" + countArray[2]);
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
		}
		//死神魔咒
		private static void Ordinary_R(){
			//判断技能是否冷却完成
			if (countArray[3] > 0) {
				Console.WriteLine ("R技能冷却中，无法释放，剩余时间：" + countArray[3]);
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
		}

		//Monster死亡时，移除List中的monster,该方法给Monster的F_Attack调用
		public static void RemoveMonster(Monster p){
			Console.WriteLine (p.Name + "死亡");
			player.E_Attack -= p.F_Attack;
			monsterList.Remove (p);

		}
	}
}

