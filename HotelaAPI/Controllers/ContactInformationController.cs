using HotelAPI.Models;
using HotelAPI.Services.ContactInformation;
using HotelAPI.Services.HotelData;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationService contactInformationService;

        public ContactInformationController(IContactInformationService contactInformationService)
        {
            this.contactInformationService = contactInformationService;
        }

        [HttpPost]
        public IActionResult AddContactInformation([FromBody] ContactInformation contactInformation)
        {

            try
            {
                contactInformationService.AddContactInformation(contactInformation);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteContactInformation(Guid hotelId)
        {
            try
            {
                contactInformationService.DeleteContactInformation(hotelId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
