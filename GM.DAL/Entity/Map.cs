using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Map : BaseEntity
    {

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Matche> Matches { get; set; } 

        public Map()
        {
            Matches = new HashSet<Matche>(); 
        }

    }
}