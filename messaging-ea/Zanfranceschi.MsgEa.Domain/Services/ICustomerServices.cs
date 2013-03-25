namespace Zanfranceschi.MsgEa.Domain.Services
{
	using Zanfranceschi.MsgEa.Model;
	using System;

	public interface ICustomerServices 
		: IDisposable
	{
		void ExcludeCustomer(User user, string customerId, out Message message);
		Customer RegisterCustomer(User user, string customerName, out Message message);
		Customer[] SearchCustomers(User user, string nameLike, out Message message);
		void UpdateCustomer(User user, string customerId, string newCustomerName, out Message message);
	}
}