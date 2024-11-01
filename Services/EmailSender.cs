using Application.Services.Abs;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    /// <summary>
    /// Wysyłanie maili przez Brevo
    /// </summary>
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _configuration;
        private string apiKey = "";
        private string nameFrom = "";
        private string emailFrom = "";
        private string emailTo = "";

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKey = _configuration["Brevo:Key"].ToString();
            nameFrom = _configuration["Brevo:NameFrom"].ToString();
            emailFrom = _configuration["Brevo:EmailFrom"].ToString();
            emailTo = _configuration["Brevo:EmailTo"].ToString();
        }


        public void SendEmail(string emailTo)
        {
            string htmlContent = @"<html>
<head>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            padding: 20px;
        }
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
            color: #ff5722;
        }
        .offer {
            background-color: #ffeb3b;
            padding: 10px;
            border-radius: 5px;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1> Specjalna oferta dla Ciebie! </h1>
        <p> Czas na wyjątkowe zniżki! Skorzystaj z naszej specjalnej oferty i zaoszczędź 20 % na pierwszym zamówieniu.</p>
        <div class='offer'>
            <strong>Użyj kodu: SAVE20</strong>
        </div>
        <p>Pozdrawiamy,<br>Zespół Marketingu</p>
    </div>
</body>
</html>";


            Configuration.Default.ApiKey.Add("api-key", apiKey);

            var apiInstance = new TransactionalEmailsApi();

            var sender = new SendSmtpEmailSender(nameFrom, emailFrom);
            var recipient = new SendSmtpEmailTo(emailTo, "mgmcdeveloper");

            List<SendSmtpEmailTo> recipients = new List<SendSmtpEmailTo> { recipient };

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, recipients, null, null, htmlContent, "Text content", "subject_AAAAA");
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Console.WriteLine($"Email wysłany: {result.MessageId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }


        }




        public void SendEmail(string emailTo, string title, string htmlContent)
        {
            // pobranie nazwy użytkownika z maila
            string[] nameToSplit = emailTo.Split('@');
            string nameTo = emailTo;
            if (nameToSplit.Length > 0)
                nameTo = nameToSplit[0];


            Configuration.Default.ApiKey.Add("api-key", apiKey);

            var apiInstance = new TransactionalEmailsApi();

            var sender = new SendSmtpEmailSender(nameFrom, emailFrom);
            var recipient = new SendSmtpEmailTo(emailTo, nameTo);

            List<SendSmtpEmailTo> recipients = new List<SendSmtpEmailTo> { recipient };

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, recipients, null, null, htmlContent, "Text content", title);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Console.WriteLine($"Email wysłany: {result.MessageId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }


        }



        /*
                public void SendEmail(string emailTo, string title, string htmlContent)
                {
                    // pobranie nazwy użytkownika z maila
                    string[] nameToSplit = emailTo.Split('@');
                    string nameTo = emailTo;
                    if (nameToSplit.Length > 0)
                        nameTo = nameToSplit[0];


                    Configuration.Default.ApiKey.Add("api-key", apiKey);

                    var apiInstance = new TransactionalEmailsApi();

                    var sender = new SendSmtpEmailSender(nameFrom, emailFrom);
                    var recipient = new SendSmtpEmailTo(emailTo, nameTo);

                    List<SendSmtpEmailTo> recipients = new List<SendSmtpEmailTo> { recipient };

                    try
                    {
                        var sendSmtpEmail = new SendSmtpEmail(sender, recipients, null, null, htmlContent, "Text content", title);
                        CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                        Console.WriteLine($"Email wysłany: {result.MessageId}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Wystąpił błąd: {e.Message}");
                    }


                }
        */



        /*
       public void SendEmail(string emailTo, string title, string htmlContent)
       {

           // pobranie nazwy użytkownika z maila
           string[] nameToSplit = emailTo.Split('@');
           string nameTo = emailTo;
           if (nameToSplit.Length > 0)
               nameTo = nameToSplit[0];


           Configuration.Default.ApiKey.Add("api-key", apiKey);

           var apiInstance = new TransactionalEmailsApi();

           var sender = new SendSmtpEmailSender(nameFrom, emailFrom); // od kogo
           // var recipient = new SendSmtpEmailTo(emailTo, nameTo); // do kogo
           var recipient = new SendSmtpEmailTo("mgmcdeveloper@gmail.com", "mgmcdeveloper");

           List<SendSmtpEmailTo> recipients = new List<SendSmtpEmailTo> { recipient };

           try
           {
               var sendSmtpEmail = new SendSmtpEmail(sender, recipients, null, null, htmlContent, "", title);
               CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
               Console.WriteLine($"Email wysłany: {result.MessageId}");
           }
           catch (Exception e)
           {
               Console.WriteLine($"Wystąpił błąd: {e.Message}");
           }

       }
*/

    }
}
