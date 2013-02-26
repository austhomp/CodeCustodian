namespace CodeCustodian.TFS
{
    public class TfsCommandResult
    {
        public TfsCommandResult(string exitCode, string output)
        {
            this.ExitCode = exitCode;
            this.Output = output;
        }

        public string ExitCode { get; private set; }

        public string Output { get; private set; }
    }
}