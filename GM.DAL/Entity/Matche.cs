using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Matche : BaseEntity
    {
        public Matche()
        {
            Scoreboards = new HashSet<Scoreboard>();
        }

        public virtual Server Server { get; set; }

        [ForeignKey("Server")]
        public long ServerId { get; set; }

        public virtual Map Map { get; set; }

        [ForeignKey("Map")]
        public long MapId { get; set; }

        public virtual GameMode GameMode { get; set; }

        [ForeignKey("GameMode")]
        public long GameModeId { get; set; }

        public int FragLimit { get; set; }

        public int TimeLimit { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartTimeStamp { get; set; }

        public virtual ICollection<Scoreboard> Scoreboards { get; set; }
    }
}