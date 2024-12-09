using Datas.Models.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace WebApp.Common
{
    public class AdminSettingUtils
    {
        public AdminSettingModel Setting { set; get; } = new AdminSettingModel();
        /// <summary>
        /// Instance of this
        /// </summary>
        //private readonly ILogger _logger = logger;
        private readonly IWebHostEnvironment _environment;

        public AdminSettingUtils(IWebHostEnvironment environment)
        {
            _environment = environment;
            Read();
        }

        public void Read()
        {
            try
            {
                var localPath = _environment.ContentRootPath + "/AppData/";
                var filePath = Path.Combine(localPath, "admin-settings.json");
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                if (System.IO.File.Exists(filePath))
                {
                    Setting = JsonConvert.DeserializeObject<AdminSettingModel>(System.IO.File.ReadAllText(filePath));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex,"");
            }
        }

        public bool Save(AdminSettingModel setting)
        {
            try
            {
                var localPath = _environment.ContentRootPath + "/AppData/";
                var filePath = Path.Combine(localPath, "admin-settings.json");
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(setting));
                Setting = setting;
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "");
            }
            return false;
        }
    }
}
