namespace Web.Utils
{
    /// <summary>
    /// Utility class for loading env file
    /// </summary>
    public static class EnvLoader
    {
        private static string? rootPath;

        private static bool IsEnvFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.Equals(".env", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Load env file and insert to Environment class
        /// <para>If file not found or not .env, this class will does nothing</para>
        /// </summary>
        /// <param name="filePath">Path to env file start from root project</param>
        /// <exception cref="NullReferenceException"></exception>
        public static void Load(string filePath)
        {
            if (rootPath == null)
            {
                throw new NullReferenceException("Set root path first");
            }

            string file = Path.Combine(rootPath, filePath);

            if (!File.Exists(file)) return;
            if (!IsEnvFile(file)) return;

            InsertEnvironmentVariable(file);
        }

        private static void InsertEnvironmentVariable(string file)
        {
            foreach (string line in File.ReadAllLines(file))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                {
                    continue;
                }

                // Skip lines that are not key-value pairs
                var parts = line.Split('=', 2);
                if (parts.Length != 2) continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();
                Environment.SetEnvironmentVariable(key, value);
            }
        }

        public static void SetEnvRootPath(this WebApplicationBuilder builder)
        {
            var webRoot = builder.Environment.WebRootPath;
            string projRoot = Path.GetFullPath(Path.Combine(webRoot, "../../"));

            rootPath = projRoot;
        }
    }
}
