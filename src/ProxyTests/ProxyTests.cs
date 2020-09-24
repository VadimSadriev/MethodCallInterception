using Microsoft.Extensions.Logging;
using Moq;
using ProxyService.Dto;
using ProxyService.Infrastructure;
using ProxyService.Proxy;
using ProxyService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProxyTests
{
    public class ProxyTests
    {
        private readonly Proxy<IConcreteService> _proxy;
        private readonly Mock<ILogger<Proxy<IConcreteService>>> _loggerProxyMock = new Mock<ILogger<Proxy<IConcreteService>>>();
        private readonly Mock<ILogger<ConcreteService>> _loggerServiceMock = new Mock<ILogger<ConcreteService>>();
        private readonly Mock<IValidator> _validatorMock = new Mock<IValidator>();

        public ProxyTests()
        {
            var concreteService = new ConcreteService(_loggerServiceMock.Object);
            _proxy = new Proxy<IConcreteService>(concreteService, _validatorMock.Object, _loggerProxyMock.Object);
        }

        [Fact]
        public async Task ExecuteTest()
        {
            await _proxy.Execute(x => x.ConcreteTaskMethod());
            await _proxy.Execute(x => x.ConcreteVoidMethod());
        }

        [Fact]
        public async Task ExecuteWithDtoTest()
        {
            var dto = new ConcreteRequestDto
            {
                ConcreteString = "Just a string",
                ConcreteLong = 124124,
                ConcreteArray = new string[] { "2", "3" },
                ConcreteList = new List<ConcreteRequestItemDto>
                {
                    new ConcreteRequestItemDto
                    {
                        ConcreteString = "str"
                    }
                },
                ConcreteItem = new ConcreteRequestItemDto
                {
                    ConcreteLong = 232
                }
            };

            var result = await _proxy.Execute(x => x.ConcreteRequestMethod(dto));
        }
    }
}
