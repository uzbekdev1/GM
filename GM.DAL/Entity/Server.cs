using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GM.DAL.Infrastructure;

namespace GM.DAL.Entity
{
    public class Server : BaseEntity
    {
        public Server()
        {
            Matches = new HashSet<Matche>();
        }

        [StringLength(250)] public string Name { get; set; }

        [StringLength(50)] public string Hostname { get; set; }

        [Range(1024, 49151)] public int Port { get; set; }

        public virtual ICollection<Matche> Matches { get; set; }
    }
}