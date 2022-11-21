using DemoApp.Application.DTOs.Email;

using System.Threading.Tasks;

namespace DemoApp.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}