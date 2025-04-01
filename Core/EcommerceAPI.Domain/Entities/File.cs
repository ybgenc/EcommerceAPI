using EcommerceAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Domain.Entities
{
    public class File : BaseEntitiy
    {
        [NotMapped]
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }

        public string? FileName { get; set; }
        public string? Path { get; set; }
        public string? Storage { get; set; }


    }
}
