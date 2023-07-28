using Microsoft.EntityFrameworkCore;
using Scenius.CodeTest.API.Helpers;
using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Repositories
{
    public class IngestRepository : IIngestRepository
    {
        private DataContext _context;

        public IngestRepository(DataContext context)
        {
            _context = context;
        }

        // Returns all calculations in the database
        public IEnumerable<Calculation> getAllCalculations()
        {
            return _context.Calculation.ToList();
        }
    }
}
