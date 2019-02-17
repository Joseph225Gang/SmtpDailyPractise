using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMTPPractise
{
    public class JobSendEmail : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            var result = context;
            var emailAddress = context.JobDetail.JobDataMap.Get("emailAddress").ToString();
            var password = context.JobDetail.JobDataMap.Get("password").ToString();
            var content = context.JobDetail.JobDataMap.Get("content").ToString();
            string mailBodyhtml = $"<p>{context}</p>";
            var msg = new MailMessage(emailAddress, emailAddress, "Hello", mailBodyhtml);
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.live.com", 587); //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailAddress, password);
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
