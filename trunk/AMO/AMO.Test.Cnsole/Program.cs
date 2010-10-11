using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AMO.Test.Cnsole
{
	class Program
	{
		static void Main(string[] args)
		{
			IList<User> users = new List<User>() {
				new User() { Id = 1, Name = "A" },
				new User() { Id = 2, Name = "A" },
				new User() { Id = 3, Name = "B" },
				new User() { Id = 4, Name = "C" },
				new User() { Id = 5, Name = "D" },
				new User() { Id = 6, Name = "D" }
			};

			var list = from u in users group u by u.Name into g select new { Value = g };

			foreach (var item in list)
			{
				Console.WriteLine(item.Value.Key);
			}

			Console.Read();
		}
	}

	class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
