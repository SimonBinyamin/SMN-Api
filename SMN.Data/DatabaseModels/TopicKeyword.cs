using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SMN.Data.DBModels
{
    public class TopicKeyword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicKeywordId { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
