using CleanBlog.Core.ViewModels;

namespace CleanBlog.Core.Services
{
    public interface ISmtpService
    {
        bool SendEmail(ContactViewModel model);
    }
}
