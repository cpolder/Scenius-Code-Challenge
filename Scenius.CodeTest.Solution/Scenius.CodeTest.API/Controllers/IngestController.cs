using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Services;

namespace Scenius.CodeTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IngestController : ControllerBase
{
	private IIngestService _ingestService;

	private readonly ILogger<IngestController> _logger;

	// Constructor
	public IngestController(ILogger<IngestController> logger, IIngestService ingestService)
	{
		_logger = logger;
		_ingestService = ingestService;
	}

	// Get operation that returns all previously done calculations
	[HttpGet(Name = "GetCalculations")]
	public IEnumerable<Calculation> Get()
	{
		return _ingestService.getCalculations();
	}

	// Unused Post operation that adds a calculation to the database
	[HttpPost(Name = "PostCalculation")]
	[EnableCors("AllowSpecificOrigins")]
	public void Post(Calculation calculation)
	{
		_ingestService.performCalculation(calculation, false);
	}
}