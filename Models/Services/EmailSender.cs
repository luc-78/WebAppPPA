using System;
using System.Net.Mail;
using System.Net;
using WebAppPPA;

namespace WebAppPPA.Models.Services{

public class EmailSender
{
    
    public EmailSender()
        {
            
        }


    public void SendEmail(string receiver, string nomeReceiver, string mailSubject, string mailBody)
    {
       /*
        var senderEmail = Configuration.GetSection("Email").GetValue<string>("emailSender");
        var senderNome = Configuration.GetSection("Email").GetValue<string>("nomeSender");
        var host = Configuration.GetSection("Email").GetValue<string>("host");
        var fromPassword = Configuration.GetSection("Email").GetValue<string>("fromPassword");
        */
        var senderEmail = "lucabrighi@msn.com";
        var senderNome = "Staff ProgettoPA";
        var host = "outlook.office365.com"; // "smtp.domain.com",
        var fromPassword = "Agrippa2020";

        var fromAddress = new MailAddress(senderEmail, senderNome); // Email Sender
        var toAddress = new MailAddress(receiver, nomeReceiver); // Email Receiver
        

        var smtpClient = new SmtpClient
        {
            // Fill the following lines with your SMTP server info
            Host = host, 
            Port = 587,
            EnableSsl = true, // Set to true if the server requires SSL.
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address,fromPassword)
        };

        var message = new MailMessage(fromAddress,toAddress)
        {
            // Create the message.
            Subject = mailSubject,
            Body = mailBody
        };

        // The MailMessage.To property is a collection of emails, so you can add different recipients using:
        // message.To.Add(new MailAddress(...));
        //message.To.Add(toAddress);

        // Add reply-to address
        //message.ReplyToList.Add(replyToAddress);

        try
        {
            // Send email message
            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
            
        }
    }
}
}