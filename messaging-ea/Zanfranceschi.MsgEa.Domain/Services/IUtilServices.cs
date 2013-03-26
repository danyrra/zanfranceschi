namespace Zanfranceschi.MsgEa.Domain.Services
{
	using Zanfranceschi.MsgEa.Model;

	public interface IUtilServices
		: IServices
	{
		Address GetAddressByCEP(User user, string CEP, out Message message);
	}
}
