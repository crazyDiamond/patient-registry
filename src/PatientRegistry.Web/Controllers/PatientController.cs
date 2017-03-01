using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using WebApplication.Models.Patient;

namespace WebApplication.Controllers {
   
    public class PatientController:Controller {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository){
            _patientRepository = patientRepository;
        }
        public async Task<IActionResult> Index()
        {
            var patientList = await _patientRepository.ListAsync();

            var model = patientList.Select(session => new PatientViewModel()
            {
                Id = session.Id,
                DateCreated = session.DateCreated
            });

            return View(model);
        }
            
        [HttpPost]
        public async Task<IActionResult> Index(Patient model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientRepository.AddAsync(new Patient()
            {
                DateCreated = DateTimeOffset.Now,
                Id = model.Id
            });

            return RedirectToAction("Index");
        }

    }

}