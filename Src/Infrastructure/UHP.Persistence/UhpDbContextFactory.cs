using Microsoft.EntityFrameworkCore;
using UHP.Persistence.Infrastructure;

namespace UHP.Persistence
{
    public class UhpDbContextFactory : DesignTimeDbContextFactoryBase<UhpContext>
    {
        protected override UhpContext CreateNewInstance(DbContextOptions<UhpContext> options)
        {
            return new UhpContext(options);
        }
    }
}
