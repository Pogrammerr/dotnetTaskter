using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, JsonTaskService JsonProjectService)
        {
            _logger = logger;
            jsonProjectService = JsonProjectService;
        }


        JsonTaskService jsonProjectService;


        public List<TaskModel> Projects;

        public void OnGet()
        {
            Projects = jsonProjectService.GetTasks();
        }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }


        [BindProperty]
        public string PokemonName { get; set; }

        [BindProperty]
        public string photoPath { get; set; }

        public void OnPostProcessRequestAsync()
        {
            Console.WriteLine(PokemonName);

            photoPath = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + PokemonName + ".png";

        }

        [BindProperty]
        public string Term { get; set; }

        [BindProperty]
        public string Data { get; set; }

        WikiModel wikidata;



        public void OnPost(JsonWikiService jsonWikiService)
        {
            Console.WriteLine(Term);

            wikidata = jsonWikiService.GetWikiModel(Term);

            Data = wikidata.age.ToString();
        }

        public void CompleteTask(TaskModel task)
        {
            List<TaskModel> tasks = jsonProjectService.GetTasks();

            TaskModel query = tasks.FirstOrDefault(x => x.id == task.id);
            if (query != null)
            {
                tasks[tasks.FindIndex(x => x.id == task.id)].completed = true;
                jsonProjectService.JsonWriter(tasks, true);
            }
        }

        public void OnButtonClick(TaskModel task)
        {
            List<TaskModel> tasks = jsonProjectService.GetTasks();

            TaskModel query = tasks.FirstOrDefault(x => x.id == task.id);
            if (query != null)
            {
                tasks[tasks.FindIndex(x => x.id == task.id)].completed = true;
                jsonProjectService.JsonWriter(tasks, true);
            }

        }

    }
}