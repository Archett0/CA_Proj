using Microsoft.AspNetCore.Mvc;

namespace CA_Proj.Controllers
{
	public class LogoutController : Controller
	{
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
	}
}
