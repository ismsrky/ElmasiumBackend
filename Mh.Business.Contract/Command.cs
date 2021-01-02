using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Mh.Business.Contract
{
    public class Command
    {
        public Guid TokenId { get; set; }

        public JObject Data { get; set; }


        public string Response { get; set; }
        public string ToJson()
        {
            string result = "";

            var data = new { Data = Data, TokenId = TokenId };

            result = JsonConvert.SerializeObject(data);


            return result;
        }

        public static Command ToCommand(string jsonData)
        {
            Command command = JsonConvert.DeserializeObject<Command>(jsonData);

            return command;
        }
    }
}