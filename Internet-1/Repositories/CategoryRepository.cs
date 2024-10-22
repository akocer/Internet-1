using AutoMapper;
using Internet_1.Models;
using Internet_1.ViewModels;

namespace Internet_1.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryModel> GetList()
        {
            var categories = _context.Categories.ToList();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            return categoryModels;
        }
        public CategoryModel GetById(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).FirstOrDefault();
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return categoryModel;
        }
        public void Add(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(CategoryModel model)
        {
            var category = _context.Categories.Where(s => s.Id == model.Id).FirstOrDefault();
            if (category != null)
            {
                category.Name = model.Name;
              
                category.IsActive = model.IsActive;

                _context.Categories.Update(category);
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).FirstOrDefault();
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
