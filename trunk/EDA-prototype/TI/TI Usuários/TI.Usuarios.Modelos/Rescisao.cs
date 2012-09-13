using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Db4objects.Db4o.CS;

namespace TI.Usuarios.Modelos
{
    public class Rescisao
    {
        public int Id { get; set; }
        public DateTime DataRescisao { get; set; }
        public string ColaboradorNome { get; set; }
        public string ColaboradorEmpresa { get; set; }
        public string ColaboradorDpto { get; set; }
        public string ColaboradorCentroCusto { get; set; }

        public void Save()
        {
            using (var db = Db4oClientServer.OpenClient("localhost", 8080, "db4o", "123"))
            {
                db.Store(this);
            }
        }

		public static IEnumerable<Rescisao> GetAll()
		{
			using (var db = Db4oClientServer.OpenClient("localhost", 8080, "db4o", "123"))
			{
				var rescisoes = db.Query<Rescisao>()
					.OrderByDescending<Rescisao, int>(
						r => r.Id,
						new RescisaoIdComparer()
						).ToList();

				return rescisoes;
			}
		}

        public static Rescisao GetLastOrDefault()
        {
            using (var db = Db4oClientServer.OpenClient("localhost", 8080, "db4o", "123"))
            {
                var rescisao = db.Query<Rescisao>()
                                .OrderByDescending<Rescisao, int>(
                                    r => r.Id,
                                    new RescisaoIdComparer()
                                    ).FirstOrDefault();
                return rescisao;
            }
        }

        public static IEnumerable<Rescisao> GetManyFromJson(string json)
        {
            object result = JsonConvert.DeserializeObject(json);
            /*
                "fields": 
             *      {
             *      "colaborador_dpto": "Projetos de TI", 
             *      "colaborador_centro_custo": "TI001", 
             *      "data_rescisao": "2012-01-01", 
             *      "colaborador_nome": "Francisco", 
             *      "colaborador_empresa": "Zanfranceschi LTDA"
             *     }
            */

            var objs = ((JContainer)result);

            foreach (var obj in objs)
            {
                yield return new Rescisao()
                {
                    Id = int.Parse(((JContainer)obj)["pk"].ToString()),
                    ColaboradorDpto = ((JContainer)obj)["fields"]["colaborador_dpto"].ToString(),
                    ColaboradorNome = ((JContainer)obj)["fields"]["colaborador_nome"].ToString(),
                    ColaboradorEmpresa = ((JContainer)obj)["fields"]["colaborador_empresa"].ToString(),
                    DataRescisao = DateTime.Parse(((JContainer)obj)["fields"]["data_rescisao"].ToString()),
                    ColaboradorCentroCusto = ((JContainer)obj)["fields"]["colaborador_centro_custo"].ToString(),
                };
            }
        }

        public static Rescisao GetOneFromJson(string json)
        {
            object obj = JsonConvert.DeserializeObject(json);
            /*
                "fields": 
             *      {
             *      "colaborador_dpto": "Projetos de TI", 
             *      "colaborador_centro_custo": "TI001", 
             *      "data_rescisao": "2012-01-01", 
             *      "colaborador_nome": "Francisco", 
             *      "colaborador_empresa": "Zanfranceschi LTDA"
             *     }
            */
            return new Rescisao()
            {
                Id = int.Parse(((JContainer)obj)[0]["pk"].ToString()),
                ColaboradorDpto = ((JContainer)obj)[0]["fields"]["colaborador_dpto"].ToString(),
                ColaboradorNome = ((JContainer)obj)[0]["fields"]["colaborador_nome"].ToString(),
                ColaboradorEmpresa = ((JContainer)obj)[0]["fields"]["colaborador_empresa"].ToString(),
                DataRescisao = DateTime.Parse(((JContainer)obj)[0]["fields"]["data_rescisao"].ToString()),
                ColaboradorCentroCusto = ((JContainer)obj)[0]["fields"]["colaborador_centro_custo"].ToString(),
            };
        }
    }

    internal class RescisaoIdComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}
