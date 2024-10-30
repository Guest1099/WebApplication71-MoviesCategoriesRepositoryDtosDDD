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
        private string apiKeyBrevo = "";
        private string emailFrom = "";
        private string emailTo = "";

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKeyBrevo = _configuration["Brevo:Key"].ToString();
            emailFrom = _configuration["Brevo:EmailFrom"].ToString();
            emailTo = _configuration["Brevo:EmailTo"].ToString();
        }


        public void SendEmail(string email)
        {
            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "dariuszwacchat"; // bierze pierwszy człon wyrazu
            string SenderEmail = "dariuszwacchat@gmail.com";

            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToName = "mgmcdeveloper"; // bierze pierwszy człon wyrazu
            string ToEmail = "mgmcdeveloper@gmail.com";

            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);


            string HtmlContent = null;
            string TextContent = "text content";


            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, "Subjectttttt ");
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e.Message);
            }
        }


        /*
                public void SendEmail(string email)
                {
                    Configuration.Default.ApiKey.Add("api-key", apiKeyBrevo);

                    var apiInstance = new TransactionalEmailsApi();
                    string SenderName = emailFrom.Split('@')[0]; // bierze pierwszy człon wyrazu
                    string SenderEmail = emailFrom;

                    SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
                    string ToName = emailFrom.Split('@')[0]; // bierze pierwszy człon wyrazu
                    string ToEmail = emailTo;

                    SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

                    List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
                    To.Add(smtpEmailTo);


                    string HtmlContent = null;
                    string TextContent = "text content";


                    try
                    {
                        var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, "Subjectttttt ");
                        CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                    }
                    catch (Exception e)
                    {
                        // Debug.WriteLine(e.Message);
                    }
                }
        */
        public void SendEmail(string email, string title, string description)
        {

        }
         
         
    }
}
