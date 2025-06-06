using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Infra.EventBus.Settings
{
    public class MessageBrokerSettings
    {
        public string? Url { get; set; }
        public string? Queue { get; set; }
    }
}
