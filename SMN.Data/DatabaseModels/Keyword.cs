using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SMN.Data.DBModels
{
    public class Keyword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KeywordId { get; set; }
        public string Value { get; set; }
    }
}
