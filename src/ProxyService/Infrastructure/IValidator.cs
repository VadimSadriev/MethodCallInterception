using System.Threading.Tasks;

namespace ProxyService.Infrastructure
{
    public interface IValidator
    {
        Task ValidateAsync(object model);
    }
}
