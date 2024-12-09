using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services;
using System.Linq.Expressions;
using WebApp.Common;
using Common.Entity.Permission;
using Microsoft.AspNetCore.Http;
using Datas.Models.ViewModels;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class PeopleController(LogService logService, ILogger<PeopleController> logger, AuthenUtils authenUtils, PeopleService peopleService, PeopleCategoryService peopleCategoryService, WardService wardService, DistrictService districtService, LanguageService languageService, IWebHostEnvironment environment) : BaseController<PeopleController>(logger, authenUtils,logService)
    {
        private IWebHostEnvironment _hostEnvironment = environment;
        private PeopleCategoryService _peopleCategoryService = peopleCategoryService;
        private PeopleService _peopleService = peopleService;
        private WardService _wardService = wardService;
        private DistrictService _districtService = districtService;
        private LanguageService _languageService = languageService;

        private void GetListDistrict()
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _districtService.GetAll(x => x.Status == Enums.ActiveStatus.Active))
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Districts = lstData;
        }

        private void GetListLanguage()
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _languageService.GetAll(x => x.Status == Enums.ActiveStatus.Active))
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Languages = lstData;
        }

        private void BuildPermission()
        {
            var permission = new PeoplePermission();
            permission.IsView = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW);
            permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD);
            permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT);
            permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_DELETE);
            permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_SHOW);
            permission.IsViewRequest = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REQUEST);
            permission.IsViewConfirm = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_CONFIRM);
            permission.IsViewReject = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REJECT);
            permission.IsConfirm = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_CONFIRM);
            permission.IsRequest = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST);
            permission.IsReject = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REJECT);
            ViewBag.Permission = permission;
        }

        private void BuidDataIndex()
        {
            BuildPermission();
            ViewBag.PeopleCategories = GetListPeopleCategory();
            ViewBag.Peoples = GetListPeoples();
            GetListDistrict();
            GetListLanguage();
            GetListWard();
        }

        // GET: Admin/News
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PeoplePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW))
            {
                ViewBag.ConfirmStatus = Enums.PeopleConfirmStatus.Update;
                BuidDataIndex();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("IndexConfirm")]
        public ActionResult IndexConfirm()
        {
            var permission = new PeoplePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_CONFIRM))
            {
                ViewBag.ConfirmStatus = Enums.PeopleConfirmStatus.Confirm;
                BuidDataIndex();
                return View("Index");
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("IndexRequest")]
        public ActionResult IndexRequest()
        {
            var permission = new PeoplePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REQUEST))
            {
                ViewBag.ConfirmStatus = Enums.PeopleConfirmStatus.Request;
                BuidDataIndex();
                return View("Index");
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("IndexReject")]
        public ActionResult IndexReject()
        {
            var permission = new PeoplePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REJECT))
            {
                ViewBag.ConfirmStatus = Enums.PeopleConfirmStatus.Reject;
                BuidDataIndex();
                return View("Index");
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("IndexDelete")]
        public ActionResult IndexDelete()
        {
            var permission = new PeoplePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_DELETE))
            {
                ViewBag.IsDelete = true;
                BuidDataIndex();
                return View("Index");
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("ImportExcel")]
        public ActionResult ImportExcel()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                var permission = new PeoplePermission();
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_DELETE);
                permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_SHOW);
                ViewBag.Permission = permission;
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                ViewBag.Date = excel.ImportDate;
                ViewBag.FileName = excel.FileName;
                return View(excel.Datas);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("ImportExcel")]
        public ActionResult ImportExcel(IFormFile file, bool save = false)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                var permission = new PeoplePermission();
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_DELETE);
                permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_SHOW);
                ViewBag.Permission = permission;
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                if (file != null && file.Length > 0)
                {
                    Utilities.DeleteFile(Path.Combine(excel.GetPath(), excel.FileName));
                    SaveDataFile(file, excel.GetPath());
                    var data = ReadExcelSheet(Path.Combine(excel.GetPath(), Path.GetFileName(file.FileName)), save);
                    excel.Datas = data;
                    excel.FileName = file.FileName;
                    excel.ImportDate = DateTime.Now;
                    excel.Save();
                }
                else
                {
                    base.ErrorNotification("Bạn chưa chọn dữ liệu cần import");
                }

                ViewBag.Date = excel.ImportDate;
                ViewBag.FileName = excel.FileName;
                return View(excel.Datas);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        private bool SaveDataFile(IFormFile file, string pathFolder)
        {
            var result = false;
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(pathFolder, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return result;
        }


        private List<PeopleModel> ReadExcelSheet(string filePath, bool isSave = false)
        {
            var countAdd = 0;
            var lstData = new List<PeopleModel>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    Worksheet sheet = worksheetPart.Worksheet;
                    var rows = sheet.Descendants<Row>().ToList();
                    for (int i = 1; i < rows.Count(); i++)
                    {
                        var row = rows[i];
                        var cells = row.Elements<Cell>().ToList();
                        var model = new PeopleModel();
                        model.Id = i;
                        bool isAdd = cells.Count > 0;
                        for (int j = 0; j < cells.Count; j++)
                        {
                            var c = cells[j];
                            SetData(model, j, c, sst);
                            if (j == 1 && (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name)))
                            {
                                isAdd = false;
                                break;
                            }
                        }
                        if (isAdd)
                        {
                            if (isSave)
                            {
                                if (_peopleService.AddByCheckName(model).IsSuccess())
                                    countAdd++;
                                else
                                {
                                    SetBaseValue(model);
                                    lstData.Add(model);
                                }
                            }
                            else
                            {
                                SetBaseValue(model);
                                lstData.Add(model);
                            }
                        }

                    }
                }
            }
            ViewBag.CountAdd = countAdd;
            return lstData;
        }

        private void SetBaseValue(PeopleModel model)
        {
            var type = model.GetPeopleCategory();
            if (!string.IsNullOrEmpty(type))
            {
                var typeData = _peopleCategoryService.GetFirstOrDefault(o => o.Name.ToLower().Equals(type));
                if (typeData != null)
                {
                    model.PeopleCategoryId = typeData.Id;
                }
            }
        }

        private void SetData(PeopleModel peopleModel, int index, Cell cellData, SharedStringTable sst)
        {
            var value = string.Empty;
            if ((cellData.DataType != null) && (cellData.DataType == CellValues.SharedString))
            {
                int ssid = int.Parse(cellData.CellValue.Text);
                value = sst.ChildElements[ssid].InnerText;
            }
            else if (cellData.CellValue != null)
            {
                value = cellData.CellValue.Text;
            }
            var cellRef = cellData.CellReference.ToString().Substring(0, 1);
            switch (cellRef)
            {
                case "A":
                    break;
                //case "B":
                //    peopleModel.Name = value;
                //    break;
                //case "C":
                //    peopleModel.Address = value;
                //    break;
                //case "D":
                //    peopleModel.Phone = value;
                //    break;
                //case "E":
                //    peopleModel.Email = value;
                //    break;
                //case "F":
                //    peopleModel.Image360 = value;
                //    break;
                //case "G":
                //    peopleModel.Lat = value;
                //    break;
                //case "H":
                //    peopleModel.Long = value;
                //    break;
                //case "I":
                //    peopleModel.Details = value;
                //    break;
                //case "J":
                //    peopleModel.Content = value;
                //    break;
                //case "K":
                //    //if(!string.IsNullOrEmpty(value))
                //    //{
                //    //    int typeId = 0;
                //    //    if(int.TryParse(value,out typeId))
                //    //    {
                //    //        historicalRelicModel.HistoricalRelicTypeId = typeId;
                //    //    }
                //    //}
                //    peopleModel.SetHistoricalRelicType(value);
                //    break;
                //case "L":
                //    //if (!string.IsNullOrEmpty(value))
                //    //{
                //    //    int wardId = 0;
                //    //    if (int.TryParse(value, out wardId))
                //    //    {
                //    //        historicalRelicModel.WardId = wardId;
                //    //    }
                //    //}
                //    peopleModel.SetWard(value);
                //    break;
                //case "M":
                //    peopleModel.Image0 = value;
                //    break;
                //case "N":
                //    peopleModel.Image1 = value;
                //    break;
                //case "O":
                //    peopleModel.Image2 = value;
                //    break;
                //case "P":
                //    peopleModel.Image3 = value;
                //    break;
                //case "Q":
                //    peopleModel.Image4 = value;
                //    break;
                //case "R":
                //    peopleModel.Image5 = value;
                //    break;
                //case "S":
                //    peopleModel.Image6 = value;
                //    break;
                //case "T":
                //    peopleModel.Image7 = value;
                //    break;
                //case "U":
                //    peopleModel.Image8 = value;
                //    break;
                default:
                    break;
            }
        }

        private JsonResult LoadAjax(string category, string district, string ward, string people,bool isShowDelete, int type = 0)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            var orderColumnIndex = HttpContext.Request.Query["order[0][column]"].ToString();
            var orderColumnName = HttpContext.Request.Query["columns[" + orderColumnIndex + "][data]"].ToString();
            IEnumerable<People> list = null;
            if (term != null)
            {
                term = term.ToLower();
            }
            try
            {
                start = Int32.Parse(startParam);
                length = Int32.Parse(lengthParam);

            }
            catch (Exception)
            {
            }
            var confirmStatus = Enums.PeopleConfirmStatus.Update;
            if (Enum.IsDefined(typeof(Enums.PeopleConfirmStatus), type))
            {
                confirmStatus = (Enums.PeopleConfirmStatus)type;
            }
            var c = 0;
            var p = 0;
            var d = 0;
            var w = 0;
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            if (!string.IsNullOrEmpty(district))
                int.TryParse(district, out d);
            if (!string.IsNullOrEmpty(ward))
                int.TryParse(ward, out w);
            var totalRecord = 0;
            var total = isShowDelete ? _peopleService.GetAllByDelete().Count() : _peopleService.GetAll().Count();
            if ((CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW) && confirmStatus == Enums.PeopleConfirmStatus.Update) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_CONFIRM) && confirmStatus == Enums.PeopleConfirmStatus.Confirm) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REJECT) && confirmStatus == Enums.PeopleConfirmStatus.Reject) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Request))
            {
                Expression<Func<People, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.Normal && o.ConfirmStatus == confirmStatus;
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.PeopleCategoryId.HasValue && o.PeopleCategoryId == c);
                }
                if (d > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.DistrictId.HasValue && x.DistrictId == d));
                }
                if (w > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.WardId.HasValue && x.WardId == w));
                }
                if (p > 0)
                {
                    expression = expression.AndAlso1(o => o.ParentId.HasValue && o.ParentId == p);
                }
                if (!string.IsNullOrEmpty(term))
                {
                    expression = expression.AndAlso1(e => e.Content.ToLower().Contains(term) || e.Name.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.History) && e.History.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)));
                }
                bool isSort = true;
                Func<People, object> sortData = null;
                switch (orderColumnName)
                {
                    case "name":
                        sortData = o => o.Name;
                        break;
                    case "population":
                        sortData = o => o.Population;
                        break;
                    case "address":
                        sortData = o => o.Address;
                        break;
                    default:
                        isSort = false;
                        break;
                }
                if (isSort)
                {
                    if (orderDir.Equals("asc"))
                        list = isShowDelete ? _peopleService.GetAllByDelete(expression).OrderBy(sortData) : _peopleService.GetAll(expression).OrderBy(sortData);
                    else
                        list = isShowDelete ? _peopleService.GetAllByDelete(expression).OrderByDescending(sortData) : _peopleService.GetAll(expression).OrderByDescending(sortData);
                }
                else
                    list = isShowDelete ? _peopleService.GetAllByDelete(expression) : _peopleService.GetAll(expression);
                totalRecord = list.Count();
            }
            var result = new
            {
                recordsTotal = total,
                recordsFiltered = totalRecord,
                aaData = list != null ? BindData(list, start, length) : null
            };
            var jsonResult = Json(result);
            return jsonResult;
        }

        [Route("LoadAjaxNew")]
        public JsonResult LoadAjaxNew(string category, string district, string ward, string people, int type = 0)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            var orderColumnIndex = HttpContext.Request.Query["order[0][column]"].ToString();
            var orderColumnName = HttpContext.Request.Query["columns[" + orderColumnIndex + "][data]"].ToString();
            IEnumerable<People> list = null;
            if (term != null)
            {
                term = term.ToLower();
            }
            try
            {
                start = Int32.Parse(startParam);
                length = Int32.Parse(lengthParam);

            }
            catch (Exception)
            {
            }
            var confirmStatus = Enums.PeopleConfirmStatus.Update;
            if (Enum.IsDefined(typeof(Enums.PeopleConfirmStatus), type))
            {
                confirmStatus = (Enums.PeopleConfirmStatus)type;
            }
            var c = 0;
            var p = 0;
            var d = 0;
            var w = 0;
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            if (!string.IsNullOrEmpty(district))
                int.TryParse(district, out d);
            if (!string.IsNullOrEmpty(ward))
                int.TryParse(ward, out w);
            var totalRecord = 0;
            var total = _peopleService.GetAll().Count();
            if ((CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW) && confirmStatus == Enums.PeopleConfirmStatus.Update) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_CONFIRM) && confirmStatus == Enums.PeopleConfirmStatus.Confirm) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REJECT) && confirmStatus == Enums.PeopleConfirmStatus.Reject) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Request))
            {
                Expression<Func<People, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.Normal && o.ConfirmStatus == confirmStatus;
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.PeopleCategoryId.HasValue && o.PeopleCategoryId == c);
                }
                if (d > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.DistrictId.HasValue && x.DistrictId == d));
                }
                if (w > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.WardId.HasValue && x.WardId == w));
                }
                if (p > 0)
                {
                    expression = expression.AndAlso1(o => o.ParentId.HasValue && o.ParentId == p);
                }
                if (!string.IsNullOrEmpty(term))
                {
                    expression = expression.AndAlso1(e => e.Content.ToLower().Contains(term) || e.Name.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.History) && e.History.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)));
                }
                bool isSort = true;
                Func<People, object> sortData = null;
                switch (orderColumnName)
                {
                    case "name":
                        sortData = o => o.Name;
                        break;
                    case "population":
                        sortData = o => o.Population;
                        break;
                    case "address":
                        sortData = o => o.Address;
                        break;
                    default:
                        isSort = false;
                        break;
                }
                if (isSort)
                {
                    if (orderDir.Equals("asc"))
                        list = _peopleService.GetAll(expression).OrderBy(sortData);
                    else
                        list = _peopleService.GetAll(expression).OrderByDescending(sortData);
                }
                else
                    list = _peopleService.GetAll(expression);
                totalRecord = list.Count();
            }
            var result = new
            {
                recordsTotal = total,
                recordsFiltered = totalRecord,
                aaData = list != null ? BindData(list, start, length) : null
            };
            var jsonResult = Json(result);
            return jsonResult;
        }

        [Route("LoadAjaxByDelete")]
        public JsonResult LoadAjaxByDelete(string category, string district, string ward, string people)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            var orderColumnIndex = HttpContext.Request.Query["order[0][column]"].ToString();
            var orderColumnName = HttpContext.Request.Query["columns[" + orderColumnIndex + "][data]"].ToString();
            IEnumerable<People> list = null;
            if (term != null)
            {
                term = term.ToLower();
            }
            try
            {
                start = Int32.Parse(startParam);
                length = Int32.Parse(lengthParam);

            }
            catch (Exception)
            {
            }
            var c = 0;
            var p = 0;
            var d = 0;
            var w = 0;
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            if (!string.IsNullOrEmpty(district))
                int.TryParse(district, out d);
            if (!string.IsNullOrEmpty(ward))
                int.TryParse(ward, out w);
            var totalRecord = 0;
            var total = _peopleService.GetAllByDelete().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_DELETE))
            {
                Expression<Func<People, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.IsDelete;
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.PeopleCategoryId.HasValue && o.PeopleCategoryId == c);
                }
                if (d > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.DistrictId.HasValue && x.DistrictId == d));
                }
                if (w > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.WardId.HasValue && x.WardId == w));
                }
                if (p > 0)
                {
                    expression = expression.AndAlso1(o => o.ParentId.HasValue && o.ParentId == p);
                }
                if (!string.IsNullOrEmpty(term))
                {
                    expression = expression.AndAlso1(e => e.Content.ToLower().Contains(term) || e.Name.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.History) && e.History.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Population) && e.Population.ToLower().Contains(term)));
                }
                bool isSort = true;
                Func<People, object> sortData = null;
                switch (orderColumnName)
                {
                    case "name":
                        sortData = o => o.Name;
                        break;
                    case "population":
                        sortData = o => o.Population;
                        break;
                    case "address":
                        sortData = o => o.Address;
                        break;
                    default:
                        isSort = false;
                        break;
                }
                if (isSort)
                {
                    if (orderDir.Equals("asc"))
                        list = _peopleService.GetAllByDelete(expression).OrderBy(sortData);
                    else
                        list = _peopleService.GetAllByDelete(expression).OrderByDescending(sortData);
                }
                else
                    list = _peopleService.GetAllByDelete(expression);
                totalRecord = list.Count();
            }
            var result = new
            {
                recordsTotal = total,
                recordsFiltered = totalRecord,
                aaData = list != null ? BindData(list, start, length) : null
            };
            var jsonResult = Json(result);
            return jsonResult;
        }

        private Object BindData(IEnumerable<People> list, int start, int length)
        {
            var result = new List<object>();
            var index = start + 1;

            foreach (var item in list.Skip(start).Take(length))
            {
                result.Add(new
                {
                    item.Id,
                    Index = index,
                    Image = item.Image0,
                    Name = item.Name,
                    Population = item.Population,
                    Address = item.Address,
                    ConfirmStatus = item.ConfirmStatus
                });
                index++;
            }
            return result;
        }

        // GET: Admin/News/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                ViewBag.PeopleCategories = GetListPeopleCategory();
                ViewBag.Peoples = GetListPeoples();
                GetListDistrict();
                GetListLanguage();
                BuildPermission();
                GetListWard();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }


        private List<SelectListItem> GetListPeopleCategory(int? peopleCategoryId = null)
        {
            Expression<Func<PeopleCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (peopleCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != peopleCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _peopleCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreatePeopleCategory(item, peopleCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> GetListPeoples()
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _peopleService.GetAll(x=>x.Status == Enums.ActiveStatus.Active && x.Parent == null))
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            return lstData;
        }

        private List<SelectListItem> CreatePeopleCategory(PeopleCategory peopleCategory, int? newsCategoryId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = peopleCategory.Id.ToString(), Text = prefix + peopleCategory.Name });
            foreach (var item in peopleCategory.Childrens)
            {
                if (newsCategoryId.HasValue && !item.Id.Equals(newsCategoryId.Value))
                {
                    lstComany.AddRange(CreatePeopleCategory(item, newsCategoryId, prefix + "---"));
                }
                else if (!newsCategoryId.HasValue)
                {
                    lstComany.AddRange(CreatePeopleCategory(item, newsCategoryId, prefix + "---"));
                }
            }
            return lstComany;
        }


        private List<SelectListItem> NewsCategory(NewsCategory newsCategory, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = newsCategory.Id.ToString(), Text = prefix + newsCategory.Name });
            foreach (var item in newsCategory.Childrens)
            {
                lstData.AddRange(NewsCategory(item, prefix + "---"));
            }
            return lstData;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(PeopleModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                if (ModelState.IsValid)
                {
                    _peopleService.Add(model);
                    base.SuccessNotification("Thêm mới dân tộc thành công !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.PeopleCategories = GetListPeopleCategory();
                    ViewBag.Peoples = GetListPeoples();
                    BuildPermission();
                    GetListDistrict();
                    GetListLanguage();
                    //GetListWard(model.DistrictId);
                    return View(model);
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            var data = _peopleService.GetById(id);
            if (data != null)
            {
                var confirmStatus = data.ConfirmStatus;
                if ((CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT) && confirmStatus == Enums.PeopleConfirmStatus.Update) ||
(CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Update) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_CONFIRM) && confirmStatus == Enums.PeopleConfirmStatus.Confirm) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REJECT) && confirmStatus == Enums.PeopleConfirmStatus.Reject) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Request))
                {
                    ViewBag.PeopleCategories = GetListPeopleCategory();
                    ViewBag.Peoples = GetListPeoples();
                    BuildPermission();
                    GetListDistrict();
                    GetListLanguage();
                    //GetListWard(data.DistrictId);
                    return View(data.ToPeopleModel());
                }
                else
                {
                    base.ErrorNotification("Tài khoản không được cấp quyền này");
                    return RedirectToAction("Index", "Default");
                }

            }
            base.ErrorNotification("Không tồn tại dân tộc!");
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [Route("Edit")]
        [AutoValidateAntiforgeryToken]
        public ActionResult Edit(PeopleModel model)
        {
            var data = _peopleService.GetById(model.Id);
            if (data != null)
            {
                var confirmStatus = data.ConfirmStatus;
                if ((CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT) && confirmStatus == Enums.PeopleConfirmStatus.Update) ||
(CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Update) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_CONFIRM) && confirmStatus == Enums.PeopleConfirmStatus.Confirm) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REJECT) && confirmStatus == Enums.PeopleConfirmStatus.Reject) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && confirmStatus == Enums.PeopleConfirmStatus.Request))
                {
                    var changeStatus = Enums.PeopleConfirmStatus.Update;
                    if (model.IsConfirm == 1)
                    {
                        changeStatus = Enums.PeopleConfirmStatus.Request;
                    }
                    else if (model.IsConfirm == 2)
                    {
                        changeStatus = Enums.PeopleConfirmStatus.Confirm;
                    }
                    else if (model.IsConfirm == 3)
                    {
                        changeStatus = Enums.PeopleConfirmStatus.Reject;
                    }
                    if ((CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT) && changeStatus == Enums.PeopleConfirmStatus.Update)
                        ||
(CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && changeStatus == Enums.PeopleConfirmStatus.Update) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_CONFIRM) && changeStatus == Enums.PeopleConfirmStatus.Confirm) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REJECT) && changeStatus == Enums.PeopleConfirmStatus.Reject) || (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_COFIRM_REQUEST) && changeStatus == Enums.PeopleConfirmStatus.Request))
                    {
                        if (ModelState.IsValid)
                        {
                            var result = new MessageResult<Enums.PeopleConfirmStatus>();
                            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
                                result = _peopleService.Update(model);
                            else
                                result = _peopleService.UpdateConfirm(model);
                            if (result.IsSuccess())
                            {
                                base.SuccessNotification("Cập nhật dân tộc thành công");
                                if (result.Value == Enums.PeopleConfirmStatus.Confirm)
                                    return RedirectToAction(nameof(IndexConfirm));
                                else if (result.Value == Enums.PeopleConfirmStatus.Reject)
                                    return RedirectToAction(nameof(IndexReject));
                                else if (result.Value == Enums.PeopleConfirmStatus.Request)
                                    return RedirectToAction(nameof(IndexRequest));
                                else
                                    return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                base.ErrorNotification(result.Message);
                                ViewBag.PeopleCategories = GetListPeopleCategory();
                                ViewBag.Peoples = GetListPeoples();
                                BuildPermission();
                                GetListDistrict();
                                GetListLanguage();
                                return View(model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                            ViewBag.PeopleCategories = GetListPeopleCategory();
                            ViewBag.Peoples = GetListPeoples();
                            BuildPermission();
                            GetListDistrict();
                            GetListLanguage();
                            //GetListWard(model.DistrictId);
                            return View(model);
                        }
                    }
                    else
                    {
                        base.ErrorNotification("Tài khoản không được cấp quyền này");
                        return RedirectToAction("Index", "Default");
                    }

                }
                else
                {
                    base.ErrorNotification("Tài khoản không được cấp quyền này");
                    return RedirectToAction("Index", "Default");
                }

            }
            base.ErrorNotification("Không tồn tại dân tộc!");
            return RedirectToAction(nameof(Index));
        }

        [Route("EditExcel")]
        public ActionResult EditExcel(int id)
        {

            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                var data = excel.Datas.FirstOrDefault(o => o.Id == id);
                if (data != null)
                {
                    ViewBag.PeopleCategories = GetListPeopleCategory();
                    ViewBag.Peoples = GetListPeoples();
                    GetListDistrict();
                    GetListLanguage();
                    //GetListWard(data.DistrictId);
                    return View(data);
                }
                base.ErrorNotification("Không tồn tại dân tộc!");
                return RedirectToAction(nameof(ImportExcel));
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("EditExcel")]
        public ActionResult EditExcel(PeopleModel model, int idExcel)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                    var result = _peopleService.Add(model);
                    if (result.IsSuccess())
                    {
                        var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                        excel.Read();
                        var data = excel.Datas.FirstOrDefault(o => o.Id == idExcel);
                        if (data != null)
                        {
                            excel.Datas.Remove(data);
                            excel.Save();
                        }
                        base.SuccessNotification("Thêm mới dân tộc từ excel thành công");
                        return RedirectToAction(nameof(ImportExcel));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.PeopleCategories = GetListPeopleCategory();
                        ViewBag.Peoples = GetListPeoples();
                        GetListDistrict();
                        GetListLanguage();
                        //GetListWard(model.DistrictId);
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.PeopleCategories = GetListPeopleCategory();
                    ViewBag.Peoples = GetListPeoples();
                    //GetListWard(model.DistrictId);
                    GetListDistrict();
                    GetListLanguage();
                    return View(model);
                }

            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_DELETE))
            {
                message = new MessageResult();
                message = _peopleService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công dân tộc";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);

        }

        [HttpPost]
        [Route("UnDelete")]
        public JsonResult UnDelete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW_DELETE))
            {
                message = new MessageResult();
                message = _peopleService.UnDelete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Khôi phục thành công dân tộc";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);

        }

        [HttpPost]
        [Route("DeleteExcel")]
        public JsonResult DeleteExcel(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_ADD))
            {
                message = new MessageResult();
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                var data = excel.Datas.FirstOrDefault(o => o.Id == id);
                if (data != null)
                {
                    excel.Datas.Remove(data);
                    excel.Save();
                    message.Message = "Xóa thành công dân tộc từ excel";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);

        }

        [HttpPost]
        [Route("GetWard")]
        public ActionResult GetWard(int? id)
        {
            var message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
            {
                GetListWard(id);
                return PartialView("_DisplayWard");
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
                Response.StatusCode = 500;
            }
            return Json(message);
        }

        [HttpPost]
        [Route("GetWardArea")]
        public ActionResult GetWardArea(int? id)
        {
            var message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
            {
                var lstData = new List<SelectListItem>();
                if (id.HasValue)
                {
                    foreach (var item in _wardService.GetAll(x => x.Status == Enums.ActiveStatus.Active && x.DistrictId.HasValue && x.DistrictId == id))
                    {
                        lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                    }
                }
                ViewBag.WardArea = lstData;
                return PartialView("_DisplayWardArea");
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
                Response.StatusCode = 500;
            }
            return Json(message);
        }

        [Route("LoadConfirmHistory")]
        [HttpPost]
        public ActionResult LoadConfirmHistory(int id)
        {
            var h = new List<PeopleConfirm>();
            var data = _peopleService.GetById(id);
            if (data != null)
            {
                h = data.PeopleConfirmes.OrderByDescending(o=>o.UpdateDate).ToList();
            }
            return PartialView("_LoadConfirmHistory",h);
        }

        [HttpPost]
        [Route("GetWardSearch")]
        public ActionResult GetWardSearch(int? id)
        {
            var message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
            {
                GetListWard(id);
                return PartialView("_DisplayWardSearch");
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
                Response.StatusCode = 500;
            }
            return Json(message);
        }

        private void GetListWard(int? dis = null)
        {
            var lstData = new List<SelectListItem>();
            if(dis.HasValue)
            {
                foreach (var item in _wardService.GetAll(x => x.Status == Enums.ActiveStatus.Active && x.DistrictId.HasValue && x.DistrictId == dis))
                {
                    lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                }
            }    
            ViewBag.Wards = lstData;
        }
    }
}
