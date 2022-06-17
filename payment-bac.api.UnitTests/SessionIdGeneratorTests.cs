using Xunit;
using Moq;
using payment_bac.api.Utilities;

namespace payment_bac.api.UnitTests
{
    public class SessionIdGeneratorTests
    {
        [Fact]
        public void GivenASessionIdGenerator_WhenIRequestMultipleSessions_ItShouldReturnDifferentIdsEachTime()
        {
            var sessionGenerator = new SessionGUIDGenerator();

            var sessions = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                sessions.Add(sessionGenerator.GenerateSessionId());
            }

            for (int i = 0; i < sessions.Count - 1; i++)
            {
                string cur = sessions[i];
                string next = sessions[i + 1];

                Assert.NotEqual(cur, next);
            }
        }
    }
}