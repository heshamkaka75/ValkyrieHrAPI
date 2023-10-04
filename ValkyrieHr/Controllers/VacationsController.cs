using Microsoft.AspNetCore.Mvc;
using ValkyrieHr.Contracts.ApiRouters;
using ValkyrieHr.Domain.Dtos.Vacation;
using ValkyrieHr.Services;

namespace ValkyrieHr.Controllers
{
    [ApiController]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationsServices vacationsServices;

        public VacationsController(IVacationsServices vacationsServices)
        {
            this.vacationsServices = vacationsServices;
        }

        /// <summary>
        /// Create Vacation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.VacationsRoutes.Create)]
        public async Task<IActionResult> CreateVacationAsync(CreateVacationRequestDto dto)
        {
            var result = await vacationsServices.CreateVacationAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update Vacation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.VacationsRoutes.Update)]
        public async Task<IActionResult> UpdateVacationAsync(UpdateVacationRequestDto dto)
        {
            var result = await vacationsServices.UpdateVacationAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get Vacation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.VacationsRoutes.Get)]
        public async Task<IActionResult> GetVacationAsync(Guid id)
        {
            var result = await vacationsServices.GetVacationAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete Vacation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoute.VacationsRoutes.Delete)]
        public async Task<IActionResult> DeleteVacationAsync(Guid id)
        {
            var result = await vacationsServices.DeleteVacationAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get All Vacation
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.VacationsRoutes.GetAll)]
        public async Task<IActionResult> GetAllVacationAsync()
        {
            var result = await vacationsServices.GetAllVacationAsync();
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Get Form data Vacation
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.VacationsRoutes.CreateForm)]
        public async Task<IActionResult> GetCreateFormVacationAsync()
        {
            var result = await vacationsServices.GetCreateFormVacationAsync();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get Vacation Report
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.VacationsRoutes.AllEmpReports)]
        public async Task<IActionResult> GetAllEmployeeVacationReportsAsync()
        {
            var result = await vacationsServices.GetAllEmployeeVacationReportsAsync();
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Create Employee Vacation
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoute.VacationsRoutes.CreateEmpVacation)]
        public async Task<IActionResult> CreateEmployeeVacationAsync(CreateEmployeeVacationDto req)
        {
            var result = await vacationsServices.CreateEmployeeVacationAsync(req);
            return StatusCode(result.StatusCode, result);
        }

    }
}

