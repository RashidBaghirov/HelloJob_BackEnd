using HelloJobBackEnd.Entities;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.ViewModel
{
    public class DashboardModel
    {
        public List<Cv> CVs { get; set; }
        public List<Company> Companies { get; set; }
    }
}
