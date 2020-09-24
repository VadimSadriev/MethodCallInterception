using Microsoft.AspNetCore.Mvc;
using ProxyService.Dto;
using ProxyService.Proxy;
using ProxyService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyService.Controllers
{
    [Route("/proxy")]
    public class ConcreteController : ControllerBase
    {
        private readonly IProxy<IConcreteService> _concreteProxyService;

        public ConcreteController(IProxy<IConcreteService> concreteProxyService)
        {
            _concreteProxyService = concreteProxyService;
        }

        [HttpGet]
        public async Task ConcreteApiMethod()
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

            var result = await _concreteProxyService.Execute(x => x.ConcreteRequestMethod(dto));

            await _concreteProxyService.Execute(x => x.ConcreteTaskMethod());
        }
    }
}
