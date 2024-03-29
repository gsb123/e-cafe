﻿using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BusinessLogic
{
    public class EmailSending
    {

        const string fromPassword = "F7bs7Bvk";
        const string subject = "Subject";
        
        

        public EmailSending()
        {
        }

        public void send_mail(Exception x)
        {
            MailAddress fromAddress = new MailAddress("allin.cafe.admin@gmail.com", "NOTRIX - E_cAfe");
            MailAddress toAddress = new MailAddress("laszlo.erno@gmail.com", "Administrator");
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = x.Data.ToString() + "+++ STACK:" + x.StackTrace.ToString()
               
            })
            {
                smtp.Send(message);
            }

        }

        public void send_mail(string msg)
        {
            MailAddress fromAddress = new MailAddress("allin.cafe.admin@gmail.com", "NOTRIX - E_cAfe");
            MailAddress toAddress = new MailAddress("laszlo.erno@gmail.com", "Administrator");
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = msg//x.Data.ToString() + "+++ STACK:" + x.StackTrace.ToString()
            })
            {
                smtp.Send(message);
            }

        }

        public void sendLogs(string[] files)
        {
            MailAddress fromAddress = new MailAddress("allin.cafe.admin@gmail.com", "NOTRIX - E_Cafe");
            MailAddress toAddress = new MailAddress("laszlo.erno@gmail.com", "Administrator");
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
           

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = "Logok",



            })
            {
                foreach (var f in files)
                {
                    Attachment data = new Attachment(f, MediaTypeNames.Application.Octet);
                    message.Attachments.Add(data);
                }
                

                smtp.Send(message);
            }

        }


    }

    





}
