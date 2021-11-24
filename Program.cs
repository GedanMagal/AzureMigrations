using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace AzureApp
{
    class Program
    {
        private const string personalAccessToken = "";
        
        static void Main(string[] args)
        {
            CreateVariableGroup("teste", "teste");
            // var variableGroupsSource = GetGroupsAndVariables("", "");
            // var variableGroupsDestiny = GetGroupsAndVariables("", "");
        }

        public static void CreateVariableGroup(string organization, string project)
        {
            
            var restClient = new RestClient($"https://dev.azure.com/");
            restClient.Authenticator = new HttpBasicAuthenticator($"{personalAccessToken}", "");

            var request =
                new RestRequest(
                    $"{organization}/{project}/_apis/distributedtask/variablegroups?api-version=5.1-preview.1");

            var teste = new
            {
                variables = new
                {
                    variavelNome = "valorVariavel"
                }
            };

            JObject testeJson = JObject.FromObject(teste);
            request.AddJsonBody(testeJson);

            var response = restClient.Post(request);
            
        }

        private static IEnumerable<VariableGroup> GetGroupsAndVariables(string organization, string project)
        {
            try
            {
                var restClient = new RestClient($"https://dev.azure.com/");
                restClient.Authenticator = new HttpBasicAuthenticator($"{personalAccessToken}", "");

                var request =
                    new RestRequest(
                        $"{organization}/{project}/_apis/distributedtask/variablegroups?api-version=6.0-preview.2",
                        DataFormat.Json);
                var response = restClient.Get(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.Content);
                }

                dynamic expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(response.Content);

                var variableGroupList = new List<VariableGroup>();

                foreach (var groupValues in expandoObject.value)
                {
                    VariableGroup variableGroup = null;

                    foreach (var buildGroup in groupValues)
                    {
                        if (!"name".Equals(buildGroup.Key)) continue;

                        variableGroup = new VariableGroup();
                        variableGroup.GroupName = buildGroup.Value;
                        variableGroup.Variables = new List<KeyValuePair<string, string>>();
                        Console.WriteLine($" GRUPO:{buildGroup.Key}, Valor:{buildGroup.Value}");
                    }

                    if (variableGroup == null)
                    {
                        continue;
                    }

                    GetVariablesByGroup(groupValues, variableGroup);

                    variableGroupList.Add(variableGroup);
                }

                return variableGroupList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private static void GetVariablesByGroup(dynamic groupValues, VariableGroup variableGroup)
        {
            foreach (var buildVariables in groupValues)
            {
                if (!"variables".Equals(buildVariables.Key)) continue;

                foreach (var variableName in buildVariables.Value)
                {
                    foreach (var propertyName in variableName.Value)
                    {
                        if (!"value".Equals(propertyName.Key)) continue;

                        variableGroup.Variables.Add(
                            new KeyValuePair<string, string>(variableName.Key, propertyName.Value));
                        Console.WriteLine($"Chave:{variableName.Key}, Valor:{propertyName.Value}");
                    }
                }
            }
        }
    }
}