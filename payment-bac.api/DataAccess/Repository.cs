using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payment_bac.api.Data;
using payment_bac.api.Models;
using payment_bac.api.Utilities;

namespace payment_bac.api.DataAccess
{
    public interface IRepository
    {
        public void AddSession(Session session);

        public Policy GetPolicy(string SessionId);

        public void PayTowardsPolicy(string SessionId, double Amount);

        public void MarkSessionAsComplete(string SessionId);
    }

    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void AddSession(Session session)
        {
            _context.Sessions.Add(session);

            _context.SaveChanges();
        }

        public Policy GetPolicy(string SessionId)
        {
            var current = _context.Sessions
                .Where(x => x.ID.Equals(SessionId))
                    .Include(x => x.Policy)
                .First();

            var policy = current.Policy;

            return policy;
        }

        public void PayTowardsPolicy(string SessionId, double Amount)
        {
            var current = _context.Sessions
                .Where(x => x.ID.Equals(SessionId))
                .Include(x => x.Policy)
                .First();

            var policy = current.Policy;

            policy.AmountPaid += Amount;

            _context.SaveChanges();
        }

        public void MarkSessionAsComplete(string SessionId)
        {
            var current = _context.Sessions
                .Where(x => x.ID.Equals(SessionId))
                .Include(x => x.Policy)
                .First();

            current.IsComplete = true;

            _context.SaveChanges();
        }
    }
}
