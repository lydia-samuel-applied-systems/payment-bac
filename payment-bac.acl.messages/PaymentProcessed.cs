using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace payment_bac.acl.messages
{
    public class PaymentProcessed : IMessage
    {
        public bool Success { get; set; }
    }
}
