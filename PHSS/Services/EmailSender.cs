using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace PHSS.Services
{
    public class EmailSender : SmtpClient
    {

        //Gmail Username
        public string UserName { get; set; }

        public EmailSender():
            base(ConfigurationManager.AppSettings["EmailHost"], Int32.Parse(ConfigurationManager.AppSettings["EmailPort"]))
        {
            //Get values from web.config file:
            this.UserName = ConfigurationManager.AppSettings["EmailUserName"];
            this.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["EmailSsl"]);
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(this.UserName, ConfigurationManager.AppSettings["EmailPassword"]);
        }

    }
}