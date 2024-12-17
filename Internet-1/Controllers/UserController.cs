using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using AutoMapper;
using Internet_1.Models;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NETCore.Encrypt.Extensions;
using System.Collections.Specialized;

namespace Internet_1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly INotyfService _notyf;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserController(IMapper mapper, IConfiguration config, INotyfService notyf, IFileProvider fileProvider, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {

            _mapper = mapper;
            _config = config;
            _notyf = notyf;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userModels = _mapper.Map<List<UserModel>>(users);
            return View(userModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserModel model)
        {

            var user = new AppUser();
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhotoUrl = "no-img.png";
            var identityResult = await _userManager.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);


                    _notyf.Error(item.Description);
                }

                return View(model);
            }
            // default olarak Uye rolü ekleme
            var user1 = await _userManager.FindByNameAsync(model.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Uye");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Uye" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user1, "Uye");
            _notyf.Success("Üye Eklendi");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userModel = _mapper.Map<UserModel>(user);
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);
            _notyf.Success("Üye Güncellendi");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userModel = _mapper.Map<UserModel>(user);
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                _notyf.Error("Yönetici Üye Silinemez!");
                return RedirectToAction("Index");
            }
            await _userManager.DeleteAsync(user);
            _notyf.Success("Üye Silindi");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.Users.Where(s => s.UserName == userName).FirstOrDefaultAsync();
            var userModel = _mapper.Map<RegisterModel>(user);
            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(RegisterModel model)
        {
            var userName = User.Identity.Name;
            var user = await _userManager.Users.Where(s => s.UserName == userName).FirstOrDefaultAsync();

            if (model.Password != model.PasswordConfirm)
            {
                _notyf.Error("Parola Tekrarı Tutarsız!");
                return RedirectToAction("Profile");
            }

            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;


            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "no-img.png";
            if (model.PhotoFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "userPhotos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;

            }

            user.PhotoUrl = photoUrl;


            await _userManager.UpdateAsync(user);
            _notyf.Success("Kullanıcı Bilgileri Güncellendi");

            return RedirectToAction("Index", "Admin");

        }

        public async Task<IActionResult> UserRole(string id)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var user = await _userManager.FindByIdAsync(id);
            var userRoleModels = new List<UserRoleModel>();
            foreach (var role in roles)
            {
                var userRoleModel = new UserRoleModel();
                userRoleModel.UserId = id;
                userRoleModel.RoleId = role.Id;
                userRoleModel.RoleName = role.Name;
                userRoleModel.IsInRole = await _userManager.IsInRoleAsync(user, role.Name);
                userRoleModels.Add(userRoleModel);
            }

            return View(userRoleModels);
        }

        public async Task<IActionResult> UserRoleAdd(string id, string userId)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("UserRole", new { id = userId });
        }
        public async Task<IActionResult> UserRoleDelete(string id, string userId)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveFromRoleAsync(user, role.Name);

            return RedirectToAction("UserRole", new { id = userId });
        }
    }
}

