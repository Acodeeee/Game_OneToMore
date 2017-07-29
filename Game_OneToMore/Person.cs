using System;

namespace Game_OneToMore
{
	//抽象Person类，用于Player和Monster的公共属性
	public abstract class Person
	{
		public string Name{ get; set;}
		public int Attack{ get; set;}
		public int HP{ get; set;}

		public event EventHandler E_Attack;

		protected Person (string name, int attack, int hp)
		{
			Name = name;
			Attack = attack;
			HP = hp;
		}

		//判断是否死亡
		public bool isDead(){
			return HP <= 0;
		}

		//执行E_Attack
		public void StartAttact(Skill s){
			if (E_Attack != null) {
				E_Attack (this, s);
			}
		}

		//响应事件的方法
		public abstract void F_Attack (object o, EventArgs e);

	}


}

