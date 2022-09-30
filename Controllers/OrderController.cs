using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using System.Linq;
using CA_Proj.Models;
using CA_Proj.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CA_Proj.Controllers
{
    public class OrderController : Controller
    {
        private readonly SystemContext _context;

        public OrderController(SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: This should be fixed after we could get user id from session.
            var userId = 1;
            // var userId = HttpContext.Session.GetObject<int>("userid");

            if (userId != 0)
            {
                // get purchases from the current user
                var query = _context.Purchases.AsQueryable();
                query = query.Where(c => c.User_id == userId);
                query = query.Where(c => c.Is_cart == 0);
                //System.Console.WriteLine(query.Count());

                // if there's no match, just return to the page and let front site process the null data
                if (!query.Any())
                {
                    return View();
                }
                // if there's match, we add additional data into a viewmodel and return it to the front site
                else
                {
                    var viewModels = new List<HistoryOrderViewModel>();
                    var purchaseList = await query.ToListAsync();

                    // get products for each purchase
                    foreach (var purchase in purchaseList)
                    {
                        // we set the default rating and comment IF the user haven't comment in 30days after he purchased the product
                        var currentTime = DateTime.Now;
                        var timeAfterThirtyDays = purchase.CreateTime.AddDays(30);

                        var purchaseProductList = await _context.PurchaseProducts
                            .Where(c => c.PurchaseId == purchase.Purchase_id).Include(p => p.Product).ToListAsync();

                        foreach (var purchaseProduct in purchaseProductList)
                        {
                            var changed = false;
                            if (purchaseProduct.CustomerRating.Equals(0.0))
                            {
                                if (currentTime >= timeAfterThirtyDays)
                                {
                                    purchaseProduct.CustomerRating = 5.0;
                                    changed = true;
                                }
                            }
                            if (purchaseProduct.CustomerComment == null ||
                                     purchaseProduct.CustomerComment.Trim().Length == 0)
                            {
                                if (currentTime >= timeAfterThirtyDays)
                                {
                                    purchaseProduct.CustomerComment = "User didn't comment in 30 days, system default commented.";
                                    changed = true;
                                }
                            }

                            // save context only if some items are changed
                            if(changed) await _context.SaveChangesAsync();
                        }

                        var historyOrder = new HistoryOrderViewModel
                        {
                            PurchaseId = purchase.Purchase_id,
                            Purchase = purchase,
                            Products = purchaseProductList
                        };
                        viewModels.Add(historyOrder);
                    }

                    return View(viewModels);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RateAndComment(int purchaseId, int productId, string rates, string comments,
            string whichBtn)
        {
            System.Console.WriteLine("Rates: {0}; Comments: {1}, whichBtn: {2}", rates, comments, whichBtn);
            System.Console.WriteLine("PurchaseId: {0};ProductId: {1}", purchaseId, productId);

            // get data from context
            var purchaseProduct = await _context.PurchaseProducts
                .Where(p => p.PurchaseId.Equals(purchaseId) && p.ProductId.Equals(productId)).FirstAsync();
            if (purchaseProduct == null) return RedirectToAction("Index");

            // if the action is rating the product
            var isRate = whichBtn.Equals("star");
            if (isRate)
            {
                // invalid input, so just reload the page
                if (rates == null || rates.Trim().Length == 0) return RedirectToAction("Index");

                // valid input, we set the value and submit
                // process and set data
                var actualRating = double.Parse(rates);
                purchaseProduct.CustomerRating = actualRating;
            }
            // if the action is commenting the product
            else
            {
                // invalid input, so just reload the page
                if (comments == null || comments.Trim().Length == 0 || comments.Trim().Length > 255) return RedirectToAction("Index");

                // valid input, we set the value and submit
                // process and set data
                var actualComment = comments.Trim();
                purchaseProduct.CustomerComment = actualComment;
            }

            // save the changes and reload the page
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}