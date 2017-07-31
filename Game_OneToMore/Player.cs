//
//  Player.cs
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

using System.Collections.Generic;
using System.Threading;


namespace Game_OneToMore
{
	public class Player : Person
	{
		//装备List
		List<Equipment> eqList;
		//金钱
		public int Money{ get; set;}
		//暴击率
		private float proOfCrit;
		public float ProOfCrit { 
			get{ return proOfCrit; }
			private set { 
				//暴击率在0 ～ 100%之间
				if (value > 0.0f && value < 1.0f) {
					proOfCrit = value;
				} else {
					proOfCrit = 0.0f;
				}
			}
		}


		public Player (string name, int attack, int hp) : base(name, attack, hp){
			eqList = new List<Equipment>();
			proOfCrit = 0.3f;
		}

		//买装备
		public void BuyEquipment(Equipment e){
			if(this.Money < e.Price){
				Console.WriteLine ("金钱不够，无法购买！");
				return;
			}
			//改变自身属性
			this.Money -= e.Price;
			this.Attack += e.Attack;
			this.HP += e.Attack;
			this.ProOfCrit += e.ProOfCrit;
			Console.WriteLine ("购买" + e.Name + "成功");
			eqList.Add (e);
		}
		//卖装备，以原价格的50%出售
		public void SellEquipment(Equipment e){
			//改变自身属性
			this.Money += e.Price/2;
			this.Attack -= e.Attack;
			this.HP -= e.Attack;
			this.ProOfCrit -= e.ProOfCrit;
			Console.WriteLine ("出售" + e.Name + "成功");
			eqList.Remove (e);
		}


		//响应Monster的攻击指令
		public override void F_Attack (object o, EventArgs e){
			Thread.Sleep(500);
			if (o is Monster && e is Skill && e != null) {
				Monster m = (Monster)o;
				Skill s = (Skill)e;

				HP -= m.Attack;
				Console.WriteLine (m.Name + " 使用 " + s.Name + " 攻击 " + Name + "，伤害：" + m.Attack);
				Console.WriteLine (Name + "剩余HP:" + HP);

			} else {
				Console.WriteLine ("攻击者不是Monster或者无技能，错误!");
			}
		}
	}
}

