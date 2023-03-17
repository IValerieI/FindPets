namespace FindPets.Services.EmailSender;

public interface IEmailSenderService
{
    Task SendEmail(string message);
}
