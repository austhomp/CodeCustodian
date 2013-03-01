namespace CodeCustodian.TFS
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class TfsCommand : ITfsCommand
    {
        private readonly string arguments;

        private readonly string workingDirectory;

        public TfsCommand(string arguments, string workingDirectory = null)
        {
            this.arguments = arguments;
            this.workingDirectory = workingDirectory;
        }

        public TfsCommandResult Run()
        {
            var programPath = GetTfExePath();
            if (programPath == null)
            {
                throw new FileNotFoundException("TF.EXE");
            }

            var startInfo = new ProcessStartInfo(programPath, this.arguments);
            if (this.workingDirectory != null)
            {
                startInfo.WorkingDirectory = this.workingDirectory;
            }

            startInfo.CreateNoWindow = true;
            var process = new Process() { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
            return new TfsCommandResult(process.ExitCode, process.StandardOutput.ReadToEnd());
        }

        private string GetTfExePath()
        {
            string path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft Visual Studio 11.0\Common7\IDE", "tf.exe");
            return File.Exists(path) ? path : null;
        }
    }
}