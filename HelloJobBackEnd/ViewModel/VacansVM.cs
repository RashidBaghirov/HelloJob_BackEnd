using HelloJobBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace HelloJobBackEnd.ViewModel
{
    public class VacansVM
    {
        [Required(ErrorMessage = " Şəhər  qeyd olunmalıdır.*")]
        public int CityId { get; set; }
        [Required(ErrorMessage = " İş saatı  qeyd olunmalıdır.*")]

        public int OperatingModeId { get; set; }
        [Required(ErrorMessage = " İş stajı  qeyd olunmalıdır.*")]

        public int ExperienceId { get; set; }
        public int? Salary { get; set; }
        [Required(ErrorMessage = " Vəzifə  qeyd olunmalıdır.*")]

        public string Position { get; set; }
        [Required(ErrorMessage = " Kateqoriya  qeyd olunmalıdır.*")]

        public int BusinessAreaId { get; set; }
        [Required(ErrorMessage = " Təhsil  qeyd olunmalıdır.*")]

        public int EducationId { get; set; }
        [Required(ErrorMessage = " Şirkət  qeyd olunmalıdır.*")]

        public int CompanyId { get; set; }
        public bool DrivingLicense { get; set; }
        [Required(ErrorMessage = " İş barədə məlumat  qeyd olunmalıdır.*")]

        public string? InfoWorks { get; set; }
        [Required(ErrorMessage = " Namizəddən tələblər   qeyd olunmalıdır.*")]

        public string? infoEmployeers { get; set; }
    }
}
