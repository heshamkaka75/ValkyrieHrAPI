using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValkyrieHr.Contracts.ApiRouters;
using ValkyrieHr.Domain.Dtos.Account;
using ValkyrieHr.Services;

namespace ValkyrieHr.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public AccountController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(ApiRoute.AccountRoutes.Create)]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequestDto dto)
        {
            var result = await identityService.CreateUserAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Update User Account 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut(ApiRoute.AccountRoutes.Update)]
        public async Task<IActionResult> UpdateAccountRequest(UpdateAccountRequestDto req)
        {
            var result = await identityService.UpdateAccount(req);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="User_Credentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(ApiRoute.AccountRoutes.Login)]
        public async Task<IActionResult> LoginRequest(LoginRequestDto dto)
        {
            var result = await identityService.AuthenticateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(ApiRoute.AccountRoutes.AllUser)]
        public async Task<IActionResult> AllUsersAsync()
        {
            var result = await identityService.GetAllUsersAsync();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(ApiRoute.AccountRoutes.ById)]
        public async Task<IActionResult> UserDetailsRequest(string id)
        {
            var result = await identityService.GetAccountDetails(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update User Image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut(ApiRoute.AccountRoutes.UserImageUpdate)]
        public async Task<IActionResult> ImageUpdateAsync([FromForm] UserImageUpdateDto dto)
        {
            var result = await identityService.UpdateImage(dto);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.AccountRoutes.AddRole)]
        public async Task<IActionResult> CreateRoleAsync(string dto)
        {
            var result = await identityService.AddRoleAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
