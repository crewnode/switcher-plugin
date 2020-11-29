using System;
using System.IO;
using CrewNodeSwitcherPlugin.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CrewNodeSwitcherPlugin.Utils
{
    static class Config
    {
        /// <summary>
        ///     A logger that works seamlessly with the server.
        /// </summary>
        private static ILogger<CrewNodeSwitcherPlugin> _logger;
        private static ConfigModel _config;

        public static bool Initialize(ILogger<CrewNodeSwitcherPlugin> logger)
        {
            // Setup logger
            _logger = logger;

            // Check configuration file
            string configFilePath = getConfigFilePath();
            if (configFilePath == null)
                return false;

            // Load configuration file
            _config = getConfiguration(configFilePath);
            return true;
        }

        private static ConfigModel getConfiguration(string path)
        {
            if (_config != null) return _config;
            return _config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(path));
        }

        private static string getConfigFilePath()
        {
            string executionPath = Directory.GetCurrentDirectory();
            string pluginPath = Path.Join(executionPath, "plugins");
            string configFileName = Path.Join(pluginPath, "crewnode-switcher.config.json");

            if (File.Exists(configFileName))
                return configFileName;

            if (Directory.Exists(pluginPath))
            {
                try
                {
                    // Create a new configuration file
                    string exampleConfig = Properties.Resources.configJson;
                    ConfigModel modelConfig = JsonConvert.DeserializeObject<ConfigModel>(exampleConfig);

                    // Write new file
                    using (var fileStream = new FileStream(configFileName, FileMode.OpenOrCreate))
                    using (var streamWriter = new StreamWriter(fileStream))
                        streamWriter.Write(exampleConfig);

                    // First Time Use
                    _logger.LogInformation("A new configuration file in your 'plugins/' directory has been made for 'crewnode-switcher.config.json'. Please populate it with your server details.");

                    // Return path
                    return configFileName;
                }
                catch
                {
                    throw new Exception("Unable to generate a new CrewNode Switcher configuration file.");
                }
            }

            return null;
        }

        public static ConfigModel Get()
        {
            return _config;
        }
    }
}
