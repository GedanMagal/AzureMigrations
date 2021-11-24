using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace AzureApp
{
    public class VariableGroup
    {
        public string GroupName { get; set; }
        
        public List<KeyValuePair<string, string>> Variables { get; set; }
    }
}