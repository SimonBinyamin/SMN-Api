using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
    public class TopicRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicRoleId { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Role Role { get; set; }
    }
}
