using elFinder.NetCore.Drivers.FileSystem;
using elFinder.NetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.EMMA;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/file-user")]
    public class FileUserController : Controller
    {
        IWebHostEnvironment _env;
        AuthenUtils _authenUtils;
        public FileUserController(IWebHostEnvironment env, AuthenUtils authenUtils)
        {
            _env = env;
            _authenUtils = authenUtils;
        }

        // Url để client-side kết nối đến backend
        // /el-finder-file-system/connector
        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var userInfo = _authenUtils.GetSessionUserInfo();
            if (userInfo != null)
            {
                var connector = GetConnector(userInfo.Id);
                return await connector.ProcessAsync(Request);
            }
            return Unauthorized();
        }

        // Địa chỉ để truy vấn thumbnail
        // /el-finder-file-system/thumb
        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var userInfo = _authenUtils.GetSessionUserInfo();
            if (userInfo != null)
            {
                var connector = GetConnector(userInfo.Id);
                return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
            }
            return Unauthorized();
          
        }

        private Connector GetConnector(int userId)
        {
            // Thư mục gốc lưu trữ là wwwwroot/files (đảm bảo có tạo thư mục này)
            string pathroot = "AppData\\files\\" + userId;

            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            // .. ... wwww/files
            string rootDirectory = Path.Combine(_env.ContentRootPath, pathroot);
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }

            // https://localhost:5001/files/
            string url = $"{uri.Scheme}://{uri.Authority}/{pathroot}/";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/Admin/file-user/thumb/";


            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "files", // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                //LockedFolders = new List<string>(new string[] { "Folder1" }
                ThumbnailSize = 100,
                UploadDeny = new List<string> { "text/html", "application/java-archive" , "text/javascript" , "application/javascript", "application/xhtml+xml", "application/vnd.microsoft.portable-executable" , "application/exe", "application/x-exe" , "application/dos-exe" }
            };


            driver.AddRoot(root);

            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
