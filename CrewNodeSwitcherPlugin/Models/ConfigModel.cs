namespace CrewNodeSwitcherPlugin.Models
{
    class ConfigModel
    {
        public string apiKey { get; set; }
        public string serverName { get; set; }
        public string serverDescription { get; set; }
        public int maxPlayers { get; set; }

        public ConfigModel(string apiKey, string serverName, string serverDescription, int maxPlayers)
        {
            this.apiKey = apiKey;
            this.serverName = serverName;
            this.serverDescription = serverDescription;
            this.maxPlayers = maxPlayers;
        }
    }
}
