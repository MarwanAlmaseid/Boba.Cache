using Boba.Cache.Web.Memory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Boba.Cache.Web.Memory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly ICacheManager _cacheManager;
        private readonly ICacheService _cacheService;

        public HomeController(ILogger<HomeController> logger, ICacheKeyService cacheKeyService, ICacheManager cacheManager, ICacheService cacheService)
        {
            _logger = logger;
            _cacheKeyService = cacheKeyService;
            _cacheManager = cacheManager;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var key = _cacheKeyService.PrepareKey("TestTest.Test2");

            var employees = await _cacheManager.GetAsync(key, async () =>
            {
                var employeeService = new EmployeeService();

                return employeeService.GetDummyEmployees();
            });

            return View(employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Clean()
        {
            _cacheService.DeleteAllWithPrefix("TestTest");

            return Ok();
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }

    public class EmployeeService
    {
        private List<Employee> _employees;

        public EmployeeService()
        {
            // Initialize the list with some dummy employees
            _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Software Developer", HireDate = new DateTime(2020, 1, 15), Salary = 60000 },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager", HireDate = new DateTime(2018, 3, 10), Salary = 85000 },
            new Employee { Id = 3, Name = "Michael Johnson", Position = "QA Engineer", HireDate = new DateTime(2019, 5, 25), Salary = 50000 },
            new Employee { Id = 4, Name = "Emily Davis", Position = "UX Designer", HireDate = new DateTime(2021, 7, 30), Salary = 70000 },
            new Employee { Id = 5, Name = "David Wilson", Position = "DevOps Engineer", HireDate = new DateTime(2022, 11, 5), Salary = 75000 }
        };
        }

        // Method to retrieve dummy employees
        public List<Employee> GetDummyEmployees()
        {
            return _employees;
        }

        // Method to add a new employee
        public void AddEmployee(Employee newEmployee)
        {
            _employees.Add(newEmployee);
        }
    }
}
