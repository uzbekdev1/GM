using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GM.XUnit
{
    public class DbUnitTest
    {
        [Fact(DisplayName = "Check version")]
        public async void Check_Versoin_Test()
        {
            var factory = new ApplicationDbContextFactory().CreateDbContext(new string[0]);

            await factory.Database.OpenConnectionAsync();

            using (var db = factory.Database.GetDbConnection())
            {
                using (var com = db.CreateCommand())
                {
                    com.CommandText = "select @@VERSION";
                    var version = (string)com.ExecuteScalar();

                    Assert.NotNull(version);
                }
            }

        }
    }
}
