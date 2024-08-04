using DataInitializerApp.InitialData;
using DataInitializerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DataInitializerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("InsertData")]
        [HttpPost]
        public bool InsertData([FromBody] DataValuesViewModel model)
        {
            var success = false;
            try
            {
                SyncData.InitializeCorrect(model.PassPhrase, model.matchCards);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        [Route("GetData")]
        [HttpGet]
        public List<string> GetData()
        {
            var getAll = SyncData.GetCorrectPassPhrase();
            if (getAll.Count < 12)
            {
                Thread.Sleep(500);
                GetData();
            }
            var passPhrase = SyncData.GetCorrectPassPhrase().OrderBy(row => row.RowIndex).Select(row => row.Keyword).ToList();
            return passPhrase;
        }

        [Route("GetCurrentData")]
        [HttpGet]
        public List<string> GetCurrentData()
        {
            return SyncData.GetCorrectPassPhrase().OrderBy(row => row.RowIndex).Select(row => row.Keyword).ToList();
        }

        [Route("ResetData")]
        [HttpPost]
        public bool ResetData()
        {
            var success = false;
            try
            {
                SyncData.ResetList();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }
    }
}