using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class GameMode : BaseEntity
    {

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Matche> Matches { get; set; }
         
        public GameMode()
        {
            Matches = new HashSet<Matche>(); 
        }

    }
}