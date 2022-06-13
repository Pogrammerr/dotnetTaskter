using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Pages
{
    public class YeniGorevModel : PageModel
    {
        public JsonTaskService jsonProjectService;
        public YeniGorevModel(JsonTaskService JsonProjectService)
        {
            jsonProjectService = JsonProjectService;
        }

        [BindProperty]
        public TaskModel task { get; set; }

        [BindProperty]
        public string SearchId { get; set; }

        [BindProperty]
        public string Memberlist { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPostForm()
        {
            jsonProjectService.AddTask(task);

            return RedirectToPage("/Index", new { Status = "Success" });

        }

        public void OnPostClear()
        {
            task = null;
            SearchId = "";
        }

        public void OnPostGetProjectbyID()
        {
            task = jsonProjectService.GetTaskbyID(Convert.ToInt32(SearchId));
        }

        public string[] stringtolist(string memberlist)
        {

            if (!string.IsNullOrEmpty(memberlist))
                return memberlist.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            else
                return Array.Empty<string>();

        }

        public string listtostring(string[] memberlist)
        {
            if (memberlist.Length == 0)
                return string.Join(Environment.NewLine, memberlist);
            else
                return "";


        }
    }
}
