﻿using HelloJobBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace HelloJobBackEnd.ViewModel
{
    public class VacansVM
    {
        public string Name { get; set; }
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

        public string InfoWorks { get; set; }
        public string infoEmployeers { get; set; }
    }
}
