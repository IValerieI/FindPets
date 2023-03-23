using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FindPets.Services.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task SendEmail(EmailModel model)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailAddress").Value));
        email.To.Add(MailboxAddress.Parse(model.Email));
        email.Subject = model.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = model.Message };

        using var smtp = new SmtpClient();
        smtp.Connect(configuration.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(configuration.GetSection("EmailUserName").Value, configuration.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);


    }



}
