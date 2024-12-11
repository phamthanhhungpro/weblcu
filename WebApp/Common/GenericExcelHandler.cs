using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace WebApp.Common
{
    public class GenericExcelHandler<T> where T : class
    {
        private readonly ILogger _logger;
        private readonly string _path;

        public DateTime ImportDate { get; set; } = DateTime.Now;
        public string FileName { get; set; } = string.Empty;
        public List<T> Datas { get; set; } = new List<T>();

        /// <summary>
        /// Constructor to initialize GenericExcelHandler
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="path">Base path for storing files</param>
        /// <param name="userId">Unique identifier for the user</param>
        public GenericExcelHandler(ILogger logger, string path, int userId)
        {
            _logger = logger;
            _path = Path.Combine(path, "AppData", "excel", userId.ToString());
        }

        /// <summary>
        /// Gets the storage path
        /// </summary>
        /// <returns>Path as a string</returns>
        public string GetPath()
        {
            return _path;
        }

        /// <summary>
        /// Reads data from the JSON file
        /// </summary>
        public void Read()
        {
            try
            {
                var filePath = Path.Combine(_path, "data.json");
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                if (File.Exists(filePath))
                {
                    var value = JsonConvert.DeserializeObject<GenericExcelHandler<T>>(File.ReadAllText(filePath));
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
                _logger.LogError(ex, "Error reading data from JSON file.");
            }
        }

        /// <summary>
        /// Saves the current state to the JSON file
        /// </summary>
        /// <returns>True if successful, otherwise false</returns>
        public bool Save()
        {
            try
            {
                var filePath = Path.Combine(_path, "data.json");
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                File.WriteAllText(filePath, JsonConvert.SerializeObject(this));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving data to JSON file.");
            }
            return false;
        }
    }
}
