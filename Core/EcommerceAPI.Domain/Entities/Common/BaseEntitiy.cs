namespace EcommerceAPI.Domain.Entities.Common
{
    public class BaseEntitiy
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
    }
}
