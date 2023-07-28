using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Scenius.CodeTest.API.Exceptions;
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

	// Post operation that adds a calculation to the database via the event bus
	[HttpPost(Name = "PostCalculation")]
	public ActionResult Post(String calculation)
	{
		try
		{
			Calculation calc = new Calculation();
			calc.Input = calculation;
			_ingestService.performCalculation(calc, false);

		} catch(InputException e)
		{
			_logger.LogError(e.Message);
			return BadRequest();
		}
		return Ok();
	}
}