using System.Threading.Tasks;
using WindowsFormsAppSummerPracticeMVC.Models;

namespace WindowsFormsAppSummerPracticeMVC.Controllers
{
    public class MainController
    {
        private readonly MainModel model = new MainModel();
        public async Task GetTextAsync(string requestUrl)
        {
            await model.GetRequestAsync<string>(requestUrl);
        }
    }
}