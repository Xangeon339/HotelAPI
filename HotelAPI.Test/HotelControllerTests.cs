using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelAPI;
using HotelAPI.Controllers;
using HotelAPI.Models;

namespace HotelAPI.Test
{
    public class HotelControllerTests
    {
        [Test]
        public void AddHotelTest()
        {

            var controller = new HotelController();

            List<ContactInformation> testConInfoList = new List<ContactInformation>();

            testConInfoList.Add(new ContactInformation()
            {
                InformationType = EnmInformationType.Email,
                InformationContent = "test@test.com"
            });

            Hotel hotel = new Hotel()
            {
                AuthorizedName = "Test",
                AuthorizedSurname = "Test",
                CompanyTitle = "Test",
                ContactInformation = testConInfoList
            };

            var result = controller.AddHotel(hotel);

        }

        [Test]
        public void GetAllHotelsAuthorizedPersonDetail()
        {

            var controller = new HotelController();

            var result = controller.GetAllHotelsAuthorizedPersonDetail();

        }
    }
}
