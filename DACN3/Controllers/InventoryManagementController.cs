using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACN3.Controllers
{
    public class InventoryManagementController : Controller
    {
        [Authorize("Inventory Management")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
