using System;
using System.Collections.Generic;
using System.Text;
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

            optionsBuilder.SafeConfigure("Server=localhost;Database=GM-TEST;User Id=sa;Password=web@1234;MultipleActiveResultSets=true;");

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
