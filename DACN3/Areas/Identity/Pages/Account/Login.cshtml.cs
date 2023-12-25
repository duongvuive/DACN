// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DACN3.Models;
using System.Security.Claims;

namespace DACN3.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly Qldevice1Context _context;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, Qldevice1Context context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Email không được để trống.")]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Mật khẩu không được để trống.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Quên mật khẩu?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    /* var user= _context.AspNetUsers.FirstOrDefault(x=>x.Email == Input.Email);
                     var userRole = _context.AspNetUserRoles.FirstOrDefault(x => x.UserId == user.Id);
                     var Role=_context.AspNetRoles.FirstOrDefault(x=>x.Id==userRole.RoleId);*/
                    var user = _context.AspNetUsers.FirstOrDefault(x => x.Email == Input.Email);
                    var IsAccountInformation = _context.AccountInformations.FirstOrDefault(x => x.IdAspNetUsers == user.Id);

                    if (IsAccountInformation != null)
                    {
                        if (IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (IsInRole("Inventory Management"))
                        {
                            return RedirectToAction("TrangChu", "Notification");
                        }
                        else if (IsInRole("Manager"))
                        {
                            return RedirectToAction("TrangChu", "Manager");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Tài khoản này chưa được phân quyền");
                            return Page();
                        }
                    }
                    else if (IsAccountInformation == null)
                    {
                        return RedirectToAction("AccountInformation", "Information");
                    }

                    /*_logger.LogInformation("Người dùng đã đăng nhập.");
                    return LocalRedirect(returnUrl);*/
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Tài khoản người dùng bị khóa.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();

            bool IsInRole(string nameRole)
            {
                var Role = _context.AspNetRoles
                                                     .Join(_context.AspNetUserRoles, role => role.Id, userRole => userRole.RoleId, (role, userRole) => new { role, userRole })
                                                     .Join(_context.AspNetUsers, ur => ur.userRole.UserId, user => user.Id, (ur, user) => new { ur.role, user })
                                                     .Where(ur => ur.user.Email == Input.Email)
                                                     .Select(ur => ur.role)
                                                     .FirstOrDefault();
                if (nameRole == Role.Name.ToString())
                {
                    return true;
                }
                return false;
            }
        }


    }
}
