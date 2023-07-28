using MassTransit;
using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Services
{
    public class Producer : IProducer
    {
        readonly ISendEndpointProvider _bus;

        public Producer(ISendEndpointProvider bus)
        {
            _bus = bus;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken, Calculation calculation)
        {

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:post-calculation?durable=true"));
            await endpoint.Send(calculation);   
            //.Publish(new Records.Calculation { Value = $"{calculation.Input} = {calculation.Result}" }, stoppingToken);
        }
    }
}
