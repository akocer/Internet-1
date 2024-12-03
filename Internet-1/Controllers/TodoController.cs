using AutoMapper;
using Internet_1.Models;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Internet_1.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;
        ResultModel resultModel = new ResultModel();
        public TodoController(TodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListAjax()
        {
            var todos = await _todoRepository.GetAllAsync();
            var todoModels = _mapper.Map<List<TodoModel>>(todos);
            return Json(todoModels);
        }

        public async Task<IActionResult> GetByIdAjax(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            var todoModel = _mapper.Map<TodoModel>(todo);
            return Json(todoModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateAjax(TodoModel model)
        {
            if (model.Id == 0)
            {
                var todo = new Todo();
                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.IsOK = model.IsOK;
                todo.IsActive = true;
                todo.Created = DateTime.Now;
                todo.Updated = DateTime.Now;
                await _todoRepository.AddAsync(todo);
                resultModel.Status = true;
                resultModel.Message = "Görev Eklendi";
            }
            else
            {
                var todo = await _todoRepository.GetByIdAsync(model.Id);
                if (todo == null)
                {
                    resultModel.Status = false;
                    resultModel.Message = "Kayıt Bulunamadı!";
                    return Json(resultModel);
                }
                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.IsOK = model.IsOK;
                todo.Updated = DateTime.Now;
                await _todoRepository.UpdateAsync(todo);
                resultModel.Status = true;
                resultModel.Message = "Görev Düzenlendi";
            }

            return Json(resultModel);
        }

        public async Task<IActionResult> DeleteAjax(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
            {
                resultModel.Status = false;
                resultModel.Message = "Kayıt Bulunamadı!";
                return Json(resultModel);
            }
            await _todoRepository.DeleteAsync(id);
            resultModel.Status = true;
            resultModel.Message = "Görev Silindi";
            return Json(resultModel);
        }
    }
}