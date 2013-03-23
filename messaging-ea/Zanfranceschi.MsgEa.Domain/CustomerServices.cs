using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zanfranceschi.MsgEa.Model;
using Zanfranceschi.MsgEa.Domain.DAL;

namespace Zanfranceschi.MsgEa.Domain
{
	public class CustomerServices
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
	}
}