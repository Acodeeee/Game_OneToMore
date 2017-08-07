using System;
using System.Collections.Generic;
using System.Threading;


namespace Game_OneToMore
{
	public class Player : Person
	{
		//背包
		public Bag bag{ get; set;}
		//技能List
		public List<Skill> skillList;

		public int Money{ get; set;}
		//暴击率
		private int proOfCrit;
		public int ProOfCrit { 
			get{ return proOfCrit; }
			private set { 
				//暴击率在0 ～ 100%之间
				if (value > 0 && value < 100) {
					proOfCrit = value;
				} else {
					proOfCrit = 0;
				}
			}
		}

		//蓝量，用于释放技能使用
		public int MP{ get; set;}

		public Player (string name, int attack, int hp) : base(name, attack, hp){
			bag = new Bag ();
			skillList = new List<Skill>();
			Money = 5000;
			proOfCrit = 30;
			MP = 100;
		}

		//吃药
		public void EatMedicine(Medicine m) {
			this.HP += m.HP;
			this.MP += m.MP;
			bag.UseMedicine(m);
		}

		//买装备
		public bool BuyEquipment(Equipment e){
			if(this.Money < e.Price){
				Console.WriteLine ("金钱不够，无法购买！");
				return false;
			}
			//添加装备的背包，添加失败就结束购买
			if (!bag.AddEquipment (e)) {
				return false;
			}
			//判断装备的属性
			switch (e.Type) {
			case EquipmentType.WEAPON:
				Weapon w = e as Weapon;
				if (w != null) {
					this.Money -= w.Price;
					this.Attack += w.Attack;
					this.ProOfCrit += w.ProOfCrit;
					Console.WriteLine ("购买" + e.Name + "成功");
					return true;
				} else {
					Console.WriteLine ("未知属性的装备！");
					return false;
				}
			
			case EquipmentType.CLOTHES:
				Clothes c = e as Clothes;
				if (c != null) {
					this.Money -= c.Price;
					this.HP += c.HP;
					Console.WriteLine ("购买" + e.Name + "成功");
					return true;
				} else {
					Console.WriteLine ("未知属性的装备！");
					return false;
					}

			case EquipmentType.DECORATE:
				Decorate d = e as Decorate;
				if (d != null) {
					this.Money -= d.Price;
					this.HP += d.HP;
					this.MP += d.MP;
					Console.WriteLine ("购买" + e.Name + "成功");
					return true;
				} else {
					Console.WriteLine ("未知属性的装备！");
					return false;
					}
			
			case EquipmentType.MEDICINE:
				Medicine m = e as Medicine;
				if (m != null) {
					this.Money -= m.Price;
					Console.WriteLine ("购买" + e.Name + "成功");
					return true;
				} else {
					Console.WriteLine ("未知属性的装备！");
					return false;
					}
			
			default:
				Console.WriteLine ("未知属性的装备！");
				return false;
			
			}
		}



//		//卖装备，以原价格的50%出售
//		public bool SellEquipmentByID(int id){
//			if (!bag.RemoveEquipmentById (id)) {
//				Console.WriteLine ("装备出售失败");
//				return false;
//			}
//			Equipment e = bag.GetEquipmentByID (id);
//			//改变自身属性
//			this.Money += e.Price/2;
//			this.Attack -= e.Attack;
//			this.HP -= e.Attack;
//			this.ProOfCrit -= e.ProOfCrit;
//			Console.WriteLine ("出售" + e.Name + "成功");
//			eqList.Remove (e);
//			return true;
//		}

		//展示背包
		public void ShowBag(int tag){
			switch (tag) {
			//ID
			case 1:
				bag.SortByID ();
				break;

				//Rank
			case 2:
				bag.SortByRank ();
				break;

				//Price
			case 3:
				bag.SortByPrice ();
				break;

				//Attack
			case 4:
				bag.SortByAttack ();
				break;

				//HP
			case 5:
				bag.SortByHP ();
				break;

				//MP
			case 6:
				bag.SortByMP ();
				break;

				//ProOfCrit
			case 7:
				bag.SortByProOfCrit ();
				break;

			default:
				Console.WriteLine ("装备展示失败");
				break;
			}
		}


		//响应Monster的攻击指令
		public override void F_Attack (object o, EventArgs e){
			Thread.Sleep(300);
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
		public override string ToString ()
		{
			return string.Format ("{0}---攻击力：{1}, 血量：{2}, 蓝量：{3}：当前暴击率：{4}%, 金钱：{5}金币\n\n", Name, Attack, HP, MP, ProOfCrit, Money);
		}
	}
}

