using HelloJobBackEnd.Entities;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.ViewModel
{
    public class DashboardVM
    {
        public List<Cv> AcceptedCv { get; set; }
        public List<Cv> PendingCV { get; set; }
        public List<Cv> RejectedCV { get; set; }
        public List<Vacans> AcceptedVacans { get; set; }
        public List<Vacans> PendingVacans { get; set; }
        public List<Vacans> RejectedVacans { get; set; }
        public double Maliyye { get; set; }
        public double Marketinq { get; set; }
        public double Texnalogiya { get; set; }
        public double Satish { get; set; }
        public double Xidmet { get; set; }
        public double Dizayn { get; set; }
        public double Muxtelif { get; set; }
        public double Sehiyye { get; set; }
        public double Huquq { get; set; }
        public double TehsilElm { get; set; }
        public double Senaye { get; set; }
        public double Inzibati { get; set; }
        public double SaturdayCvOrders { get; set; }
        public double SaturdayVacansOrders { get; set; }
        public double SundayCvOrders { get; set; }
        public double SundayVacansOrders { get; set; }
        public double MondayCVOrders { get; set; }
        public double MondayVacansOrders { get; set; }
        public double TuesdayCVOrders { get; set; }
        public double TuesdayVacansOrders { get; set; }
        public double WednesCVOrders { get; set; }
        public double WednesdayVacansOrders { get; set; }
        public double ThursdayCVOrders { get; set; }
        public double ThursdayVacansOrders { get; set; }
        public double FridayCVOrders { get; set; }
        public double FridayVacansOrders { get; set; }
        public Cv MostOrderedCv { get; set; }
        public Vacans MostOrderedVacans { get; set; }
    }
}
