using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using AutoMapper;
using Internet_1.Models;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly INotyfService _notyf;
        private readonly IFileProvider _fileProvider;
        public UserController(UserRepository userRepository, IMapper mapper, IConfiguration config, INotyfService notyf, IFileProvider fileProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _notyf = notyf;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
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
            if (_userRepository.Where(s => s.UserName == model.UserName).Count() > 0)
            {
                _notyf.Error("Girilen Kullanıcı Adı Kayıtlıdır!");
                return View(model);
            }
            if (_userRepository.Where(s => s.Email == model.Email).Count() > 0)
            {
                _notyf.Error("Girilen E-Posta Adresi Kayıtlıdır!");
                return View(model);
            }
            var user = new User();
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Created = DateTime.Now;
            user.Updated = DateTime.Now;
            user.PhotoUrl = "no-img.png";
            user.Password = MD5Hash(model.Password);
            user.Role = model.Role;
            await _userRepository.AddAsync(user);
            _notyf.Success("Üye Eklendi");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userModel = _mapper.Map<UserModel>(user);
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Role = model.Role;
            user.Updated = DateTime.Now;

            await _userRepository.UpdateAsync(user);
            _notyf.Success("Üye Güncellendi");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userModel = _mapper.Map<UserModel>(user);
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserModel model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);

            if (user.Role == "Admin")
            {
                _notyf.Error("Yönetici Üye Silinemez!");
                return RedirectToAction("Index");
            }
            await _userRepository.DeleteAsync(model.Id);
            _notyf.Success("Üye Silindi");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var userName = User.Claims.First(c => c.Type == "UserName").Value;
            var user = await _userRepository.Where(s => s.UserName == userName).FirstOrDefaultAsync();
            var userModel = _mapper.Map<RegisterModel>(user);
            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(RegisterModel model)
        {
            var userName = User.Claims.First(c => c.Type == "UserName").Value;
            var user = await _userRepository.Where(s => s.UserName == userName).FirstOrDefaultAsync();

            if (model.Password != model.PasswordConfirm)
            {
                _notyf.Error("Parola Tekrarı Tutarsız!");
                return RedirectToAction("Profile");
            }
            if (_userRepository.Where(s => s.UserName == model.UserName && s.Id != user.Id).Count() > 0)
            {
                _notyf.Error("Girilen Kullanıcı Adı Kayıtlıdır!");
                return View(model);
            }
            if (_userRepository.Where(s => s.Email == model.Email && s.Id != user.Id).Count() > 0)
            {
                _notyf.Error("Girilen E-Posta Adresi Kayıtlıdır!");
                return View(model);
            }


            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Updated = DateTime.Now;

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
            user.Password = MD5Hash(model.Password);


            await _userRepository.UpdateAsync(user);
            _notyf.Success("Kullanıcı Bilgileri Güncellendi");

            return RedirectToAction("Index", "Admin");

        }

        public string MD5Hash(string pass)
        {
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }


    }
}

