using NServiceBus;
using payment_bac.domain.Daemons;
using Topshelf;

namespace payment_bac.domain
{
    public class Program
    {
        public static void Main()
        {
            // TODO: lookup how topshelf works so I can run the NSB stuff in the background
            var rc = HostFactory.Run(x =>
            {
                x.Service<PaymentDaemon>(s =>
                {

                });
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}