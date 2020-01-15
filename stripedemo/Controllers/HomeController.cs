using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Stripe;
using stripedemo.Models;

namespace stripedemo.Controllers
{
    public class HomeController : Controller
    {
        string APIKey= "sk_test_QH3gMc3gz7gErIluQpaeggK700R0BXpPkP";
        public ActionResult Index()
        {
            
           
            dynamic data=Listplan();
           
            return View();
        }
        public dynamic Create(Common model)
        {
            StripeConfiguration.ApiKey = APIKey;

            var options = new CustomerCreateOptions
            {
                Name=model.name,
                Email=model.email,
                Description = model.description,
            };
            var service = new CustomerService();
           return service.Create(options);
        }
        public dynamic Pay()

        {
            StripeConfiguration.ApiKey = APIKey;

            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Amount = 1099,
                Currency = "inr",
                Customer = "cus_GXQijo0EPiMTkd"
            };
           return service.Create(options);
        }
        public dynamic Sheduldeplan(Common model)
        {
            StripeConfiguration.ApiKey = APIKey;
            dynamic d= Create(model);
            model.cusid = "cus_GXQijo0EPiMTkd";
            var options = new SubscriptionCreateOptions
            {
                Customer = model.cusid,
                Items = new List<SubscriptionItemOptions>
                {
                 new SubscriptionItemOptions
                 {
                      Plan = model.palnid,
                 },
                },
            };

            var service = new SubscriptionService();
          return  service.Create(options);
        }
        public dynamic Listplan()
        {
            StripeConfiguration.ApiKey = APIKey;

            var options = new PlanListOptions { };
            var service = new PlanService();
           return service.List(options);
        }



    }
}
