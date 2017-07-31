using System;
using System.Collections.Generic;

namespace Game_OneToMore
{
	public class MonsterSet
	{
		private static int bigMonsterCount = 2;
		private static int smallMonsterCount = 10;

		private List<Monster> monsterList;

		//将MonsterSet写为单例模式
		private MonsterSet(){
			monsterList = new List<Monster> ();
		}

		private static class Holder{
			public static MonsterSet Instance = new MonsterSet ();
		}

		public static MonsterSet GetInstance(){
			return Holder.Instance;
		}

		public List<Monster> getMonsterList(){
			return monsterList;
		}

		public void NextGame(){
			//添加monster
			for (int i = 1; i <= smallMonsterCount; ++i) {
				monsterList.Add (new Monster("小兵" + i, 10, 400));
			}
			for (int i = 1; i <= bigMonsterCount; ++i) {
				monsterList.Add (new Monster("大炮兵" + i, 30, 800));
			}
			//monster增加，用于下一关
			bigMonsterCount++;
			smallMonsterCount += 5;
		}
	}
}

