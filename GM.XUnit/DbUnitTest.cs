using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GM.XUnit
{
    public class DbUnitTest
    {
        [Fact(DisplayName = "Check version")]
        public async void Check_Versoin_Test()
        {
            var factory = ApplicationDbContextFactory.CreateDbContextInstance();

            await factory.Database.OpenConnectionAsync();

            using (var db = factory.Database.GetDbConnection())
            {
                using (var com = db.CreateCommand())
                {
                    com.CommandText = "SELECT @@VERSION";
                    var version = (string) com.ExecuteScalar();

                    Assert.NotNull(version);
                }
            }
        }
    }
}