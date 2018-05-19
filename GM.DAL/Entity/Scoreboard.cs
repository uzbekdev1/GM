using System.ComponentModel.DataAnnotations.Schema;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Scoreboard : BaseEntity
    {

        public virtual Player Player { get; set; }

        [ForeignKey("Player")]
        public long PlayerId { get; set; }

        public virtual Matche Matche { get; set; }

        [ForeignKey("Matche")]
        public long MatcheId { get; set; }

        public int Frags { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

    }
}