namespace CodeCustodian.TFS
{
    using System;

    public class TfsCommandPathLocator : ITfsCommandPathLocator
    {
        public string GetTfExeLocation()
        {
            var installedDirectory = Environment.GetEnvironmentVariable("VS110COMNTOOLS");
            if (!string.IsNullOrEmpty(installedDirectory))
            {
                return System.IO.Path.Combine(installedDirectory, @"..\IDE\tf.exe");
            }

            return string.Empty;
        }

        public string GetTfptExeLocation()
        {
            return string.Empty;
        }
    }
}