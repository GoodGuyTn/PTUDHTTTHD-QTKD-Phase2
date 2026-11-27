using Microsoft.AspNetCore.Mvc;
using QTKD.Application.Dtos;
using QTKD.Application.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractsController(IContractService contractService) : ControllerBase
{
    /// <summary>Xem chi tiết một hợp đồng theo Id. Không tồn tại → 404.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ContractDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await contractService.GetByIdAsync(id, ct));
}
