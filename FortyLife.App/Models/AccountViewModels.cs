﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FortyLife.DataAccess.UserAccount;

namespace FortyLife.App.Models
{
    public class ActivateAccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Activation Key")]
        public string ActivationKey { get; set; }
    }

    public class ResendActivationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool AgreeToGuidelines { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ViewProfileViewModel
    {
        public string DisplayName { get; set; }

        public DateTime CreateDate { get; set; }

        public string AboutMe { get; set; }
    }

    public class ManageViewModel
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}
