namespace Internet_1.ViewModels
{
    public class TodoModel:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IsOK { get; set; }
    }
}
