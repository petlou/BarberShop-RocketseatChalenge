using BarberShop.Application.UseCases.Billings;
using BarberShop.Communication.RequestDTO.Billings;
using BarberShop.Communication.ResponseDTO.Billings;
using BarberShop.Communication.ResponseDTO.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers
{
    public class RegisterController : BarberShopBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterBillingDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status400BadRequest)]
        public IActionResult RegisterBilling([FromBody] RequestRegisterBillingDTO request)
        {
            var response = new RegisterNewBilling().Execute(request);

            return Created(string.Empty, response);
        }
    }
}
