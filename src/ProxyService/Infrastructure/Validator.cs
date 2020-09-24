using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ProxyService.Infrastructure
{
    public class Validator : IValidator
    {
        private readonly ILogger<Validator> _logger;

        public Validator(ILogger<Validator> logger)
        {
            _logger = logger;
        }

        public Task ValidateAsync(object model)
        {
            _logger.LogInformation("Validated model: {model}", model);
            return Task.CompletedTask;
        }
    }
}
