using BankAppER.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace BankAppER.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBankRepository _repo;

        public HomeController(IBankRepository repo)
        {
            _repo = repo;
        }


        public IActionResult Index()
        {
            var model = _repo.GetCustomers().ToList();

            return View(model);
        }
    }
}
