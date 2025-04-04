using BarberShop.Application.UseCases.Billings.Delete;
using BarberShop.Application.UseCases.Billings.GetAll;
using BarberShop.Application.UseCases.Billings.GetOne;
using BarberShop.Application.UseCases.Billings.Register;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Communication.ResponseDTO.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;
public class BillingController : BarberShopBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterBillingDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterNewBilling useCase,
        [FromBody] RequestRegisterBillingDTO request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseBillingsDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllBillings useCase)
    {
        var response = await useCase.Execute();

        if (response.Billings.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBillingDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOne(
        [FromServices] IGetOneBilling useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteBilling useCase,
        [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
