using Microsoft.AspNetCore.Mvc;
using yard.application.Services.Interface;

namespace yard.api.Controllers
{
	[Route("api/Transaction")]
	[ApiController]
	public class TransactionController : BaseApiController
	{
		private readonly ITransactionService _transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet("GetAllSuccessfulTransaction")]
		public async Task<IActionResult> GetAllSuccessfulTransaction(int pageNumber, int pageSize)
		{
			var result = await _transactionService.GetAllSuccessfulTransaction(pageNumber, pageSize);
			return Response(result);
		}
	}
}