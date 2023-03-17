using MailKit.Net.Smtp;
using MimeKit;

namespace FindPets.Services.EmailSender;

public class EmailSenderService : IEmailSenderService
{

    public async Task SendEmail(string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(" "));
        email.To.Add(MailboxAddress.Parse(" "));
        email.Subject = "Test";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(" ", " ");
        smtp.Send(email);
        smtp.Disconnect(true);


    }
}
