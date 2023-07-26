using MassTransit;
using Microsoft.Extensions.Logging;
using Scenius.CodeTest.Consumer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenius.CodeTest.Consumer.Consumers
{
    public class PostCalculationConsumer : IConsumer<Calculations>
    {
        readonly ILogger<PostCalculationConsumer> _logger;

        public PostCalculationConsumer(ILogger<PostCalculationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Calculations> context)
        {
            _logger.LogInformation("Received Calculation: {Calculation}", context.Message.Value);

            // TODO: Add database storage

            return Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
