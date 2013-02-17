namespace CodeCustodian.Core
{
    public class CodeRepositoryItem
    {
        public CodeRepositoryItem(string name, string status, string type)
        {
            this.Name = name;
            this.Status = status;
            this.Type = type;
        }

        public string Name { get; private set; } 
        
        public string Status { get; private set; } 
        
        public string Type { get; private set; } 
    }
}