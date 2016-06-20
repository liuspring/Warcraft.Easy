using System.Linq;
using Abp.Timing;
using TaskManager.EntityFramework;
using TaskManager.Events;
using TaskManager.MultiTenancy;

namespace TaskManager.Tests.Data
{
    public class TestDataBuilder
    {
        public const string TestEventTitle = "Test event title";

        private readonly TaskManagerDbContext _context;

        public TestDataBuilder(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateTestEvent();
        }

        private void CreateTestEvent()
        {
            var defaultTenant = _context.Tenants.Single(t => t.TenancyName == Tenant.DefaultTenantName);
            _context.Events.Add(Event.Create(defaultTenant.Id, TestEventTitle, Clock.Now.AddDays(1)));
            _context.SaveChanges();
        }
    }
}