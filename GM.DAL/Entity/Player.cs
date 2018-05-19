using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Player : BaseEntity
    {

        [StringLength(50)]
        public string Name { get; set; }
         
        public virtual ICollection<Scoreboard> Scoreboards { get; set; }

        public Player()
        {
            Scoreboards = new HashSet<Scoreboard>(); 
        }

    }
}