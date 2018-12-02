using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMN.Data.DBModels
{
    public class Topic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public string VideoRoute { get; set; }
        public byte[] Picture { get; set; }
        public bool Locked { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime TopicroveDate { get; set; }
        public virtual User Poster { get; set; }
        public virtual User Topicrover { get; set; }
        public virtual Browse Browse { get; set; }
        [NotMapped]
        public List<Cellphone> Cellphones { get; set; }
    }
}
