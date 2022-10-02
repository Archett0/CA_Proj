using CA_Proj.Data;
using CA_Proj.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA_Proj.Controllers
{
	public class OrderController : Controller
	{
		private readonly SystemContext _context;
		private const int MaxAutoCommentTime = 30;
		private const double AutoRatingMark = 5.0;
		private const string AutoCommentContent = "Customer didn't comment in 30 days, system automatically commented.";

		public OrderController(SystemContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var username = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(username))
			{//haven't login
				ViewBag.IsLogin = false;
				return View();
			}
			else//already login
			{
				ViewBag.Username = username;
				ViewBag.IsLogin = true;
			}
			// TODO: This should be fixed after we could get user id from session.
			var userId = Convert.ToInt32(HttpContext.Session.GetString("userid"));
			// var userId = HttpContext.Session.GetObject<int>("userid");
			// get purchases from the current user
			var query = _context.Purchases.AsQueryable();
			query = query.Where(c => c.UserId == userId && c.IsCart == 0);
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
					var timeAfterThirtyDays = purchase.CreateTime.AddDays(MaxAutoCommentTime);

					var purchaseProductList = await _context.PurchaseProducts
						.Where(pp => pp.PurchaseId == purchase.PurchaseId).Include(p => p.Product).ToListAsync();

					foreach (var purchaseProduct in purchaseProductList)
					{
						var changed = false;
						// find activation codes for the product
						var acCodeQuery = _context.ProductActivationCodes.AsQueryable();
						acCodeQuery = acCodeQuery.Where(c => c.PurchaseProductId == purchaseProduct.Id);
						purchaseProduct.ActivationCodes = new List<string>(await acCodeQuery.Select(c => c.ActivationCode).ToListAsync());

						if (purchaseProduct.CustomerRating.Equals(0.0))
						{
							if (currentTime >= timeAfterThirtyDays)
							{
								purchaseProduct.CustomerRating = AutoRatingMark;
								AffectProductOverallRating(AutoRatingMark, purchaseProduct.ProductId);
								changed = true;
							}
						}
						if (purchaseProduct.CustomerComment == null ||
								 purchaseProduct.CustomerComment.Trim().Length == 0)
						{
							if (currentTime >= timeAfterThirtyDays)
							{
								purchaseProduct.CustomerComment = AutoCommentContent;
								changed = true;
							}
						}

						// save context only if some items are changed
						if (changed) await _context.SaveChangesAsync();
					}

					var historyOrder = new HistoryOrderViewModel
					{
						PurchaseId = purchase.PurchaseId,
						Purchase = purchase,
						Products = purchaseProductList
					};
					viewModels.Add(historyOrder);
				}

				return View(viewModels);
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
				AffectProductOverallRating(purchaseProduct.CustomerRating, purchaseProduct.ProductId);
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

		// update overall rating each time a user gives rating
		private void AffectProductOverallRating(double newCustomerRating, int productId)
		{
			// get target product
			var products = _context.Products.ToList();
			var targetProduct = products.First(p => p.ProductId.Equals(productId));
			var currentRating = targetProduct.ProductOverallRating;
			var pplBought = targetProduct.ProductQuantitySold;

			// invalid rating
			if (newCustomerRating is < 1.0 or > 5.0) return;
			// set new rating and save
			targetProduct.ProductOverallRating = (currentRating * (pplBought - 1) + newCustomerRating) / pplBought;
			_context.SaveChanges();
		}
	}
}