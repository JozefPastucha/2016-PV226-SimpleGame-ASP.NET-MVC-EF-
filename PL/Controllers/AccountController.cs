﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BL.DTOs.UserAccount;
using BL.Facades;
using PL.Helpers.Auth;

namespace PL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Dependencies

        public SignInManager SignInManager { get; set; }

        public PlayerFacade PlayerFacade { get; set; }

        #endregion

        #region RegisterActionMethods

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("RegisterView", new UserRegistrationDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool success;
                    var accountId = 
                        PlayerFacade.RegisterPlayer
                        (model, out success);
                    if (success == false)
                    {
                        ModelState.AddModelError("Password", "Account with this username already exists");
                        return View("RegisterView", model);
                    }

                    SignInManager.SignIn(accountId, false);

                    return RedirectToAction("Index", "Home");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("RegisterView", model);
        }

        #endregion

        #region LogIn/OutActionMethods

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Login", new UserLoginDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var accountId = PlayerFacade.AuthenticateUser(model);

                if (!accountId.Equals(Guid.Empty))
                {
                    SignInManager.SignIn(accountId, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    var player = PlayerFacade.GetPlayerAccordingToUserName(model.UserName);
                    

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
            }

            return View("Login", model);
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                SignInManager.SignOut();
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}