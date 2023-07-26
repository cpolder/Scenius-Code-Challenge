using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Services
{
    public interface IIngestService
    {
        public IEnumerable<Calculation> getCalculations();

        public void performCalculation(Calculation calculation, bool generated);
    }
}
