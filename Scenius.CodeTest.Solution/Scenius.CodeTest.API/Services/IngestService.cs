using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Repositories;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace Scenius.CodeTest.API.Services
{
    public class IngestService : IIngestService
    {

        private readonly IIngestRepository _calculatorRepository;
        private readonly IProducer _producer;

        public IngestService(IIngestRepository ingestRepository, IProducer producer) 
        {
            _calculatorRepository = ingestRepository;
            _producer = producer;
        }

        // Retrieves all calculations from the database
        public IEnumerable<Calculation> getCalculations()
        {
            return _calculatorRepository.getAllCalculations();
        }

        // Generates a random number as the answer to a calculation and puts the combination on the message queue
        public async void performCalculation(Calculation calculation, bool generated)
        {
            // TODO: Replace Random number generator with calculation
            Random random = new Random();
            calculation.Result = random.Next();

            await _producer.ExecuteAsync(new CancellationToken(), calculation);
        }
    }
}
