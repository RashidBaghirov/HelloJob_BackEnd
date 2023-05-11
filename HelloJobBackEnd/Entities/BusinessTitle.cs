using HelloJobBackEnd.Entities.Base;

namespace HelloJobBackEnd.Entities
{
    public class BusinessTitle : BaseEntity
    {
        public string Name { get; set; }
        public List<BusinessArea> BusinessAreas { get; set; }
    }
}
