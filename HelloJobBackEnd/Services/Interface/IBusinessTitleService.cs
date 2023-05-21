using HelloJobBackEnd.Entities;

namespace HelloJobBackEnd.Services.Interface
{
    public interface IBusinessTitleService
    {
        List<BusinessTitle> GetAllBusinessTitlesWithAreas();
    }
}
