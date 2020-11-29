using System;
using System.Collections.Generic;
using System.Text;

namespace CrewNodeSwitcherPlugin.Models
{
    public static class ApiModel
    {
        public class Authorize
        {
            public string sessionId { get; }

            public Authorize(string sessionId)
            {
                this.sessionId = sessionId;
            }
        }
    }
}
