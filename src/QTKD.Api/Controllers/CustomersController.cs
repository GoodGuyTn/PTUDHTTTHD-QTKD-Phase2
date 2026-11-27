using Microsoft.AspNetCore.Mvc;
using QTKD.Application.Dtos;
using QTKD.Application.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    /// <summary>Lấy danh sách khách hàng.</summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CustomerDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await customerService.GetAllAsync(ct));
}
