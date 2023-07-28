using System.ComponentModel.DataAnnotations.Schema;

namespace Scenius.CodeTest.API.Models
{
    public class Calculation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String Input { get; set; }

        public int Result { get; set; }
    }
}
