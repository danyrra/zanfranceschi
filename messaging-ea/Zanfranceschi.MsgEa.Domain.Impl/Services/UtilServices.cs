namespace Zanfranceschi.MsgEa.Domain.Impl.Services
{
	using System.Data;
	using System.IO;
	using System.Net;
	using Zanfranceschi.MsgEa.Model;
	using Zanfranceschi.MsgEa.Domain.Services;
	
	public class UtilServices
		: IUtilServices
	{
		public Address GetAddressByCEP(User user, string CEP, out Message message)
		{
			WebProxy proxy = new WebProxy("http://proxy.bandeirantes.com.br:3128");
			proxy.Credentials = new NetworkCredential("fzanfranceschi", "frazan*1088");
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://cep.republicavirtual.com.br/web_cep.php?cep=" + CEP.Replace("-", "").Trim() + "&formato=xml");
			request.Method = "POST";
			request.Proxy = proxy;

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			Stream stream = response.GetResponseStream();

			StreamReader reader = new StreamReader(stream);

			DataSet ds = new DataSet();
			ds.ReadXml(reader);

			Address address = null;

			message = null;

			if (ds != null)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					address = new Address();
					string _resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
					message = new Message("Address found.");
					switch (_resultado)
					{
						case "1":
							address.Region = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
							address.City = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
							address.District = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
							address.AddressType = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
							address.AddressDescription = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
							break;
						case "2":
							address.Region = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
							address.City = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
							break;
						default:
							address = null;
							message = new Message("No address found. Sorry.");
							break;
					}
				}
			}
			return address;
		}

		private static Stream CopyAndClose(Stream inputStream)
		{
			const int readSize = 256;
			byte[] buffer = new byte[readSize];
			MemoryStream ms = new MemoryStream();

			int count = inputStream.Read(buffer, 0, readSize);
			while (count > 0)
			{
				ms.Write(buffer, 0, count);
				count = inputStream.Read(buffer, 0, readSize);
			}
			ms.Position = 0;
			inputStream.Close();
			return ms;
		}

		public void Dispose()
		{
			
		}
	}
}
