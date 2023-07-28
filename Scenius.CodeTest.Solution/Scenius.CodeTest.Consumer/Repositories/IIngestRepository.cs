using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.Consumer.Repositories
{
    public interface IIngestRepository
    {

        public void postCalculation(Calculation calculation);
    }
}
