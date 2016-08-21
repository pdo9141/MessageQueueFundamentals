using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixeyed.MessageQueue.Messages.Commands
{
    public class UnsubscribeCommand
    {
        public string EmailAddress { get; set; }
    }
}
