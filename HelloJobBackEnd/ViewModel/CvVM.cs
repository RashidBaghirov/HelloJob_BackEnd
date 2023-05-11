using System.ComponentModel.DataAnnotations;

namespace HelloJobBackEnd.ViewModel
{
    public class CvVM
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]

        public DateTime BornDate { get; set; }
        public int CityId { get; set; }
        public int OperatingModeId { get; set; }
        public int ExperienceId { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public int BusinessAreaId { get; set; }
        public int EducationId { get; set; }
        public bool DrivingLicense { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]

        public int Number { get; set; }
        public IFormFile? CvPDF { get; set; }
    }
}
