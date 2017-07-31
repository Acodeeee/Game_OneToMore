//
//  Monster.cs
//
//  Author:
//       wrf_mac <>
//
//  Copyright (c) 2017 wrf_mac
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Threading;

namespace Game_OneToMore
{
	public class Monster : Person
	{
		public Monster (string name, int attack, int hp) : base(name, attack, hp){}

		//响应Player的攻击事件
		public override void F_Attack (object o, EventArgs e){
			Thread.Sleep(800);
			if (o is Player && e is Skill && e != null) {
				Player p = (Player)o;
				Skill s = (Skill)e;


				Random rand = new Random ();
				//是否暴击
				bool isCrit = rand.Next (0, 100) <= p.ProOfCrit * 100;
				if (isCrit) {
					//暴击攻击
					int force = Convert.ToInt32( p.Attack * (1.0f + p.ProOfCrit));
					HP -= force;
					Console.WriteLine (p.Name + " 使用 " + s.Name + " 攻击 " + Name + "，产生暴击，暴击伤害：" + force);
				} else {
					//无暴击攻击
					HP -= p.Attack;
					Console.WriteLine (p.Name + " 使用 " + s.Name + " 攻击 " + Name + "，未产生暴击，伤害：" + p.Attack);
				}
				Console.WriteLine (Name + "剩余HP:" + HP);
				//Monster死亡加钱
				if (isDead ()) {
					p.Money += 100;
					GameLogic.RemoveMonster (this);
				}

			} else {
				Console.WriteLine ("攻击者不是Player或者无技能，错误!");
			}
		}
	}
}

