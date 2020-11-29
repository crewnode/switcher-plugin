using System.Threading.Tasks;
using CrewNodeSwitcherPlugin.Utils;
using Impostor.Api.Events.Managers;
using Impostor.Api.Plugins;
using Microsoft.Extensions.Logging;

namespace CrewNodeSwitcherPlugin
{
    /// <summary>
    ///     The metadata information of your plugin, this is required.
    /// </summary>
    [ImpostorPlugin(
        package: "crewnode.switcher",
        name: "CrewNode Switcher",
        author: "CrewNode",
        version: "1.0.0")]
    public class CrewNodeSwitcherPlugin : PluginBase
    {
        /// <summary>
        ///     A logger that works seamlessly with the server.
        /// </summary>
        private readonly ILogger<CrewNodeSwitcherPlugin> _logger;

        /// <summary>
        ///     The constructor of the plugin. There are a few parameters you can add here and they
        ///     will be injected automatically by the server, two examples are used here.
        ///
        ///     They are not necessary but very recommended.
        /// </summary>
        /// <param name="logger">
        ///     A logger to write messages in the console.
        /// </param>
        /// <param name="eventManager">
        ///     An event manager to register event listeners.
        ///     Useful if you want your plugin to interact with the game.
        /// </param>
        public CrewNodeSwitcherPlugin(ILogger<CrewNodeSwitcherPlugin> logger, IEventManager eventManager)
        {
            _logger = logger;
        }

        /// <summary>
        ///     This is called when your plugin is enabled by the server.
        /// </summary>
        /// <returns></returns>
        public override ValueTask EnableAsync()
        {
            if (!Config.Initialize(_logger) || !API.Connect())
                return this.DisableAsync();

            _logger.LogInformation("CrewNodeSwitcherPlugin is being enabled.");
            return default;
        }

        /// <summary>
        ///     This is called when your plugin is disabled by the server.
        ///     Most likely because it is shutting down, this is the place to clean up any managed resources.
        /// </summary>
        /// <returns></returns>
        public override ValueTask DisableAsync()
        {
            _logger.LogInformation("CrewNodeSwitcherPlugin is being disabled.");
            return default;
        }
    }
}
