namespace HRMData.Application.Interfaces
{
    public interface IEmailServices
    {
        Task SendResetPasswordEmailAsync(string resetPasswordEmailLink, string toEmail);
        Task SendPasswordChangeEmailAsync(string passwordChangeEmailLink, string toEmail, string Password);
    }
}
