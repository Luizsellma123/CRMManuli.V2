using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Text.Json;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class JsonConversao
    {
        public string ConverteObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch (Exception ex)
            {

                string erro = ex.Message;
                throw;
            }
        }

        public T ConverteJSonParaObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        public string CorrigeEstruraJsonRetornoAPITranSanches(string jsonErrado)
        {
            // Converte o JSON string para uma lista de objetos
            var list = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonErrado);

            // Cria uma nova lista para armazenar os objetos "cte"
            var newCteList = new List<JsonElement>();

            foreach (var item in list)
            {
                if (item.ContainsKey("cte"))
                {
                    newCteList.Add(item["cte"]);
                }
            }

            // Cria um novo dicionário com a estrutura desejada
            var newJsonObject = new Dictionary<string, List<JsonElement>>
            {
                { "cte", newCteList }
            };

            // Converte o novo objeto para JSON string
            string newJson = JsonSerializer.Serialize(newJsonObject, new JsonSerializerOptions { WriteIndented = true });

            return newJson;
        }

    }
}