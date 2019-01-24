using Chilindo.Banking.Presentation.Repository;
using Chilindo.Banking.WebApi.Models;
using System.Net.Http;
using System.Web.Mvc;

namespace Chilindo.Banking.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private HttpResponseMessage _response;
        private ServiceRepository _serviceObj;

        public AccountController()
        {
            _response = new HttpResponseMessage();
            _serviceObj = new ServiceRepository();
        }

        public ActionResult Deposit(BankAccountDto bankAccount)
        {
            ViewBag.Currencies = AppHelper.AppHelper.GetCurrencies();
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(int accountNumber, decimal amount, string currency)
        {
            try {
                var bankAccount = new BankAccountDto() { AccountNumber = accountNumber, Amount = amount, Currency = currency };
                _response = _serviceObj.PostResponse("api/account/deposit", bankAccount);
                _response.EnsureSuccessStatusCode();
                var currBankAccount = _response.Content.ReadAsAsync<BankAccountDto>().Result;
                return View(currBankAccount);
            }
            catch {
                var bankAccount = _response.Content.ReadAsAsync<BankAccountDto>().Result;
                return View(bankAccount);
            }
        }

        public ActionResult Withdraw(BankAccountDto bankAccount)
        {
            ViewBag.Currencies = AppHelper.AppHelper.GetCurrencies();
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(int accountNumber, decimal amount, string currency)
        {
            try {
                var bankAccount = new BankAccountDto() { AccountNumber = accountNumber, Amount = amount, Currency = currency };
                _response = _serviceObj.PostResponse("api/account/withdraw", bankAccount);
                _response.EnsureSuccessStatusCode();
                var currBankAccount = _response.Content.ReadAsAsync<BankAccountDto>().Result;
                return View(currBankAccount);
            }
            catch {
                var bankAccount = _response.Content.ReadAsAsync<BankAccountDto>().Result;
                return View(bankAccount);
            }
        }

        public ActionResult Balance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Balance(int accountNumber)
        {
            try {
                _response = _serviceObj.GetResponse($"api/account/balance?AccountNumber={accountNumber}");
                _response.EnsureSuccessStatusCode();
                var bankAccount = _response.Content.ReadAsAsync<BankAccountDto>().Result;
                return View(bankAccount);
            }
            catch {
                return View();
            }
        }

       
    }
}
