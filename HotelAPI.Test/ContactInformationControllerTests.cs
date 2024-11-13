using HotelAPI.Controllers;
using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Test
{
    public class ContactInformationControllerTests
    {
        [Test]
        public void AddContactInformation()
        {

            var controller = new ContactInformationController();

            ContactInformation conInfo = new ContactInformation()
            {
                InformationType = EnmInformationType.PhoneNumber,
                InformationContent = "5556667788"
            };

            var result = controller.AddContactInformation(conInfo);
        }

    }
}
