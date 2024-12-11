﻿using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class InstrumentController(LogService logService, ILogger<InstrumentController> logger, AuthenUtils authenUtils, IWebHostEnvironment environment, InstrumentCategoryService InstrumentCategoryService, InstrumentService InstrumentService, PeopleService peopleService) : BaseController<InstrumentController>(logger, authenUtils, logService)
    {
        private IWebHostEnvironment _hostEnvironment = environment;
        private InstrumentCategoryService _InstrumentCategoryService = InstrumentCategoryService;
        private InstrumentService _InstrumentService = InstrumentService;
        private PeopleService _peopleService = peopleService;

        // GET: Admin/News
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new InstrumentPermission();
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_DELETE);
                permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_SHOW);
                ViewBag.Permission = permission;
                ViewBag.InstrumentCategories = GetListInstrumentCategory();
                ViewBag.Peoples = GetListPeople(true);
                return View();
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
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                var permission = new InstrumentPermission();
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_DELETE);
                permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_SHOW);
                ViewBag.Permission = permission;
                var excel = new InstrumentExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                var permission = new InstrumentPermission();
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_DELETE);
                permission.IsShow = CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_SHOW);
                ViewBag.Permission = permission;
                var excel = new GenericExcelHandler<InstrumentModel>(Log, _hostEnvironment.ContentRootPath, UserData.Id);
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


        private List<InstrumentModel> ReadExcelSheet(string filePath, bool isSave = false)
        {
            var countAdd = 0;
            var lstData = new List<InstrumentModel>();
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
                        var model = new InstrumentModel();
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
                                if (_InstrumentService.AddByCheckName(model).IsSuccess())
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

        private void SetBaseValue(InstrumentModel model)
        {
            var type = model.GetCategoryName();
            if (!string.IsNullOrEmpty(type))
            {
                var typeData = _InstrumentCategoryService.GetFirstOrDefault(o => o.Name.ToLower().Equals(type));
                if (typeData != null)
                {
                    model.CategoryId = typeData.Id;
                }
            }
            var people = model.GetPeopleName();
            if (!string.IsNullOrEmpty(type))
            {
                var peopleData = _peopleService.GetFirstOrDefault(o => o.Name.ToLower().Equals(people));
                if (peopleData != null)
                {
                    model.PeopleId = peopleData.Id;
                }
            }
        }

        private void SetData(InstrumentModel InstrumentModel, int index, Cell cellData, SharedStringTable sst)
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

        [Route("LoadAjaxNew")]
        public JsonResult LoadAjaxNew(string category, string people)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            IEnumerable<Instrument> list = null;
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
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            var totalRecord = 0;
            var total = _peopleService.GetAll().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_VIEW))
            {
                Expression<Func<Instrument, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.Normal;
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.CategoryId.HasValue && o.CategoryId == c);
                }
                if (p > 0)
                {
                    expression = expression.AndAlso1(o => o.PeopleId.HasValue && o.PeopleId == p);
                }
                if (!string.IsNullOrEmpty(term))
                {
                    expression = expression.AndAlso1(e => e.Content.ToLower().Contains(term) || e.Name.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Image360) && e.Image360.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Shape) && e.Shape.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.CurrentStatus) && e.CurrentStatus.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Classify) && e.Classify.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Certification) && e.Certification.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Material) && e.Material.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Color) && e.Color.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Size) && e.Size.ToLower().Contains(term)));
                }
                list = _InstrumentService.GetAll(expression);
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

        private Object BindData(IEnumerable<Instrument> list, int start, int length)
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
                    People = item.PeopleId.HasValue ? item.People.Name : "",
                    Material = item.Material,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        // GET: Admin/News/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                ViewBag.InstrumentCategories = GetListInstrumentCategory();
                ViewBag.Peoples = GetListPeople(true);
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        private List<SelectListItem> GetListPeople(bool getChild = false)
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _peopleService.GetAll(x => x.Status == Enums.ActiveStatus.Active && x.Parent == null).ToList())
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                if (getChild && item.Childrens != null)
                {
                    foreach (var child in item.Childrens.Where(x => x.Status == Enums.ActiveStatus.Active && x.DeleteStatus == Enums.DeleteStatus.Normal))
                    {
                        lstData.Add(new SelectListItem { Value = child.Id.ToString(), Text = "--- " + child.Name });
                    }
                }
            }
            return lstData;
        }

        private List<SelectListItem> GetListInstrumentCategory(int? InstrumentCategoryId = null)
        {
            Expression<Func<InstrumentCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (InstrumentCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != InstrumentCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _InstrumentCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreateInstrumentCategory(item, InstrumentCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateInstrumentCategory(InstrumentCategory InstrumentCategory, int? newsCategoryId, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = InstrumentCategory.Id.ToString(), Text = prefix + InstrumentCategory.Name });
            foreach (var item in InstrumentCategory.Childrens)
            {
                if (newsCategoryId.HasValue && !item.Id.Equals(newsCategoryId.Value))
                {
                    lstData.AddRange(CreateInstrumentCategory(item, newsCategoryId, prefix + "---"));
                }
                else if (!newsCategoryId.HasValue)
                {
                    lstData.AddRange(CreateInstrumentCategory(item, newsCategoryId, prefix + "---"));
                }
            }
            return lstData;
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
        public ActionResult Create(InstrumentModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                if (ModelState.IsValid)
                {
                    _InstrumentService.Add(model);
                    base.SuccessNotification("Thêm mới công cụ sản xuất thành công !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
                    ViewBag.Peoples = GetListPeople(true);
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
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT))
            {
                var data = _InstrumentService.GetById(id);

                if (data != null)
                {
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
                    ViewBag.Peoples = GetListPeople(true);
                    return View(data.ToModel());
                }
                base.ErrorNotification("Không tồn tại công cụ sản xuất!");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("Edit")]
        [AutoValidateAntiforgeryToken]
        public ActionResult Edit(InstrumentModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _InstrumentService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật trang phục thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.Peoples = GetListPeople(true);
                        ViewBag.InstrumentCategories = GetListInstrumentCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.Peoples = GetListPeople();
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
                    return View(model);
                }

            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("EditExcel")]
        public ActionResult EditExcel(int id)
        {

            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                var data = excel.Datas.FirstOrDefault(o => o.Id == id);
                if (data != null)
                {
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
                    ViewBag.Peoples = GetListPeople(true);
                    return View(data);
                }
                base.ErrorNotification("Không tồn tại công cụ sản xuất!");
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
        public ActionResult EditExcel(InstrumentModel model, int idExcel)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_EDIT))
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                    var result = _InstrumentService.Add(model);
                    if (result.IsSuccess())
                    {
                        var excel = new InstrumentExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                        excel.Read();
                        var data = excel.Datas.FirstOrDefault(o => o.Id == idExcel);
                        if (data != null)
                        {
                            excel.Datas.Remove(data);
                            excel.Save();
                        }
                        base.SuccessNotification("Thêm mới công cụ sản xuất từ excel thành công");
                        return RedirectToAction(nameof(ImportExcel));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.Peoples = GetListPeople(true);
                        ViewBag.InstrumentCategories = GetListInstrumentCategory();
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Peoples = GetListPeople(true);
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
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
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_DELETE))
            {
                message = new MessageResult();
                message = _InstrumentService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công công cụ sản xuất";
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
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUMENT_ADD))
            {
                message = new MessageResult();
                var excel = new PeopleExcel(Log, _hostEnvironment.ContentRootPath, UserData.Id);
                excel.Read();
                var data = excel.Datas.FirstOrDefault(o => o.Id == id);
                if (data != null)
                {
                    excel.Datas.Remove(data);
                    excel.Save();
                    message.Message = "Xóa thành công công cụ sản xuất từ excel";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);

        }
    }
}
