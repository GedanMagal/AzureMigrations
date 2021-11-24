namespace AzureApp.Models.Requests
{
    public class CreateVariableGroup
    {
        
        public Variables Variables { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsShared { get; set; }
        public object VariableGroupProjectReferences { get; set; }
        
    }
    
    public class TESTEVARIAVEISAPI
    {
        public string Value { get; set; }
    }

    public class Variables
    {
        public dynamic VariableName { get; set; }
    }
}