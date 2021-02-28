using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using DotvvmValidationSample.Model;
using DotVVM.Framework.ViewModel.Validation;

namespace DotvvmValidationSample.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        public SignInForm SignInForm { get; set; } = new SignInForm();

        public CreateAccountForm CreateAccountForm { get; set; } = new CreateAccountForm();

        public void SignIn()
        {
            // this code actually doesn't sign anyone in - it's just a validation demo
            // you can use e.g. SignInManager.PasswordSignInAsync from ASP.NET Core Identity

            if (SignInForm.Email == "info@dotvvm.com" && SignInForm.Password == "Password1234")
            {
                Context.RedirectToRoute("Success");
            }
            else
            {
                // TODO: report invalid credentials
                Context.ModelState.Errors.Add(new ViewModelValidationError()
                {
                    ErrorMessage = "Incorrect credentials."
                });
                Context.FailOnInvalidModelState();
            }
        }

        public void CreateAccount()
        {
            // this code actually doesn't create any account - it's just a validation demo
            // you can use e.g. UserManager.CreateAsync from ASP.NET Core Identity

            if (CreateAccountForm.Email == "info@dotvvm.com")
            {
                // TODO: report the account already exists
                Context.ModelState.Errors.Add(new ViewModelValidationError()
                {
                    ErrorMessage = "The user account already exists!"
                });
                Context.FailOnInvalidModelState();
            } 
            else
            {
                Context.RedirectToRoute("Success");
            }
        }

    }
}
