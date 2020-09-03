using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiBinaryHr._Helper;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Models;
using WebApiBinaryHr.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiBinaryHr.Controllers
{
    [Route("api/[controller]")]
    [Produces("Application/json")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly IUnitofwork Services_Repo;

        private UserManager<User> UserManager;

        private SignInManager<User> SignInManager;

        public IdentityController(IUnitofwork unitofwork , UserManager<User>_usermanager, SignInManager<User> signInManager)
        
        {             
            this.Services_Repo = unitofwork;
            this.UserManager = _usermanager;
            this.SignInManager = signInManager;

        }
        // GET: api/<IdentityController>
        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await this.Services_Repo.Userservice.GetAllUsers();
        }

        // GET api/<IdentityController>/5
        [HttpGet]
        [Route("GetUserById/{Id}")]
        public async Task<ActionResult<User>> Get( string Id)
        {
            if (Id == null) { return BadRequest(new { Mgs = " User Id Is Required !" }); }

            try {

                var Result = await this.UserManager.FindByIdAsync(Id);

                if (Result == null) { return NotFound(new { Mgs= " User Not Found !" }); }

                else { return Ok(Result); }
            
            }
            catch ( Exception Ex)
            {

                return BadRequest();
            }
        
        }

        // GET api/<controller>/5
        [HttpPost]
        [Route("GetEmail")]
        public async Task<ActionResult<User>> PostEmail([FromBody] Emailmodel Model)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            User User = await this.UserManager.FindByEmailAsync(Model.Email);

            if (User != null) { return BadRequest("Email Exist"); } else { return Ok("User Not Found"); }
        }

        // GET api/<controller>/5
        [HttpPost]
        [Route("GetUserName")]
        public async Task<ActionResult<User>> PostUserName([FromBody] UserNameModel Model)
        {

            if (!ModelState.IsValid) { return BadRequest(); }
            User User = await this.UserManager.FindByNameAsync(Model.Username);

            if (User != null) { return BadRequest("Username Exist"); } else { return Ok("User Not Found"); }
        }


        // POST api/<controller>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> PostCreateUser([FromBody] RegisterModel Model)
        {

            if (!ModelState.IsValid) { return BadRequest("Model state Error"); }


            try
            {
                
                    var UserEmail = await this.UserManager.FindByEmailAsync(Model.Email);

                    if (UserEmail != null) { return BadRequest("Email Already Exist"); }


                User NewUser = new User
                {

                    Email = Model.Email,
                    UserName = Model.FirstName,
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Created_At = DateTime.Now

                  

                    };

                    var Result = await UserManager.CreateAsync(NewUser, Model.Password);

                if (Result.Succeeded)
                {

                    var role = await this.UserManager.AddToRoleAsync(NewUser, "User");

                    if (role.Succeeded) { return Ok("User Created Successfully"); } else { return BadRequest("Internal server error role failed"); }

                }
                else { return BadRequest("error"); }
                
               
            }
            catch (Exception Ex)
            { return BadRequest("Model state Error"); }



        }

        [HttpPost]
        [Route("CreateUserWithRole")]
        public async Task<ActionResult> CreateUserWithRolePost([FromBody] RegisterModel Model)
        {

            if (ModelState.IsValid)
            {

                var User = await this.UserManager.FindByEmailAsync(Model.Email);

                if (User == null)
                {
                    User NewUser = new User
                    {
                        
                        
                        Email = Model.Email,
                        UserName = Model.FirstName,
                        FirstName = Model.FirstName,
                        LastName = Model.LastName,
                        Created_At = DateTime.Now



                    };

                    var Result = await UserManager.CreateAsync(NewUser, Model.Password);

                    if (Result.Succeeded)
                    {

                        var role = await this.UserManager.AddToRoleAsync(NewUser, Model.Role);

                        if (role.Succeeded) { return Ok("User Created Successfully"); }

                    }
                    else { return BadRequest(); }
                }
                else { return BadRequest("Email Alraedy Exist"); }

            }

            return null;
        }


        // PUT api/<controller>/5
        [HttpPut]
        [Authorize]
        [Route("UpdateUser/{id}")]
        public async Task<ActionResult> PutUpdateuser(string id, [FromBody] UpdateModel Model)
        {

            if (id == null) { return BadRequest("Id is Empty"); }


            string CurrentUserId = ExtentionHelper.GetUserId(User);

            var UserRole = await UserManager.FindByIdAsync(CurrentUserId);

            bool RoleResult = await UserManager.IsInRoleAsync(UserRole, "Director");

            if (CurrentUserId != id || RoleResult == false) { Unauthorized(" You do Not have the Permission for this action"); }

            if (ModelState.IsValid)
            {

                User UserToUpdate = new User
                {
                  
                   
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    UserName = Model.FirstName

                };



                var Result = await this.UserManager.UpdateAsync(UserToUpdate);

                if (Result.Succeeded) { return Ok("Update Successful"); } else { return NotFound("Could Not Update"); }


            }
            return BadRequest();

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("UpdateUserRole/{id}")]
        [Authorize(Roles = "Director")]
        public async Task<ActionResult> UpdateUserWithRolePut(string id, [FromBody] UserRole Model)
        {

            if (id == null) { return BadRequest("Id is Empty"); }

            if (ModelState.IsValid)
            {

                var User = await this.UserManager.FindByIdAsync(id);
                if (User == null) { return NotFound("User Not Found"); }


                var CurrentRole = await this.UserManager.GetRolesAsync(User);

                var RemoveResult = await this.UserManager.RemoveFromRoleAsync(User, CurrentRole.ToString());

                if (RemoveResult.Succeeded)
                {

                    var AddResult = await UserManager.AddToRoleAsync(User, Model.RoleName);


                    if (AddResult.Succeeded) { return Ok("User Role Updated Successfully"); }

                }




            }
            return BadRequest();

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Director")]
        public async Task<ActionResult> Deleteuser(string id)
        {

            if (id == null) { return NotFound("Id is Empty"); }

            User User = await this.UserManager.FindByIdAsync(id);

            if (User != null)
            {

                var Result = await this.UserManager.DeleteAsync(User);

                if (Result.Succeeded) { return Ok("User Deleted"); } else { return BadRequest(); }
            }
            else { return BadRequest(); }


        }



        // POST api/<controller>
        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<ActionResult> Postuser([FromBody] AuthenticationModel Model)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            try
            {


                User User = await this.UserManager.FindByEmailAsync(Model.Email);

                if (User == null) { return NotFound("Invalid Login"); }

                else
                {
                    var Role = await UserManager.GetRolesAsync(User);

                    if (Role[0] != "User") { return BadRequest(" Invalid Login"); }

                    var Result = await this.SignInManager.PasswordSignInAsync(User.UserName, Model.Password, Model.RememberMe, lockoutOnFailure: true);

                    if (Result.Succeeded)
                    {

                        var AccessToken = await this.GenerateJwtTokenAsync(Model.Email, User, Role.ToString());
                        var GeneratedRefreshToken = await this.generateRefreshToken(this.ipAddress());

                        var RefreshToken = GeneratedRefreshToken.Token;

                        ////////////////////////Add token to databse--------


                        User.RefreshTokens.Add(GeneratedRefreshToken);

                        /////////update user
                        ///
                        this.Services_Repo.Userservice.UpdateUserByToken(User);
                        await this.Services_Repo.CommitAsync();

                        this.setTokenCookie(RefreshToken);

                        if (AccessToken != null) { return Ok(new { User.Id, User.UserName, User.Email, AccessToken, RefreshToken, Role }); }

                        else { return BadRequest("Token Error"); }

                    }
                    else { return BadRequest("Invalid User"); }
                }
            }

            catch (Exception Ex)
            { return BadRequest("Model State Error" + Ex); }


        }


        // POST api/<controller>
        [HttpPost]
        [Route("AuthenticateAdminUser")]
        public async Task<ActionResult> AuthenticateAdminUserPost([FromBody] AuthenticationModel Model)
        {

            if (!ModelState.IsValid) { return BadRequest("Model State Error"); }

            try
            {
                User User = await this.UserManager.FindByEmailAsync(Model.Email);

                if (User == null) { return NotFound("Invalid Email"); }

                else
                {
                    var Role = await UserManager.GetRolesAsync(User);
                    if (Role[0] == "User") { return BadRequest(" Invalid Login"); }
                    var Result = await this.SignInManager.PasswordSignInAsync(User.UserName, Model.Password, Model.RememberMe, lockoutOnFailure: true);

                    if (Result.Succeeded)
                    {

                        var AccessToken = await this.GenerateJwtTokenAsync(Model.Email, User, Role.ToString());
                        var GeneratedRefreshToken = await this.generateRefreshToken(this.ipAddress());

                        var RefreshToken = GeneratedRefreshToken.Token;

                        ////////////////////////Add token to databse--------


                        User.RefreshTokens.Add(GeneratedRefreshToken);

                        /////////update user
                        ///
                        this.Services_Repo.Userservice.UpdateUserByToken(User);
                        await this.Services_Repo.CommitAsync();

                        this.setTokenCookie(RefreshToken);

                        if (AccessToken != null) { return Ok(new { User.Id, User.UserName, User.Email, AccessToken, RefreshToken, Role }); }

                        else { return BadRequest("Token Error"); }

                    }
                    else { return BadRequest("Invalid User"); }

                }
            }
            catch (Exception Ex)
            { return BadRequest("Invalid User" + Ex); }





        }

        [HttpPost]
        [Route("LogOut")]

        public async Task<ActionResult> PostLogOut(RefreshTokenRequest Model)
        {
            if (ModelState.IsValid)
            {
                await this.SignInManager.SignOutAsync();
                await this.RevokeToken(Model);
                return Ok("Signed Out");
            }

            return BadRequest(new { Response = "Model Error" });
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest Model)
        {
            var refreshToken = Model.RefreshToken ?? Request.Cookies["refreshToken"];
            RefreshToken response = await this.Services_Repo.Userservice.AuthenticateRefreshToken(refreshToken, ipAddress());

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            User user = await this.Services_Repo.Userservice.GetUserByRefreshToken(refreshToken);
            var roles = await UserManager.GetRolesAsync(user);
            // replace old refresh token with a new one and save
            var newRefreshToken = await this.generateRefreshToken(this.ipAddress());

            response.Revoked = DateTime.UtcNow;
            response.RevokedByIp = ipAddress();
            response.ReplacedByToken = newRefreshToken.Token;

            user.RefreshTokens.Add(newRefreshToken);

            this.Services_Repo.Userservice.UpdateUserByToken(user);
            await this.Services_Repo.CommitAsync();


            // generate new jwt
            var jwtToken = this.GenerateJwtTokenAsync(user.Email, user, roles.ToString());

            this.setTokenCookie(newRefreshToken.Token);

            this.setTokenCookie(newRefreshToken.Token);

            string AccessToken = jwtToken.Result.ToString();
            string RefreshToken = newRefreshToken.Token;

            return Ok(new { AccessToken, RefreshToken });
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.RefreshToken ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = " Refresh Token is required" });

            var response = await this.Services_Repo.Userservice.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        // create token
        private async Task<object> GenerateJwtTokenAsync(string email, User user, string role)
        {
            var claims = new List<Claim>
                {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Name, user.UserName),
          new Claim(ClaimTypes.Role,role)
                     };

            var roles = await UserManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            // get options
            //  var jwtAppSettingOptions =   JwtokenOptions;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtokenOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(10);

            var token = new JwtSecurityToken(
                JwtokenOptions.Issuer,
               JwtokenOptions.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private async Task<RefreshToken> generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

    }
}

