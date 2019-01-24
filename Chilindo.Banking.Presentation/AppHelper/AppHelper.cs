using System.Collections.Generic;
using System.Web.Mvc;

namespace Chilindo.Banking.Presentation.AppHelper
{
    public class AppHelper
    {
        public static List<SelectListItem> GetCurrencies()
        {
            var currencies = new List<SelectListItem>() {
                new SelectListItem() { Value = "NGN",  Text = "NAIRA"},
                new SelectListItem() { Value = "USD", Text = "DOLLAR"},
                new SelectListItem() { Value = "GBP", Text = "POUND"},
                new SelectListItem() { Value = "EUR", Text = "EURO"},
                new SelectListItem() { Value = "TBT", Text = "BAHT"}
            };

            return currencies;
        }
    }

    //public class Currency
    //{
    //    public string CurrencyID { get; set; }
    //    public string CurrencyName { get; set; }
    //}
}