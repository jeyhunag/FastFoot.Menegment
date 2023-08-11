using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using FastFood.DAL.Data;

namespace FastFoot.Web.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext db;
        public ContactController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {

            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var displayName = configuration["Gmail:DisplayName"];

                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                MailMessage mail = new MailMessage($"{contact.Email}", $"{username}");
                mail.Subject = displayName;


                mail.Body = $@"
               <html>
               <head> 
               <style>
              

              </style>
              </head>
              <body>
              <h2>Full Name: {contact.FullName} </h2>
              <h2>City Name: {contact.CityName} </h2>
              <h3>Email: {contact.Email}</h2> 
               <h3>Message <br /> {contact.Message}</h3>
            </body>
            </html>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);


                var category = await db.Contacts.AddAsync(contact);

                if (category != null)
                {
                    return RedirectToAction("Create");
                }

                db.Contacts.AddAsync(contact);
            }
            return View(contact);
        }
    }
}
