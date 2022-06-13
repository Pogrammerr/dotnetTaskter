namespace TaskManagement.Models
{
    public class TaskModel
    {
        public int id { get; set; }

        public string topic { get; set; }

        public string details { get; set; }

        public string iletisim { get; set; }

        public string deadline { get; set; }

        public bool completed { get; set; }

    }
}
