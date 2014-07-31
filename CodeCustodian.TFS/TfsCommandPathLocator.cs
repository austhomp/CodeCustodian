namespace CodeCustodian.TFS
{
    using System;

    public class TfsCommandPathLocator : ITfsCommandPathLocator
    {
        public string GetTfExeLocation()
        {
            const string RelativePathToExe = @"..\IDE\tf.exe";
            const string Vs120Comntools = "VS120COMNTOOLS";
            string exeLocation = CheckTfExeLocation(Vs120Comntools, RelativePathToExe);

            if (string.IsNullOrWhiteSpace(exeLocation))
            {
                const string Vs110Comntools = "VS110COMNTOOLS";
                exeLocation = CheckTfExeLocation(Vs110Comntools, RelativePathToExe);
            }

            return exeLocation;
        }

        private static string CheckTfExeLocation(string toolsPathEnvironmentVariable, string relativePathToExe)
        {
            var installedDirectory = Environment.GetEnvironmentVariable(toolsPathEnvironmentVariable);
            if (!string.IsNullOrEmpty(installedDirectory))
            {
                return System.IO.Path.Combine(installedDirectory, relativePathToExe);
            }

            return string.Empty;
        }

        public string GetTfptExeLocation()
        {
            return string.Empty;
        }
    }
}