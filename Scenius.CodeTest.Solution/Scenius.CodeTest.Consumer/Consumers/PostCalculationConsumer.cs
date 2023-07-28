using MassTransit;
using Microsoft.Extensions.Logging;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.Consumer.Repositories;

namespace Scenius.CodeTest.Consumer.Consumers
{
    public class PostCalculationConsumer : IConsumer<Calculation>
    {
        readonly ILogger<PostCalculationConsumer> _logger;
        readonly IIngestRepository _ingestRepository;

        public PostCalculationConsumer(ILogger<PostCalculationConsumer> logger, IIngestRepository ingestRepository)
        {
            _logger = logger;
            _ingestRepository = ingestRepository;
        }

        public Task Consume(ConsumeContext<Calculation> context)
        {
            
            _logger.LogInformation("Received Calculation: {Calculation}", context.Message.ToString);
            
            // TODO: Add database storage
            _ingestRepository.postCalculation((Calculation) context.Message);

            return Task.CompletedTask;
        }
    }
}
