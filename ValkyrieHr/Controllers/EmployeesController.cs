using Microsoft.AspNetCore.Mvc;
using ValkyrieHr.Contracts.ApiRouters;
using ValkyrieHr.Domain.Dtos.Employee;
using ValkyrieHr.Services;

namespace ValkyrieHr.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesServices employeesServices;

        public EmployeesController(IEmployeesServices employeesServices)
        {
            this.employeesServices = employeesServices;
        }

        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.EmployeesRoutes.Create)]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequestDto dto)
        {
            var result = await employeesServices.CreateEmployeeAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.EmployeesRoutes.Update)]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeRequestDto dto)
        {
            var result = await employeesServices.UpdateEmployeeAsync(dto);
            return StatusCode(result.StatusCode, result);
        } 

        /// <summary>
        /// Get Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.EmployeesRoutes.Get)]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var result = await employeesServices.GetEmployeeAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoute.EmployeesRoutes.Delete)]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            var result = await employeesServices.DeleteEmployeeAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get All Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.EmployeesRoutes.GetAll)]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var result = await employeesServices.GetAllEmployeeAsync();
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Update Employee Image
        /// </summary>
        /// <returns></returns>
        [HttpPut(ApiRoute.EmployeesRoutes.UpdateImage)]
        public async Task<IActionResult> UpdateEmployeeImageAsync([FromForm] UpdateEmployeeImageDto req)
        {
            var result = await employeesServices.UpdateEmployeeImageAsync(req);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Get Form data Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.EmployeesRoutes.CreateForm)]
        public async Task<IActionResult> GetCreateFormEmployeeAsync()
        {
            var result = await employeesServices.GetCreateFormEmployeeAsync();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get Employee Remport
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.EmployeesRoutes.AllEmpReports)]
        public async Task<IActionResult> GetAllEmployeeReportsAsync()
        {
            var result = await employeesServices.GetAllEmployeeReportsAsync();
            return StatusCode(result.StatusCode, result);
        }

    }
}
