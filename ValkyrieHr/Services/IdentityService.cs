using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValkyrieHr.Contracts.ApiResponse;
using ValkyrieHr.Domain.Dtos.Account;
using ValkyrieHr.Domain.Enums;
using ValkyrieHr.Helper;
using ValkyrieHr.Models;
using ValkyrieHr.Persistence;

namespace ValkyrieHr.Services
{
    public interface IIdentityService
    {
        Task<BaseResponse> CreateUserAsync(CreateUserRequestDto request);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        Task<ApplicationUser> GetByUserEmailAsync(string userEmail);
        Task<BaseResponse> AuthenticateAsync(LoginRequestDto request);
        Task<BaseResponse> GetAllUsersAsync();
        Task<BaseResponse> GetAccountDetails(string Id);
        Task<BaseResponse> UpdateAccount(UpdateAccountRequestDto req);
        Task<BaseResponse> UpdateImage(UserImageUpdateDto req);
        Task<BaseResponse> AddRoleAsync(string request);
    }

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUnitOfWork unitOfWork;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger, IMapper mapper, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.roleManger = roleManger;
            this.mapper = mapper;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create User 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CreateUserAsync(CreateUserRequestDto request)
        {
            try
            {
                // Check existing user.
                var existingUser = await userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Username is Already Exists!");
                }
                // Construct user object
                var user = mapper.Map<CreateUserRequestDto, ApplicationUser>(request);
                user.UserName = user.Email;

                // Create user
                var result = await userManager.CreateAsync(user, request.Password);
                // Construct and return user creation error response
                if (!result.Succeeded)
                {
                    var errors = result.Errors.FirstOrDefault();
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, $"Validation Error!, {errors}");
                }

                // add roles to users 
                await userManager.AddToRoleAsync(user, RoleEnums.Normal.ToString());

                return new BaseResponse(true, StatusCodesEnums.Status200OK, "Successfull!");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }

        /// <summary>
        /// Get User Name 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        /// <summary>
        /// Get User by Email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetByUserEmailAsync(string userEmail)
        {
            return await userManager.FindByEmailAsync(userEmail);
        }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="request"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<BaseResponse> AuthenticateAsync(LoginRequestDto request)
        {
            try
            {
                var user = await this.GetByUserEmailAsync(request.Username);
                if (user != null)
                {
                    // validate the password.
                    if (!await userManager.CheckPasswordAsync(user, request.Password))
                    {
                        return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Password Error!");
                    }
                    // validate is user acctive.
                    if (!user.IsActive)
                    {
                        return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "User is Not Active!");
                    }

                    // Create Token
                    //var stringToken = await GenerateJwtTokenAsync(user);
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", user.UserName));
                    claims.Add(new Claim("displayname", user.UserName));

                    // Add roles as multiple claims
                    var userRoles = await userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var token = JwtHelper.GetJwtToken(
                    user.UserName,
                    configuration["Jwt:Key"],
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    TimeSpan.FromDays(364),
                    claims.ToArray());

                    var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var tokenResponseDto = new TokenResponseDto
                    {
                        Username = user.UserName,
                        Token = stringToken,
                        UserId = user.Id
                    };
                    return new BaseResponse(true, StatusCodesEnums.Status200OK, "Login success!", tokenResponseDto);
                }
                else
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "not Found!");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Get All Users 
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> GetAllUsersAsync()
        {
            var user = await userManager.Users.ToListAsync();
            if (user == null)
            {
                return new BaseResponse(false, StatusCodesEnums.Status404NotFound, "No Users");
            }
            return new BaseResponse(true, StatusCodesEnums.Status200OK, "Success", user);
        }

        /// <summary>
        /// Get Account Details 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BaseResponse> GetAccountDetails(string Id)
        {
            var user = await userManager.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (user != null)
            {
                return new BaseResponse(true, StatusCodesEnums.Status200OK, "successfully", user);
            }
            return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
        }

        /// <summary>
        /// Update User Account
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<BaseResponse> UpdateAccount(UpdateAccountRequestDto req)
        {
            try
            {
                // Get the user
                var user = await userManager.FindByIdAsync(req.Id.ToString());
                if(user == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Not Found");
                }
                mapper.Map<UpdateAccountRequestDto, ApplicationUser>(req, user);
                user.UpdateDate = DateTime.Now;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new BaseResponse(true, StatusCodesEnums.Status200OK, "updated successfully");
                }
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "something went wrong!!!");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }

        /// <summary>
        /// update image
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<BaseResponse> UpdateImage([FromForm] UserImageUpdateDto req)
        {
            try
            {
                // Get the user
                var user = await userManager.FindByIdAsync(req.Id.ToString());
                if(user == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "not Found");
                }

                var filename = string.Empty;
                if (req.UserImage == null)
                {
                    return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "there are no pic!!!");
                }
                else
                {
                    if (!CommonMethods.IsValidImageFile(req.UserImage))
                    {
                        return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Unsupported Image Type or size is big!");
                    }

                    string uploadfile = Path.Combine(webHostEnvironment.WebRootPath, "Images\\UserProfileImages");
                    filename = Guid.NewGuid().ToString() + " _ " + req.UserImage.FileName;
                    string fullpath = Path.Combine(uploadfile, filename);

                    using (var fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        req.UserImage.CopyTo(fileStream);
                    }
                }

                user.UpdateDate = DateTime.Now;
                user.ProfileImage = filename;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new BaseResponse(true, StatusCodesEnums.Status200OK, "updated successfully");
                }
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "something went wrong!!!");
            }
            catch (Exception e)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, e.Message);
            }
        }

        /// <summary>
        /// add New Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseResponse> AddRoleAsync(string request)
        {
            var existingRole = await roleManger.FindByNameAsync(request);

            if (existingRole != null)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "role already Existed ");
            }

            var role = new IdentityRole
            {
                Name = request,
                NormalizedName = request.ToUpper(),
            };

            var result = await roleManger.CreateAsync(role);
            if (!result.Succeeded)
            {
                return new BaseResponse(false, StatusCodesEnums.Status400BadRequest, "Error!");
            }
            return new BaseResponse(true, StatusCodesEnums.Status200OK, "successfully");
        }
        // create role based token
        public class JwtHelper
        {
            public static JwtSecurityToken GetJwtToken(string username, string signingKey, string issuer, string audience, TimeSpan expiration, Claim[] additionalClaims = null)
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            // this guarantees the token is unique
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                if (additionalClaims is object)
                {
                    var claimList = new List<Claim>(claims);
                    claimList.AddRange(additionalClaims);
                    claims = claimList.ToArray();
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                return new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.UtcNow.Add(expiration),
                    claims: claims,
                    signingCredentials: creds
                );
            }
        }

    }
}