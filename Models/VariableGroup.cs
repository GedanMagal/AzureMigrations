using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace AzureApp
{
    public class VariableGroup
    {
        public int Count { get; set; }
        public List<Value> Value { get; set; }
    }

    public class Key1
    {
        public string Value { get; set; }
    }

    public class Variables
    {
        [JsonProperty(PropertyName = "Foo")]
        public ExpandoObject ExpandoObject { get; set; }
    }

    public class CreatedBy
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ModifiedBy
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Value
    {
        public Variables Variables { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CreatedBy CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public ModifiedBy ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}