using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
    public class TopicCategory
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TopicCategoryId { get; set; }
    public virtual Topic Topic { get; set; }
    public virtual Category Category { get; set; }
  }

}
