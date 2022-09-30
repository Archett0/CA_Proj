using System.Collections.Generic;
using CA_Proj.Models;

namespace CA_Proj.ViewModels
{
    public class HistoryOrderViewModel
    {
        public int PurchaseId { get; set; }

        public Purchase Purchase { get; set; }

        public List<PurchaseProduct> Products { get; set; }

        public double Rating { get; set; }

        public string Comment { get; set; }
    }
}