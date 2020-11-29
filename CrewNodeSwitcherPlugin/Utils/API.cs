using Newtonsoft.Json;
using System;
using System.Net;

namespace CrewNodeSwitcherPlugin.Utils
{
    class API
    {
        private const string ApiUrl = "https://crewno.de/app";

        public static bool Connect()
        {
            if (Config.Get() == null) return false;

            // Check if we can create a valid session for this plugin
            Models.ApiModel.Authorize authorization = QueryApi("authorize", ApiType.Authorize);
            if (authorization == null)
            {
                Console.WriteLine("[CN__Switcher] Unable to authorize!");
                return false;
            }

            Console.WriteLine($"[CN__Switcher] Session ID instantiated for Switcher: {authorization.sessionId}");
            return true;
        }

        private static dynamic QueryApi(string apiPath, ApiType type)
        {
            using (WebClient c = new WebClient())
            {
                c.Headers.Add("Authorization", String.Format("CrewNode {0}", Config.Get().apiKey));
                string resp = c.DownloadString(String.Join('/', ApiUrl, apiPath));

                if (resp.Contains("DENIED")) return null;
                switch (type)
                {
                    case ApiType.Authorize:
                        return JsonConvert.DeserializeObject<Models.ApiModel.Authorize>(resp);
                    case ApiType.Push:
                        // TODO: Implement
                        return null;
                    case ApiType.Pull:
                        // TODO: Implement
                        return null;
                    default:
                        throw new Exception("Invalid ApiType specified for API Query in CrewNodeSwitcher.");
                }
            }
        }

    }

    enum ApiType
    {
        Authorize,
        Push,
        Pull
    };
}
