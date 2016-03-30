// --------------------------------------------------------
// <copyright file="Program.cs" company="ChiakiYu">
//     Copyright (c) 2015-2016 ChiakiYu.All rights reserved
// </copyright >
// <site>http://www.8023me.com</site>
// <last-editor>于琦</last-editor>
// <last-date>2016-03-25 16:24</last-date>
// --------------------------------------------------------

using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmailDemo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("yu_1990@163.com"),
                Subject = "测试邮件",
                Body = "测试邮件 内容",
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                Priority = MailPriority.High,
                IsBodyHtml = true
            };
            mail.To.Add("yu_199012@qq.com");
            SendEmailAsync(mail);
        }



        public static void SendEmailAsync(MailMessage mail)
        {
            var client = new SmtpClient();
            client.Host = "smtp.163.com";
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("yu_1990@163.com", "asd1314");
            try
            {
                client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}