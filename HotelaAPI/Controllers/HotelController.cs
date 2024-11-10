﻿using HotelAPI.Models;
using HotelAPI.Services.HotelData;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelDataService hotelDataService;

        public HotelController(IHotelDataService hotelDataService)
        {
            this.hotelDataService = hotelDataService;
        }

        [HttpPost]
        public IActionResult AddHotel([FromBody] Hotel hotel)
        {

            try
            {
               hotelDataService.AddHotel(hotel);

                return Ok();
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteHotel(Guid hotelId)
        {
            try
            {
                hotelDataService.DeleteHotel(hotelId);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(Guid hotelId) 
        { 
        }

    }
}