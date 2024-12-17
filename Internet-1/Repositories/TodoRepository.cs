using Internet_1.Models;

namespace Internet_1.Repositories
{
    public class TodoRepository : GenericRepository<Todo>
    {
        public TodoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
