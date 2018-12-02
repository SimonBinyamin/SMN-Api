using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
    public class Browse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrowseId { get; set; }
        public string Value { get; set; }
    }
}
