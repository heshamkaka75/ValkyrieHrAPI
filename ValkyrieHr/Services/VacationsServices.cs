using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ValkyrieHr.Contracts.ApiResponse;
using ValkyrieHr.Domain.Dtos.Vacation;
using ValkyrieHr.Domain.Enums;
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Persistence;

namespace ValkyrieHr.Services
{
    public interface IVacationsServices
    {
        Task<BaseResponse> CreateVacationAsync(CreateVacationRequestDto dto);
        Task<BaseResponse> UpdateVacationAsync(UpdateVacationRequestDto dto);
        Task<BaseResponse> GetVacationAsync(Guid id);
        Task<BaseResponse> DeleteVacationAsync(Guid id);
        Task<BaseResponse> GetAllVacationAsync();
        Task<BaseResponse> GetCreateFormVacationAsync();
        Task<BaseResponse> GetAllEmployeeVacationReportsAsync();
        Task<BaseResponse> CreateEmployeeVacationAsync(CreateEmployeeVacationDto req);
    }

    public class VacationsServices : IVacationsServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConverter converter;

        public VacationsServices(IUnitOfWork unitOfWork, IMapper mapper, IConverter converter)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.converter = converter;
        }

        public async Task<BaseResponse> CreateVacationAsync(CreateVacationRequestDto dto)
        {
            try
            {
                // Construct VacationBalance object
                var vacationBalance = mapper.Map<CreateVacationRequestDto, VacationBalance>(dto);

                // Create vacationBalance
                await unitOfWork.VacationBalances.AddAsync(vacationBalance);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", vacationBalance);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> UpdateVacationAsync(UpdateVacationRequestDto dto)
        {
            try
            {
                var vacationBalance = await unitOfWork.VacationBalances.GetAsync(e => e.Id == dto.Id);
                if (vacationBalance == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }
                // Construct vacationBalance object
                mapper.Map<UpdateVacationRequestDto, VacationBalance>(dto, vacationBalance);
                vacationBalance.ModifiedDate = DateTime.Now;

                // Create vacationBalance
                unitOfWork.VacationBalances.UpdateAsync(vacationBalance);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", vacationBalance);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetVacationAsync(Guid id)
        {
            try
            {
                var vacationBalance = await unitOfWork.VacationBalances.GetAsync(e => e.Id == id);
                if (vacationBalance == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }
                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", vacationBalance);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> DeleteVacationAsync(Guid id)
        {
            try
            {
                var vacationBalance = await unitOfWork.VacationBalances.GetAsync(e => e.Id == id);
                if (vacationBalance == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }

                // Delete vacationBalance
                unitOfWork.VacationBalances.Remove(vacationBalance);
                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetAllVacationAsync() 
        {
            try
            {
                var vacationBalance = await unitOfWork.VacationBalances.GetEmpWithDepAsync();
                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", vacationBalance.OrderBy(v=>v.EmployeeId));
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> GetAllEmployeeVacationReportsAsync()
        {
            try
            {
                int num = 0;
                var vacationBalance = await unitOfWork.VacationBalances.GetEmpWithDepAsync();
                var sb = new StringBuilder();
                sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Employees Vacation Balance - Valkyrie</h1></div>
                                <div class='header'><h3>{0}</h3></div>
                                <table align='center'>
                                    <tr>
                                        <th>No</th>
                                        <th>Name</th>
                                        <th>Vacation Type</th>
                                        <th>Total Days</th>
                                         <th>Number of Days left</th>
                                        <th>Department</th>
                                    </tr>", DateTime.Now);
                foreach (var emp in vacationBalance.OrderBy(v=>v.EmployeeId))
                {
                    num += 1;

                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                  </tr>", num, emp.Employee.Name, emp.VacationType.Name, emp.NumberOfDays, emp.DaysLeft, emp.Employee.Department.Name);
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
                    Out = @"D:\Employee_Vacations_Report.pdf"
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
        public async Task<BaseResponse> GetCreateFormVacationAsync()
        {
            try
            {
                var employees = await unitOfWork.Employees.GetAllAsync();
                var vacationTypes = await unitOfWork.VacationTypes.GetAllAsync();

                var employeesNames = employees.Select(g => new { gId = g.Id, gName = g.Name }).ToList();
                var vacationTypesNames = vacationTypes.Select(d => new { dId = d.Id, dName = d.Name }).ToList();
                var dto = new { employeesNames, vacationTypesNames };

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!", dto);
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }
        public async Task<BaseResponse> CreateEmployeeVacationAsync(CreateEmployeeVacationDto req)
        {
            try
            {
                var employee = await unitOfWork.Employees.GetAsync(e => e.Id == req.EmployeeId);
                if(employee == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "employee not found!");
                }

                var balance = await unitOfWork.VacationBalances.
                    GetAsync(a=>a.VacationTypeId == req.VacationTypeId && a.EmployeeId == req.EmployeeId && a.DaysLeft >= 1);

                if(balance == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "You dont have enough vacation balances");
                }

                if(req.Duration > balance.DaysLeft)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "You dont have enough vacation balances");
                }

                // Construct  object
                var vacation = mapper.Map<CreateEmployeeVacationDto, Vacation>(req);

                // Create 
                try
                {
                    await unitOfWork.Vacations.AddAsync(vacation);

                    balance.DaysLeft = balance.DaysLeft - req.Duration;
                    balance.ModifiedDate = DateTime.Now;

                    unitOfWork.VacationBalances.UpdateAsync(balance);
                }
                catch (Exception e)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
                }

                await unitOfWork.CommitAsync();

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "updated successfully", vacation);

            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }

    }
}
