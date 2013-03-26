namespace Zanfranceschi.MsgEa.Domain.Impls.Services
{

	using System;
	using System.Linq;
	using Zanfranceschi.MsgEa.Domain.Impl.DAL;
	using Zanfranceschi.MsgEa.Domain.Services;
	using Zanfranceschi.MsgEa.Model;
	using System.Net;
	using System.IO;
	using System.Data;
	
	public class CustomerServices
		: ICustomerServices
	{
		private ICustomerDAO dao;

		public CustomerServices(ICustomerDAO customerDAO)
		{
			dao = customerDAO;
		}
		
		public Customer RegisterCustomer(User user, string customerName, out Message message)
		{
			var customer = new Customer(customerName);
			dao.Add(customer);
			message = new Message(string.Format("Customer registered ({0}).", customer.Id));
			return customer;
		}

		public void UpdateCustomer(User user, string customerId, string newCustomerName, out Message message)
		{
			dao.Update(new Customer(customerId, newCustomerName));
			message = new Message("Customer updated.");
		}

		public void ExcludeCustomer(User user, string customerId, out Message message)
		{
			dao.Delete(customerId);
			message = new Message("Customer excluded.");
		}

		public Customer[] SearchCustomers(User user, string nameLike, out Message message)
		{
			var customers = dao.GetAll();
			
			var result = (
				from c in customers 
				where c.Name.ToLower().Contains(nameLike.ToLower()) 
				select c
				).ToArray();
			message = new Message(string.Format("{0} custumers found.", result.Count()));

			return result;
		}

		public void Dispose()
		{
			
		}
	}
}