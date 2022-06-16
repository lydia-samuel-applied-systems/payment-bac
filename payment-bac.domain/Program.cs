using NServiceBus;
using payment_bac.domain.Daemons;
using Topshelf;

namespace payment_bac.domain
{
    public class Program
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<PaymentDaemon>(s =>
                {
                    s.ConstructUsing(name => new PaymentDaemon());
                    s.WhenStarted(async pd => await pd.Start());
                    s.WhenStopped(async pd => await pd.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Payment BAC Domain");
                x.SetDisplayName("Payment_BAC_Domain");
                x.SetServiceName("Payment_BAC_Domain");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}