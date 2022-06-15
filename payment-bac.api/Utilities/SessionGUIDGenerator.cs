namespace payment_bac.api.Utilities
{
    public interface ISessionIdGenerator
    {
        string GenerateSessionId();
    }

    public class SessionGUIDGenerator : ISessionIdGenerator
    {
        public string GenerateSessionId()
        {
            var guid = Guid.NewGuid();

            return guid.ToString(); // Return a GUID, it's not secure but it doesn't really matter for this toy program.
        }
    }
}
