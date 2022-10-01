﻿using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;

namespace CA_Proj.Controllers
{
    public class ProductController : Controller
    {
        private readonly SystemContext _context;

        public ProductController(SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("username");
            ViewBag.Username = username;

            if (string.IsNullOrEmpty(username))
            {//haven't login
                ViewBag.IsLogin = false;
            }
            else//already login
            {
                ViewBag.IsLogin = true;
            }
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Add()
        {
            //传入商品的id和登录用户userid添加purchase_product数据
            string id = Request.Query["id"];
            return Json(new
            {
                message = id
            });
        }
    }
}
