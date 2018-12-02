using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
   public class Cellphone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CellphoneId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

    }
}
