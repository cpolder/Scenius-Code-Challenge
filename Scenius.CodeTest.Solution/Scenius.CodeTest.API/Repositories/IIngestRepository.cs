using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Repositories
{
    public interface IIngestRepository
    {
        public IEnumerable<Calculation> getAllCalculations();
    }
}
