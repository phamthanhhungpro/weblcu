using Datas.Models.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace WebApp.Common
{
    public class SettingUtils
    {
        public SettingModel Setting { set; get; }
        public string SubDomain { set; get; } = string.Empty;
        /// <summary>
        /// Instance of this
        /// </summary>
        //private readonly ILogger _logger = logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _iConfig;

        public SettingUtils(IWebHostEnvironment environment, IConfiguration iConfig)
        {
            _environment = environment;
            _iConfig = iConfig;
            var sub = _iConfig.GetValue<string>("Settings:SubDomain");
            if (!string.IsNullOrEmpty(sub))
                SubDomain = sub;
            Read();

        }

        public bool IsSendMail()
        {
            return !string.IsNullOrEmpty(Setting.EmailAccount) & !string.IsNullOrEmpty(Setting.EmailPass) & !string.IsNullOrEmpty(Setting.EmailPort) & !string.IsNullOrEmpty(Setting.EmailServer) & !string.IsNullOrEmpty(Setting.EmailName);
        }

        public void Read()
        {
            try
            {
                var localPath = _environment.ContentRootPath + "/AppData/";
                var filePath = Path.Combine(localPath, "settings.json");
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                if (System.IO.File.Exists(filePath))
                {
                    Setting = JsonConvert.DeserializeObject<SettingModel>(System.IO.File.ReadAllText(filePath));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex,"");
            }
        }

        public bool Save(SettingModel setting)
        {
            try
            {
                var localPath = _environment.ContentRootPath + "/AppData/";
                var filePath = Path.Combine(localPath, "settings.json");
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
