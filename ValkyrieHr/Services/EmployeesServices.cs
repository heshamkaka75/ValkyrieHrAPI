using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ValkyrieHr.Contracts.ApiResponse;
using ValkyrieHr.Domain.Dtos.Employee;
using ValkyrieHr.Domain.Enums;
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Helper;
using ValkyrieHr.Persistence;

namespace ValkyrieHr.Services
{
    public interface IEmployeesServices
    {
        Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequestDto dto);
        Task<BaseResponse> UpdateEmployeeAsync(UpdateEmployeeRequestDto dto);
        Task<BaseResponse> GetEmployeeAsync(Guid id);
        Task<BaseResponse> DeleteEmployeeAsync(Guid id);
        Task<BaseResponse> GetAllEmployeeAsync();
        Task<BaseResponse> GetCreateFormEmployeeAsync();
        Task<BaseResponse> GetAllEmployeeReportsAsync();
        Task<BaseResponse> UpdateEmployeeImageAsync(UpdateEmployeeImageDto req);

    }

    public class EmployeesServices : IEmployeesServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConverter converter;

        public EmployeesServices(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConverter converter)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.converter = converter;
        }

        public async Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequestDto dto)
        {
            try
            {
                // Construct Employee object
                var employee = mapper.Map<CreateEmployeeRequestDto, Employee>(dto);

                // Create Employee
                await unitOfWork.Employees.AddAsync(employee);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", employee);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> UpdateEmployeeAsync(UpdateEmployeeRequestDto dto)
        {
            try
            {
                var employee = await unitOfWork.Employees.GetAsync(e => e.Id == dto.Id);
                if (employee == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }
                // Construct Employee object
                mapper.Map<UpdateEmployeeRequestDto, Employee>(dto, employee);
                employee.ModifiedDate = DateTime.Now;

                // Create Employee
                unitOfWork.Employees.UpdateAsync(employee);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", employee);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await unitOfWork.Employees.GetAsync(e => e.Id == id);
                if (employee == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }
                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", employee);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await unitOfWork.Employees.GetAsync(e => e.Id == id);
                if (employee == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }

                // Delete Employee
                unitOfWork.Employees.Remove(employee);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetAllEmployeeAsync()
        {
            try
            {
                var employee = await unitOfWork.Employees.GetEmpWithDepAsync();
                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", employee.OrderBy(e=>e.Name));
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetAllEmployeeReportsAsync()
        {
            try
            {
                int num = 0;
                var employeesDb = await unitOfWork.Employees.GetEmpWithDepAsync();
                var sb = new StringBuilder();
                sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Employees Payroll - Valkyrie</h1></div>
                                <div class='header'><h3>{0}</h3></div>
                                <table align='center'>
                                    <tr>
                                        <th>No</th>
                                        <th>Name</th>
                                        <th>Salary</th>
                                        <th>Department</th>
                                    </tr>", DateTime.Now);
                foreach (var emp in employeesDb)
                {
                    num += 1;

                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>",num, emp.Name, emp.Salary, emp.Department.Name);
                }
                sb.Append(@"
                                </table>
                            </body>
                        </html>");

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Valkyrie Report",
                    Out = @"D:\Employee_Report.pdf"
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Valkyrie HRMS" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var f = converter.Convert(pdf);

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfully created PDF document");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetCreateFormEmployeeAsync()
        {
            try
            {
                var gender = await unitOfWork.Genders.GetAllAsync();
                var dep = await unitOfWork.Departments.GetAllAsync();
                var vt = await unitOfWork.VacationTypes.GetAllAsync();
                var emp = await unitOfWork.Employees.GetAllAsync();

                var genderNames = gender.Select(g => new { gId = g.Id, gName = g.Name }).ToList();
                var depNames = dep.Select(d => new { dId = d.Id, dName = d.Name }).ToList();
                var vtNames = vt.Select(v => new { vtId = v.Id, vtName = v.Name }).ToList();
                var empNames = emp.Select(v => new { empId = v.Id, empName = v.Name }).ToList();
                var dto = new { genderNames, depNames, vtNames, empNames };

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", dto);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> UpdateEmployeeImageAsync(UpdateEmployeeImageDto req)
        {
            try
            {
                // Get the employees
                var employees = await unitOfWork.Employees.GetAsync(e => e.Id == req.Id);
                if (employees == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "not Found");
                }

                var filename = string.Empty;
                if (req.EmpImage == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "there are no pic!!!");
                }
                else
                {
                    if (!CommonMethods.IsValidImageFile(req.EmpImage))
                    {
                        return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Unsupported Image Type or size is big!");
                    }

                    string uploadfile = Path.Combine(webHostEnvironment.WebRootPath, "Images\\UserProfileImages");
                    filename = Guid.NewGuid().ToString() + " _ " + req.EmpImage.FileName;
                    string fullpath = Path.Combine(uploadfile, filename);

                    using (var fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        req.EmpImage.CopyTo(fileStream);
                    }
                }

                employees.ModifiedDate = DateTime.Now;
                employees.ProfileImage = filename;
                unitOfWork.Employees.UpdateAsync(employees);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "updated successfully");

            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }

    }
}
