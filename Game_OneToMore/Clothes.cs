//
//  Clothes.cs
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

namespace Game_OneToMore
{
	public class Clothes : Equipment
	{
		//生命值
		public int HP{ get; set;}

		public Clothes (EquipmentType type, string name, int price, int hp):base(type, name, price){
			HP = hp;
		}

		public override Equipment Clone (int id)
		{
			Clothes newC = new Clothes (this.Type, this.Name, this.Price, this.HP);
			newC.ID = id;
			return newC;
		}

		public override string ToString ()
		{
			return string.Format ("{0}:  ID：{1}  价格：{2}  生命值：{3}  等级{4}", Name, ID, Price.ToString().PadRight(4), HP.ToString().PadRight(4), Rank);
		}
	}
}

