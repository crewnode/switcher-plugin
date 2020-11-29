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
            if (authorization == null) return false;

            Console.WriteLine("alright somehow we got a session ID: " + authorization.sessionId);
            return true;
        }

        private static dynamic QueryApi(string apiPath, ApiType type)
        {
            using (WebClient c = new WebClient())
            {
                Console.WriteLine("Added API Key as '" + Config.Get().apiKey + "'");
                c.Headers.Add("Authorization", Config.Get().apiKey);
                Console.WriteLine("Querying: " + String.Join('/', ApiUrl, apiPath));
                string resp = c.DownloadString(String.Join('/', ApiUrl, apiPath));
                switch (type)
                {
                    case ApiType.Authorize:
                        return JsonConvert.DeserializeObject<Models.ApiModel.Authorize>(resp);
                    case ApiType.Push:
                        return null;
                    case ApiType.Pull:
                        return null;
                    default:
                        throw new Exception("Invalid ApiType specified for API Query in CrewNodeSwitcher.");
                }
            }
            return null;
        }

    }

    enum ApiType
    {
        Authorize,
        Push,
        Pull
    };
}
