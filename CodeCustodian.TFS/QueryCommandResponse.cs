namespace CodeCustodian.TFS
{
    public class QueryCommandResponse
    {
        public QueryCommandResponse(string result)
        {
            this.Result = result;
        }

        public string Result { get; private set; }
    }
}