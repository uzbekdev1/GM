using GM.Common;
using GM.DAL;
using GM.DAL.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GM.XUnit
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(params string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.SafeConfigure(AppSettings.ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        #region Instance

        private static ApplicationDbContextFactory _factory;

        public static ApplicationDbContext CreateDbContextInstance()
        {
            return (_factory ?? (_factory = new ApplicationDbContextFactory())).CreateDbContext();
        }

        #endregion
    }
}