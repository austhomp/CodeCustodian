namespace CodeCustodian.Core
{
    using UpdateControls.Fields;

    public class CodeRepositoryItem
    {
        private readonly Independent<string> name = new Independent<string>();

        private readonly Independent<string> status = new Independent<string>();

        private readonly Independent<string> location = new Independent<string>();

        private readonly Independent<string> type = new Independent<string>();

        public CodeRepositoryItem(string name, string type, string location, string status)
        {
            this.Name = name;
            this.Type = type;
            this.Location = location;
            this.Status = status;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name.Value = value;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status.Value = value;
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location.Value = value;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type.Value = value;
            }
        }
    }
}