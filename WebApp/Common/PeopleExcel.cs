using Datas.Models.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace WebApp.Common
{
    public class PeopleExcel
    {
        ILogger _logger;
        private string _path = string.Empty;
        public DateTime ImportDate { set; get; } = DateTime.Now;
        public string FileName { set; get; } = string.Empty;

        public List<PeopleModel> Datas { set; get; } = new List<PeopleModel>();
        /// <summary>
        /// Instance of this
        /// </summary>



        public PeopleExcel(ILogger logger,string path,int userId)
        {
            _logger = logger;
            _path = path + "/AppData/excel/" + userId + "/h";
        }

        public string GetPath()
        {
            return _path;
        }

        public void Read()
        {
            try
            {
                var filePath = Path.Combine(_path, "h.json");
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                if (System.IO.File.Exists(filePath))
                {
                    var value = JsonConvert.DeserializeObject<PeopleExcel>(System.IO.File.ReadAllText(filePath));
                    if (value != null)
                    {
                        ImportDate = value.ImportDate;
                        FileName = value.FileName;
                        Datas = value.Datas;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"");
            }
        }

        public bool Save()
        {
            try
            {
                var filePath = Path.Combine(_path, "h.json");
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(this));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }
    }
}
