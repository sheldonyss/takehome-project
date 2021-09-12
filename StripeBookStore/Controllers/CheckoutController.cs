using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;

namespace StripeBookStore.Controllers
{
    public class CheckoutController : Controller
    {
        public CheckoutController()
        {
            StripeConfiguration.ApiKey =
                "sk_test_51JY50UEn75gMW8WBoxBSzPWjZb2Wkmi6rW8zNjRXR81bosxyaUUODGvc1QezXmsjTP1W6FCAkyzJV6jusWqsXfGx00ovOACDff";

        }
        public IActionResult Index(int item)
        {
            string title = string.Empty, error = string.Empty;
            int amount = 0;
            if (item < 1 || item > 3)
            {
                return NotFound();
            }
            switch (item)
            {
                case 1:
                    title = "The Art of Doing Science and Engineering";
                    amount = 2300;
                    break;
                case 2:
                    title = "The Making of Prince of Persia: Journals 1985-1993";
                    amount = 2500;
                    break;
                case 3:
                    title = "Working in Public: The Making and Maintenance of Open Source";
                    amount = 2800;
                    break;
                default:
                    // Included in layout view, feel free to assign error
                    error = "No item selected";
                    break;

            }

            ViewData["amount"] = amount;
            ViewData["title"] = title;
            ViewData["error"] = error;
            ViewData["secret"] = CreateIntendClientSecret(amount);
            return View();
        }



        public IActionResult Success(string id)
        {
            try
            {
                var service = new PaymentIntentService();
                var intent = service.Get(id);
                var amount = intent.AmountReceived / 100;
                ViewData["status"] = intent.Status;
                ViewData["method"] = string.Join(",", intent.PaymentMethodTypes);
                ViewData["amount"] = amount.ToString("C");
                ViewData["id"] = intent.Id;
            }
            catch (StripeException ex)
            {
                return NotFound();
            }

            return View();
        }

        private string CreateIntendClientSecret(int amount)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "sgd",
                // Verify your integration in this guide by including this parameter
                Metadata = new Dictionary<string, string>
                {
                    { "integration_check", "accept_a_payment" },
                }
                
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options,new RequestOptions()
            {
                // in real life, use customer's order number as the key
                // IdempotencyKey = 
            });
            return paymentIntent.ClientSecret;
        }
    }
}
