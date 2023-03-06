using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.MyCsharp
{
	public static class MyLinq
	{
		public static void Main(){
			var animals = new[]{
				new Animal( "A002", "Tiger"),
				new Animal("A000", "Mouse"),
				new Animal( "A001", "Cow"),
				new Animal( "A003", "Rabbit"),
			};
			WriteLines(animals);

			var sortedAnimals = animals.OrderByDescending(animal => animal.Id).ToArray();
			WriteLines(sortedAnimals);
		}

		private static void WriteLines(Animal[] animals){
			foreach (var animal in animals)
			{
				Console.WriteLine("{0}: {1}", animal.Id, animal.Name);
			}
			Console.WriteLine("");
		}
	
		public class Animal{
			public Animal(string id, string name){
				this.Id = id;
				this.Name = name;
			}
			public string Id { get; }
			public string Name { get; }
		}
	}
}
