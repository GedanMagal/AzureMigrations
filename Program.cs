using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;

namespace AzureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetBuilds("", "");
        }

        public static async void GetBuilds(string organization, string project)
        {
            try
            {
                var personalaccesstoken = "";

                RestClient restClient = new RestClient($"https://dev.azure.com/");
                restClient.Authenticator = new HttpBasicAuthenticator($"{personalaccesstoken}", "");
                
                var request = new RestRequest($"{organization}/{project}/_apis/distributedtask/variablegroups?api-version=6.0-preview.2", DataFormat.Json);
                var response = restClient.Get(request);
                dynamic testeObject = JsonConvert.DeserializeObject<ExpandoObject>(response.Content);

                foreach (var atributo in testeObject.value)
                {

                    foreach (var propriedade in atributo)
                    {

                        if ("variables".Equals(propriedade.Key))
                        {
                            foreach (var variableName in propriedade.Value)
                            {

                                foreach (var propertyName in variableName.Value)
                                {

                                    if ("value".Equals(propertyName.Key))
                                    {
                                        Console.WriteLine($"Chave:{variableName.Key}, Valor:{propertyName.Value}");
                                    }
                                }
                            }
                        }
                        Console.WriteLine(propriedade.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
