using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Map : BaseEntity
    {
        public Map()
        {
            Matches = new HashSet<Matche>();
        }

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Matche> Matches { get; set; }
    }
}