using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WebAppPPA.Models.Services{

public class EmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender()
    {
        _configuration = Startup.Configuration;
    }


    public void SendEmail(string receiver, string nomeReceiver, string mailSubject, string mailBody)
    {
       
        var senderEmail = _configuration.GetSection("Email").GetValue<string>("emailSender");
        var senderNome = _configuration.GetSection("Email").GetValue<string>("nomeSender");
        var host = _configuration.GetSection("Email").GetValue<string>("host");
        var fromPassword = _configuration.GetSection("Email").GetValue<string>("fromPassword");
        var port = _configuration.GetSection("Email").GetValue<int>("port");

        var fromAddress = new MailAddress(senderEmail, senderNome); // Email Sender
        var toAddress = new MailAddress(receiver, nomeReceiver); // Email Receiver
        

        var smtpClient = new SmtpClient
        {
            // Fill the following lines with your SMTP server info
            Host = host, 
            Port = port,  //587,
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