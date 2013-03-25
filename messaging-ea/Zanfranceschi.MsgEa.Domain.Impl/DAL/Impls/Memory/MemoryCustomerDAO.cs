namespace Zanfranceschi.MsgEa.Domain.Impls.DAL.Impls.Memory
{
	using System.Collections.Generic;
	using System.Linq;
	using Zanfranceschi.MsgEa.Domain.Impl.DAL;
	using Zanfranceschi.MsgEa.Model;
	
	public class MemoryCustomerDAO
		: ICustomerDAO
	{
		private IList<Customer> customers;

		public MemoryCustomerDAO()
		{
			customers = new List<Customer>();
		}

		public void Add(Customer customer)
		{
			customers.Add(customer);
		}

		public void Update(Customer customer)
		{
			var oldCustumer = (from c in customers where c.Id == customer.Id select c).First();
			customers.Remove(oldCustumer);
			customers.Add(customer);
		}

		public void Delete(string customerId)
		{
			var memCustumer = (from c in customers where c.Id == customerId select c).First();
			customers.Remove(memCustumer);
		}

		public Customer[] GetAll()
		{
			return customers.ToArray();
		}
	}
}
