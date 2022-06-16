using payment_bac.acl.Daemons;
using Topshelf;

namespace payment_bac.acl
{
    public class Program
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<AppliedBankDaemon>(s =>
                {
                    s.ConstructUsing(name => new AppliedBankDaemon());
                    s.WhenStarted(async abd => await abd.Start());
                    s.WhenStopped(async abd => await abd.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Payment BAC ACL");
                x.SetDisplayName("Payment_BAC_ACL");
                x.SetServiceName("Payment_BAC_ACL");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}