namespace CodeCustodian.TFS
{
    public class TfsCommandResult
    {
        public TfsCommandResult(ExitCodes exitCode, string output)
            : this((int)exitCode, output)
        {
        }

        public TfsCommandResult(int exitCode, string output)
        {
            this.ExitCode = exitCode;
            this.Output = output;
        }

        public int ExitCode { get; private set; }

        public string Output { get; private set; }
    }
}