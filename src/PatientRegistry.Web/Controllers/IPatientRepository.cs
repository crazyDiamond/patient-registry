using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models.Patient;

namespace WebApplication.Controllers
{
    public interface IPatientRepository
    {
        Task<List<Patient>> ListAsync();
        Task AddAsync(Patient patient);
    }
}