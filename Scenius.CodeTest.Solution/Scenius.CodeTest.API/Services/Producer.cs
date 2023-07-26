using MassTransit;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Records;

namespace Scenius.CodeTest.API.Services
{
    public class Producer : IProducer
    {
        readonly ISendEndpointProvider _bus;

        public Producer(ISendEndpointProvider bus)
        {
            _bus = bus;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken, Models.Calculation calculation)
        {

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:test?durable=true"));
            await endpoint.Send(calculation);   
            //.Publish(new Records.Calculation { Value = $"{calculation.Input} = {calculation.Result}" }, stoppingToken);
        }
    }
}
