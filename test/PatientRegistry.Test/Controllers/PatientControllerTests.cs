using System;
using Xunit;
using Moq;
using WebApplication.Controllers;
using System.Threading.Tasks;
using WebApplication.Models.Patient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Controllers
{
    public class PatientControllerTests
    {
        private IPatientRepository mockPatientSessionRepository;

        [Fact]
        public async void IndexReturnsPatientsWhenThereArePatientsInRepo()
        {
            var mockRepo = new Mock<IPatientRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetPatients()));
            var controller = new PatientController(mockRepo.Object);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PatientViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var mockRepo = new Mock<IPatientRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetPatients()));
            var controller = new PatientController(mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");

            var result = await controller.Index(new Patient());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }



        [Fact]
        public async Task IndexPost_ReturnsViewDataWithAModelWhenModelStateIsValid()
        {
            var mockRepo = new Mock<IPatientRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetPatients()));
            var controller = new PatientController(mockRepo.Object);
            
            var result = await controller.Index(new Patient());

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        private List<Patient> GetPatients()
        {
            var sessions = new List<Patient>();
            sessions.Add(new Patient()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1
            });
            sessions.Add(new Patient()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2
            });
            return sessions;
        }

        
    }
}
