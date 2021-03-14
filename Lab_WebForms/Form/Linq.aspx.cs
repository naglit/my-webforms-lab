using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_WebForms.Form
{
	public partial class Linq : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string[] alphabets = { "a", "b", "c", "d", "e" };
			SelectMany();
			//(IEnumerable<string>)alphabets.GetEnumerator().JoinToString("-", (a => (a != "b")));
		}

		protected void SelectMany()
		{
			var petOwners = new []{
				new PetOwner { Name="Higa", Pets = new []{ "Scruffy", "Sam" } },
				new PetOwner { Name="Ashkenazi", Pets = new []{ "Walker", "Sugar" } },
				new PetOwner { Name="Price", Pets = new []{ "Scratches", "Diesel" } },
				new PetOwner { Name="Hines", Pets = new []{ "Dusty" } } };
			// Project the pet owner's name and the pet's name.
			var query = petOwners
				.SelectMany(petOwner => petOwner.Pets);

			// Print the results.
			foreach (var obj in query)
			{
				Console.WriteLine(obj);
			}
		}
	}
	class PetOwner
	{
		public string Name { get; set; }
		public string[] Pets { get; set; }
	}
}