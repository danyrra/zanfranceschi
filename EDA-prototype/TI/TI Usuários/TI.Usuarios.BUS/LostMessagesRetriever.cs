using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Db4objects.Db4o.CS;
using TI.Usuarios.Modelos;
using System.Threading;

namespace TI.Usuarios.BUS
{
    internal static class LostMessagesRetriever
    {
        internal static void Start()
        {
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < 100; i++)
                {
                    bool retrieved = false;
                    try
                    {
						Console.WriteLine("Obtendo última rescisão...");
						// recupera o último id
                        var ultima_rescisao = Rescisao.GetLastOrDefault();
                        int last_id = ultima_rescisao != null ? ultima_rescisao.Id : 0;

						Console.WriteLine("Última rescisão possui id ", last_id);

						Console.WriteLine("Contatando servidor...");
						string json = client.DownloadString(string.Format("http://127.0.0.1:8000/rescisoes/listar/mais-recentes-depois-de-id/{0}/", last_id));
                        var rescisoes = Rescisao.GetManyFromJson(json);

						if (rescisoes.Count() > 0)
						{
							Console.WriteLine("Recuperando rescisões perdidas...");
							
							foreach (var rescisao in rescisoes)
							{
								Console.WriteLine("Rescisão {0} ({1})", rescisao.Id, rescisao.ColaboradorNome);
								rescisao.Save();
							}
						}
						else
						{
							Console.WriteLine("Não há rescisões perdidas...");
						}

                        retrieved = true;
                    }
                    catch
                    {
                        Console.WriteLine("Não foi possível recuperar as rescisões perdidas...");
                    }
                    if (retrieved)
                    {
                        break;
                    }

					Thread.Sleep(2000);
                }
            }
        }
    }
}
