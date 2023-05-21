using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Services
{
    public class BusinessTitleService : IBusinessTitleService
    {
        private readonly HelloJobDbContext _context;

        public BusinessTitleService(HelloJobDbContext context)
        {
            _context = context;
        }

        public List<BusinessTitle> GetAllBusinessTitlesWithAreas()
        {
            List<BusinessTitle> titles = _context.BusinessTitle.Include(b => b.BusinessAreas).ToList();

            return titles;
        }
    }
}
