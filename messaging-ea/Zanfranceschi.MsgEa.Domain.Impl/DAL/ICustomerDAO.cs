namespace Zanfranceschi.MsgEa.Domain.Impl.DAL
{
	using Zanfranceschi.MsgEa.Model;

	public interface ICustomerDAO
	{
		void Add(Customer customer);
		void Update(Customer customer);
		void Delete(string customerId);
		Customer[] GetAll();
	}
}
