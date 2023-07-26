using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Services
{
    public interface IProducer
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Calculation calculation);
    }
}
