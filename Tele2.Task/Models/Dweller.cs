using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tele2.Task.Models
{
    [Table("Dwellers")]
    public class Dweller
    {
        [Column("Id")]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Age")]
        public int Age { get; set; }

        [Column("Sex")]
        public string Sex { get; set; }
    }
}
