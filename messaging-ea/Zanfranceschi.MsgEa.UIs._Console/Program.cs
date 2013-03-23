namespace Zanfranceschi.MsgEa.UIs._Console
{
	using System;
	using Zanfranceschi.MsgEa.Messages.Responses;
	using Zanfranceschi.MsgEa.Model;
	
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Client";
			Console.WindowHeight = 20;
			Console.WindowWidth = 60;
			
			ServiceRequest request = new ServiceRequest(new User());
			request.Connect();

			string input = string.Empty;

			while (true)
			{
				Console.WriteLine("Type the new customer name:");
				input = Console.ReadLine();
				
				if (input == "exit")
					break;

				CustomerRegisterServiceResponse response = request.RequestCustomerRegistration(input);
				Console.WriteLine(response.Message.Text);
			}

			while (true)
			{
				Console.WriteLine("Type the customer search term:");
				input = Console.ReadLine();

				if (input == "exit")
					break;

				CustomerSearchServiceResponse response = request.RequestCustomersSearch(input);

				Console.WriteLine(response.Message.Text);

				var customers = response.Customers;

				foreach (var customer in customers)
				{
					Console.WriteLine("{0} - {1}", customer.Id, customer.Name);
				}
			}

			request.Disconnect();
			Console.WriteLine("terminated...");
		}
	}
}
