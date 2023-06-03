using HelloJobBackEnd.Areas.HelloJobAdmins.ViewModel;
using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services;
using HelloJobBackEnd.Services.Interface;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{

    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class HomeController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly IVacansService _vacansService;
        private readonly ICvPageService _cvPageService;
        private readonly IBusinessTitleService _businessTitleService;
        private readonly ICompanyService _companyService;
        private readonly UserService _userService;

        public HomeController(HelloJobDbContext context, IVacansService vacansService, ICvPageService cvPageService, IBusinessTitleService businessTitleService, ICompanyService companyService, UserService userService)
        {
            _context = context;
            _vacansService = vacansService;
            _cvPageService = cvPageService;
            _businessTitleService = businessTitleService;
            _companyService = companyService;
            _userService = userService;
        }


        public IActionResult Index()
        {
            List<Vacans> vacansList = _vacansService.GetAcceptedVacansWithRelatedData().ToList();
            List<Cv> cvList = _cvPageService.GetAllCvs().ToList();
            List<Company> companyList = _companyService.GetTopAcceptedCompaniesWithVacans().ToList();
            List<BusinessTitle> businessTitles = _businessTitleService.GetAllBusinessTitlesWithAreas();
            List<BusinessTitleChartData> cvCountByBusinessArea = businessTitles.Select(ba => new BusinessTitleChartData
            {
                BusinessName = ba.Name,
                CvCount = cvList.Count(cv => cv.BusinessArea.BusinessTitleId == ba.Id),
                VacansCount = vacansList.Count(v => v.BusinessArea.BusinessTitleId == ba.Id)
            }).ToList();
            List<BusinessTitleChartData> vacansCountByBusinessArea = businessTitles.Select(ba => new BusinessTitleChartData
            {
                BusinessName = ba.Name,
                CvCount = cvList.Count(cv => cv.BusinessAreaId == ba.Id),
                VacansCount = vacansList.Count(v => v.BusinessAreaId == ba.Id)
            }).ToList();
            List<CompanyChartData> companyVacansCounts = companyList.Select(c => new CompanyChartData
            {
                CompanyName = c.Name,
                VacansCount = vacansList.Count(v => v.CompanyId == c.Id)
            }).ToList();
            var labels_company = companyVacansCounts.Select(cv => cv.CompanyName).ToList();
            var labels = businessTitles.Select(ba => ba.Name).ToList();
            var datasetData = companyVacansCounts.Select(cv => cv.VacansCount).ToList();
            var dataset1Data = cvCountByBusinessArea.Select(item => item.CvCount).ToList();
            var dataset2Data = vacansCountByBusinessArea.Select(item => item.VacansCount).ToList();
            ViewBag.LabelsCompany = labels_company;
            ViewBag.DatasetData = datasetData;
            ViewBag.Labels = labels;
            ViewBag.Dataset1Data = dataset1Data;
            ViewBag.Dataset2Data = dataset2Data;
            ViewBag.Vacans = vacansList;
            ViewBag.Company = companyList;
            ViewBag.Cvs = cvList;
            ViewBag.Users = _userService.GetAllUsers();
            return View();
        }




    }

}

