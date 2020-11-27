using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoFinalProgIII.Data;

namespace ProyectoFinalProgIII.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Usuarios> _userManager;
        private readonly SignInManager<Usuarios> _signInManager;

        public IndexModel(
            UserManager<Usuarios> userManager,
            SignInManager<Usuarios> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Nombre y Apellido")]
            public string Nombre { get; set; }

            [Display(Name = "Nombre Comercial")]
            public string NombreComercial { get; set; }
            [Display(Name = "Direccion")]
            public string Direccion { get; set; }

            [Display(Name = "Link Pagina Web")]
            public string PaginaWeb { get; set; }

            [Phone]
            [Display(Name = "Numero de Telefono")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Usuarios user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var _User = await _userManager.GetUserAsync(User);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = _User.Nombre,
                NombreComercial = _User.NombreComercial,
                Direccion = _User.Direccion,
                PaginaWeb = _User.PaginaWeb
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var _user = await _userManager.GetUserAsync(User);
            bool modModel = false;
            if (Input.Nombre != _user.Nombre)
            {
                _user.Nombre = Input.Nombre;
                modModel = true;
            }
            if (Input.NombreComercial != _user.NombreComercial)
            {
                _user.NombreComercial = Input.NombreComercial;
                modModel = true;
            }
            if (Input.Direccion != _user.Direccion)
            {
                _user.Direccion = Input.Direccion;
                modModel = true;
            }
            if (Input.PaginaWeb != _user.PaginaWeb)
            {
                _user.PaginaWeb = Input.PaginaWeb;
                modModel = true;
            }
            if (modModel)
            {
                await _userManager.UpdateAsync(_user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
