using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.Consumer.Helpers;

namespace Scenius.CodeTest.Consumer.Repositories
{
    public class IngestRepository : IIngestRepository
    {
        private DataContext _context;

        public IngestRepository(DataContext context)
        {
            _context = context;
        }

        // Adds a calculation to the database
        public void postCalculation(Calculation calculation)
        {
            _context.Add(calculation);
            _context.SaveChanges();
        }
    }
}
