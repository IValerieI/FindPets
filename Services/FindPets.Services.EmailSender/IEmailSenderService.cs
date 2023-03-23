namespace FindPets.Services.EmailSender;

public interface IEmailSenderService
{
    Task SendEmail(EmailModel model);
}
