using Microsoft.Extensions.Logging;
using ProxyService.Dto;
using System.Threading.Tasks;

namespace ProxyService.Services
{
    public class ConcreteService : IConcreteService
    {
        private readonly ILogger<ConcreteService> _logger;
        public ConcreteService(ILogger<ConcreteService> logger)
        {
            _logger = logger;
        }

        public async Task<ConcreteResultDto> ConcreteRequestMethod(ConcreteRequestDto dto)
        {
            return new ConcreteResultDto
            {
                ConcreteArray = new string[] { "1", "2" },
                ConcreteLong = 12412412,
                ConcreteString = "Just a string"
            };
        }

        public async Task ConcreteTaskMethod()
        {
            _logger.LogInformation("Executing {method}", nameof(ConcreteTaskMethod));
        }

        public void ConcreteVoidMethod()
        {
            _logger.LogInformation("Executing {method}", nameof(ConcreteVoidMethod));
        }
    }
}
