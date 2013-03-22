namespace CodeCustodian.TFS
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class TfsCommand : ITfsCommand
    {
        private readonly string arguments;

        private readonly string workingDirectory;

        private readonly string programPath;

        public TfsCommand(string programPath, string arguments, string workingDirectory = null)
        {
            if (programPath == null)
            {
                throw new ArgumentNullException("programPath");
            }

            this.programPath = programPath;
            this.arguments = arguments;
            this.workingDirectory = workingDirectory;
        }

        public TfsCommandResult Run()
        {
            ////var path = string.Format("\"{0}\"", this.programPath);
            var path = string.Format("\"{0}\"", this.programPath);
            var startInfo = new ProcessStartInfo(path);
            startInfo.Arguments = this.arguments;
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            ////startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            
            if (this.workingDirectory != null)
            {
                startInfo.WorkingDirectory = this.workingDirectory;
            }

            var process = new Process() { StartInfo = startInfo, EnableRaisingEvents = true};
            try
            {
                process.Start();
                process.WaitForExit();
                return new TfsCommandResult(process.ExitCode, process.StandardOutput.ReadToEnd());
            }
            catch (Exception e)
            {
                // todo: really log this
                string message = string.Format("Error running TfsCommand in the shell. Command \"{0}\" arguments \"{1}\"", path, arguments);
                Debug.WriteLine(message, e);
                return new TfsCommandResult(ExitCodes.Fail, string.Empty);
            }
        }
    }
}