using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.TwiML;
using OrderBot;


namespace OrderBotPage.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        private static Dictionary<string, Session>? sessionLookup = null;

        public ActionResult OnPost()
        {
            var from = Request.Form["From"];
            var body = Request.Form["Body"];
            var message = new Twilio.TwiML.MessagingResponse();

            if (sessionLookup == null)
            {
                sessionLookup = new Dictionary<string, Session>();
            }

            if (!sessionLookup.ContainsKey(from))
            {
                sessionLookup[from] = new Session(from);
            }

            var messages = sessionLookup[from].OnMessage(body);

            foreach (var m in messages)
            {
                message.Message(m);
            }

            return Content(message.ToString(), "application/xml");
        }

    }
}