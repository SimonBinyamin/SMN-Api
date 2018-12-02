using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
    public class TopicCellphone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicCellphoneId { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Cellphone Cellphone { get; set; }
    }
}
