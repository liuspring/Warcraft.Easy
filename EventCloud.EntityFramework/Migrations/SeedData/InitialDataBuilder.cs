using TaskManager.EntityFramework;

namespace TaskManager.Migrations.SeedData
{
    public class InitialDataBuilder
    {
        private readonly TaskManagerDbContext _context;

        public InitialDataBuilder(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            new DefaultTenantRoleAndUserBuilder(_context).Build();
        }
    }
}
