namespace CodeCustodian.Core
{
    public class CodeRepositoryItem
    {
        public CodeRepositoryItem(string name, string type, string location, string status)
        {
            this.Name = name;
            this.Type = type;
            this.Location = location;
            this.Status = status;
        }

        public string Name { get; private set; } 
        
        public string Location { get; private set; } 
        
        public string Status { get; set; } 
        
        public string Type { get; private set; } 
    }
}