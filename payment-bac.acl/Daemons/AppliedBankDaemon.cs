using Microsoft.Data.SqlClient;
using NServiceBus;
using payment_bac.acl.messages;

namespace payment_bac.acl.Daemons
{
    public class AppliedBankDaemon
    {
        private static readonly string _sqlConnectionString = "Data Source=UKWL00722\\SQLSERVER2017;Initial Catalog=BACDB;Integrated Security=True";
        private static readonly string _rabbitConnectionString = "host=localhost;username=guest;password=guest";

        private IEndpointInstance endpointInstance;

        public async Task Start()
        {
            var endpointName = "payment-bac.acl";

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.EnableInstallers();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(connectionBuilder: () =>
            {
                return new SqlConnection(_sqlConnectionString);
            });

            var transport = endpointConfiguration
                .UseTransport<RabbitMQTransport>()
                .ConnectionString(_rabbitConnectionString)
                .UseConventionalRoutingTopology();

            transport.Routing().RouteToEndpoint(typeof(PaymentProcessed), "payment-bac.domain");

            endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
