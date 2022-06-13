using System.Text.Json;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class JsonTaskService
    {

        public JsonTaskService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "tasks.json"); }

        }


        public List<TaskModel> GetTasks()
        {
            using var json = File.OpenText(JsonFileName);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<TaskModel[]>(json.ReadToEnd()).ToList();

        }

        public void AddTask(TaskModel newTask)
        {
            List<TaskModel> tasks = GetTasks();
            newTask.id = tasks.Max(x => x.id) + 1;

            tasks.Add(newTask);

            using var json = File.OpenWrite(JsonFileName);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize(jsonwriter, tasks);
        }

        public TaskModel GetTaskbyID(int Id)
        {
            List<TaskModel> tasks = GetTasks();
            return tasks.FirstOrDefault(x => x.id == Id);
        }

        public void JsonWriter(List<TaskModel> projects, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize(jsonwriter, projects);
            json.Close();
        }



    }
}

