using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using CA_Proj.Data;
using MySql.Data.MySqlClient;

namespace CA_Proj.Controllers
{
	public class RegisterController : Controller
	{
        public IActionResult Register(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            var sql = $"SELECT * FROM `user` WHERE username=@p0";
            var userExistOrNot = UserData.QueryIfUserExist(sql,new MySqlParameter("p0",username));
            if (userExistOrNot)
            {
                return Json(new
                {
                    success = false,
                    message= "The user name already exists!"
                });
            }
            //id(The "id" column will be set as the primary key and will be auto-incrementing.),username,password,nickname
            sql = $"INSERT INTO `user` (username,`password`,`nickname`) VALUES (@p0,@p1,@p2)";
            UserData.UpdateToRegister(sql, new MySqlParameter("p0", username), new MySqlParameter("p1", password), new MySqlParameter("p2", username));
            return RedirectToAction("Index", "Product");
        }
    }
}
