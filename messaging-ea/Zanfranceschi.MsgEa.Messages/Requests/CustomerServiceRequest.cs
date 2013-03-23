namespace Zanfranceschi.MsgEa.Messages.Requests
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public enum CustomerOperationTypeRequest
	{
		Register,
		Update,
		Delete,
		Search
	}

	[Serializable]
	public class CustomerServiceRequest
		: Request
	{
		public CustomerOperationTypeRequest OperationType { get; private set; }

		public string CustomerId { get; private set; }
		public string CustomerName { get; private set; }
		public string NameLike { get; private set; }

		public CustomerServiceRequest(
			User requestor,
			CustomerOperationTypeRequest operationType,
			string customerId,
			string customerName,
			string nameLike) : base(requestor)
		{
			OperationType = operationType;
			CustomerId = customerId;
			CustomerName = customerName;
			NameLike = nameLike;
		}
	}
}