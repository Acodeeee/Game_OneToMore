using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Game_OneToMore
{
	public static class AnalyseXml
	{
		//读取Weapon.xml
		public static List<Weapon> GetWeaponList(){
			List<Weapon> weaponList = new List<Weapon> ();

			//得到Weapon节点
			IEnumerable<XElement> xWeaponList = XDocument.Load ("Weapon.xml").Element ("Root").Elements ("Weapon");

			foreach (var item in xWeaponList) {

				XElement xName = item.Element ("Name");
				XElement xPrice = item.Element ("Price");
				XElement xAttack = item.Element ("Attack");
				XElement xProOfCrit = item.Element ("ProOfCrit");

				int price, attack, proOfCrit;
				try{
					price = int.Parse(xPrice.Value);
					attack = int.Parse(xAttack.Value);
					proOfCrit = int.Parse(xProOfCrit.Value);
				}catch{
					price = attack = proOfCrit = 0;
					Console.WriteLine ("Weapon.xml格式有错!");
				}

				weaponList.Add (new Weapon (EquipmentType.WEAPON, xName.Value, price, attack, proOfCrit));
			}

			return weaponList;

		}

		//读取Clothes.xml
		public static List<Clothes> GetClothesList(){
			List<Clothes> clothesList = new List<Clothes> ();

			//得到Clother节点
			IEnumerable<XElement> xClothesList = XDocument.Load ("Clothes.xml").Element ("Root").Elements ("Clothes");

			foreach (var item in xClothesList) {
				string name;
				int price, hp;
				name = item.Element ("Name").Value;
				try{
					price = int.Parse(item.Element("Price").Value);
					hp = int.Parse(item.Element("HP").Value);
				}catch{
					price = hp = 0;
					Console.WriteLine ("Clothes.xml格式有错！");
				}

				clothesList.Add (new Clothes(EquipmentType.CLOTHES, name, price, hp));
			}
			return clothesList;

		}

		//读取Decorate.xml
		public static List<Decorate> GetDecorateList(){
			List<Decorate> decorateList = new List<Decorate> ();

			//得到Clother节点
			IEnumerable<XElement> xDecorateList = XDocument.Load ("Decorate.xml").Element ("Root").Elements ("Decorate");

			foreach (var item in xDecorateList) {
				string name;
				int price, hp, mp;
				name = item.Element ("Name").Value;
				try {
					price = int.Parse (item.Element ("Price").Value);
					hp = int.Parse (item.Element ("HP").Value);
					mp = int.Parse (item.Element ("MP").Value);
				} catch {
					price = hp = mp = 0;
					Console.WriteLine ("Clothes.xml格式有错！");
				}

				decorateList.Add (new Decorate (EquipmentType.DECORATE, name, price, hp, mp));
			}
			return decorateList;
		}
   
		//Medicile.xml
		public static List<Medicine> GetMedicineList(){
			List<Medicine> medicineList = new List<Medicine> ();

			//得到Clother节点
			IEnumerable<XElement> xMedicineList = XDocument.Load ("Medicine.xml").Element ("Root").Elements ("Medicine");

			foreach (var item in xMedicineList) {
				string name;
				int price, hp, mp;
				name = item.Element ("Name").Value;
				try{
					price = int.Parse(item.Element("Price").Value);
					hp = int.Parse(item.Element("HP").Value);
					mp = int.Parse(item.Element("MP").Value);
				}catch{
					price = hp = mp = 0;
					Console.WriteLine ("Clothes.xml格式有错！");
				}

				medicineList.Add (new Medicine(EquipmentType.MEDICINE, name, price, hp, mp));
			}
			return medicineList;

		}


	}
}

