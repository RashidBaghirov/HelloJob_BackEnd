using HelloJobBackEnd.Entities.Base;

namespace HelloJobBackEnd.Entities
{
    public class BusinessTitle : BaseEntity
    {
        public string? Image { get; set; }

        public string Name { get; set; }
        public List<BusinessArea> BusinessAreas { get; set; }
    }
}
