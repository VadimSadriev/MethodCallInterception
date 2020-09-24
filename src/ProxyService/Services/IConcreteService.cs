using ProxyService.Dto;
using System.Threading.Tasks;

namespace ProxyService.Services
{
    public interface IConcreteService
    {
        void ConcreteVoidMethod();

        Task ConcreteTaskMethod();

        Task<ConcreteResultDto> ConcreteRequestMethod(ConcreteRequestDto dto);
    }
}
