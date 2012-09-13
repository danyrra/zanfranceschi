using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Domain.Context;
using Tutorials.Core.UI.Boundary.Controllers;
using Db4objects.Db4o;
using System.Linq.Expressions;
using Db4objects.Db4o.Config;


namespace Tutorials.GUI._Console
{
	class A
	{
		public string Id { get; set; }
		public int X { get; set; }
		public IList<B> Bs { get; set; }
		
		public override bool Equals(object obj)
		{
			A c = (A)obj;
			return this.Id.Equals(c.Id);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}

	class B
	{
		public int Y { get; set; }
	}
	
	class Program
	{
		static void Test()
		{
			// Salva
			A a = new A { Id = "A" };
			using (var db = Db4oFactory.OpenFile(@"C:\tmp\base2.yap"))
			{
				db.Store(a);
			}

			// recupera
			A apersisted = null;
			using (var db = Db4oFactory.OpenFile(@"C:\tmp\base2.yap"))
			{
				apersisted = db.Query<A>(delegate(A obj) { return obj.Id == "A"; }).First();
			}

			// atualiza
			apersisted.X = 99;
			using (var db = Db4oFactory.OpenFile(@"C:\tmp\base2.yap"))
			{
				db.Store(apersisted);
			}

			// verifica se na atualização, criou um novo ou atualizou de fato
			using (var db = Db4oFactory.OpenFile(@"C:\tmp\base2.yap"))
			{
				var result = db.Query<A>(delegate(A obj) { return obj.Id == "A"; });
			}
		}
		
		static void Main(string[] args)
		{

			Context[] ctxs = ContextController.GetAllContexts();

			if (ctxs.Count() < 1)
			{
				ContextController.CreateNewContext("", "Título", "Descrição", 0);
				ctxs = ContextController.GetAllContexts();
			}

			foreach (var ctx in ctxs)
			{
				for (int i = 0; i < 10; i++)
				{
					ContextController.CreateNewTopic(ctx.Key.ToString(), "Título " + i.ToString(), "Descrição " + i.ToString(), i);
				}
				Console.WriteLine("{0} - {1}", ctx.Key, ctx.Title);
			}
			ctxs = ContextController.GetAllContexts();

			Console.Read();
		}
	}
}