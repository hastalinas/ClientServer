using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class TugasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}