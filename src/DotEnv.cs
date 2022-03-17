using System;
using System.IO;

namespace MonsterLibrary
{
    public static class DotEnv
    {
        public static void ReadEnv(string envName)
        {
            var root = Directory.GetCurrentDirectory();
            var envPath = Path.Combine(root, envName);

            FileExists(envPath);

            var lines = File.ReadAllLines(envPath);

            for (var i = 0; i < lines.Length; i++)
            {
                ParseLine(lines[i]);

            }
        }

        private static void ParseLine(string line)
        {
            if (!IsLineComment(line))
            {
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    var key = parts[0];
                    var value = parts[1];

                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }

        private static Boolean IsLineComment(string line)
        {
            return line.StartsWith("#");
        }
        private static void FileExists(string envPath)
        {
            if (!File.Exists(envPath))
            {
                throw new FileNotFoundException($"no .env file at specified path: {envPath}");
            }
        }
    }
}