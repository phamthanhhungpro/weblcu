using Common;
using Datas.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public class DataInitializer(DataContext context)
    {
        private readonly DataContext _context = context;

        public void Run()
        {
            //var anc=  _context.Database.EnsureDeleted();
            var bcd = _context.Database.EnsureCreated();
            //_context.Database.Migrate();
            if (bcd)
            {
                GenerateDatabase();
            }
            else
            {
                _context.Database.Migrate();
                UpdateV1();
                UpdateV2();
                UpdateV3();
                UpdateV4();
                UpdateV5();
                UpdateV6();
                UpdateV7();
                UpdateV8();
            }
        }

        private void UpdateV1()
        {
            if (!_context.GroupFunctions.Any(o => o.Type == Enums.GroupFunctionType.Report))
            {
                var lstGroupFunction = new List<GroupFunction>
            {
                new GroupFunction{Name="Báo cáo",Order=6, Type=Enums.GroupFunctionType.Report},
                new GroupFunction{Name="Thành phố - Huyện",Order=23, Type=Enums.GroupFunctionType.District},
                new GroupFunction{Name="Phường - Xã",Order=24, Type=Enums.GroupFunctionType.Ward}

            };
                _context.GroupFunctions.AddRange(lstGroupFunction);
                _context.SaveChanges();
                var lstFunction = new List<Function> {


                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_REPORT_VIEW,Group = lstGroupFunction[0] },

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_DISTRICT_VIEW,Group = lstGroupFunction[1]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_DISTRICT_ADD,Group = lstGroupFunction[1]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_DISTRICT_EDIT,Group = lstGroupFunction[1]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_DISTRICT_DELETE,Group = lstGroupFunction[1]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_WARD_VIEW,Group = lstGroupFunction[2]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_WARD_ADD,Group = lstGroupFunction[2]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_WARD_EDIT,Group = lstGroupFunction[2]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_WARD_DELETE,Group = lstGroupFunction[2]},

            };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV2()
        {
            if (!_context.GroupFunctions.Any(o => o.Type == Enums.GroupFunctionType.Language))
            {
                var lstGroupFunction = new List<GroupFunction>
            {
                new GroupFunction{Name="Ngôn ngữ",Order=25, Type=Enums.GroupFunctionType.Language}

            };
                _context.GroupFunctions.AddRange(lstGroupFunction);
                _context.SaveChanges();
                var lstFunction = new List<Function> {

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_LANGUAGE_VIEW,Group = lstGroupFunction[0]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_LANGUAGE_ADD,Group = lstGroupFunction[0]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_LANGUAGE_EDIT,Group = lstGroupFunction[0]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_LANGUAGE_DELETE,Group = lstGroupFunction[0]},

            };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV3()
        {
            if (!_context.GroupFunctions.Any(o => o.Type == Enums.GroupFunctionType.File))
            {
                var lstGroupFunction = new List<GroupFunction>
            {
                new GroupFunction{Name="Tài liệu",Order=5, Type=Enums.GroupFunctionType.File}

            };
                _context.GroupFunctions.AddRange(lstGroupFunction);
                _context.SaveChanges();
                var lstFunction = new List<Function> {

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_FILE_VIEW,Group = lstGroupFunction[0]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_FILE_ADD,Group = lstGroupFunction[0]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_FILE_EDIT,Group = lstGroupFunction[0]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_FILE_DELETE,Group = lstGroupFunction[0]},

            };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV4()
        {
            var groupFunction = _context.GroupFunctions.Include(o => o.Functions).FirstOrDefault(o => o.Type == Enums.GroupFunctionType.People);
            if (groupFunction != null && !groupFunction.Functions.Any(o => o.FunctionCode.Equals(Common.Constants.PERMISSION_PEOPLE_COFIRM_REQUEST)))
            {
                var lstFunction = new List<Function> {

                    new Function{Name="Gửi duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_COFIRM_REQUEST,Group = groupFunction},
                    new Function{Name="Duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_COFIRM_CONFIRM,Group = groupFunction},
                    new Function{Name="Khôi phục dữ liệu hủy duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_COFIRM_REJECT,Group = groupFunction},
                };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }

            if (groupFunction != null && !groupFunction.Functions.Any(o => o.FunctionCode.Equals(Common.Constants.PERMISSION_PEOPLE_VIEW_REQUEST)))
            {
                var lstFunction = new List<Function> {

                    new Function{Name="Xem - Chờ duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_VIEW_REQUEST,Group = groupFunction},
                    new Function{Name="Xem - Đã duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_VIEW_CONFIRM,Group = groupFunction},
                    new Function{Name="Xem - Hủy duyệt",FunctionCode=Common.Constants.PERMISSION_PEOPLE_VIEW_REJECT,Group = groupFunction},
                };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV5()
        {
            var groupFunction = _context.GroupFunctions.Include(o => o.Functions).FirstOrDefault(o => o.Type == Enums.GroupFunctionType.People);
            if (groupFunction != null && !groupFunction.Functions.Any(o => o.FunctionCode.Equals(Common.Constants.PERMISSION_PEOPLE_VIEW_DELETE)))
            {
                var lstFunction = new List<Function> {

                    new Function{Name="Khôi phục dữ liệu đã xóa",FunctionCode=Common.Constants.PERMISSION_PEOPLE_VIEW_DELETE,Group = groupFunction},
                };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV6()
        {
            if (!_context.GroupFunctions.Any(o => o.Type == Enums.GroupFunctionType.Log))
            {
                var lstGroupFunction = new List<GroupFunction>
            {
                new GroupFunction{Name="Nhật ký HT",Order=45, Type=Enums.GroupFunctionType.Log}

            };
                _context.GroupFunctions.AddRange(lstGroupFunction);
                _context.SaveChanges();
                var lstFunction = new List<Function> {

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_LOG_VIEW,Group = lstGroupFunction[0]},

            };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV7()
        {
            var groupFunction = _context.GroupFunctions.Include(o => o.Functions).FirstOrDefault(o => o.Type == Enums.GroupFunctionType.Log);
            if (groupFunction != null && !groupFunction.Functions.Any(o => o.FunctionCode.Equals(Common.Constants.PERMISSION_LOG_SETTING)))
            {
                var lstFunction = new List<Function> {

                    new Function{Name="Kiểm soát truy cập",FunctionCode=Common.Constants.PERMISSION_LOG_SETTING,Group = groupFunction},
                };
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();
                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void UpdateV8()
        {
            AddGroupFunctionWithFunctions(
                Enums.GroupFunctionType.InstrumentCategory,
                "Danh mục nhạc cụ",
                26,
                [
                    ("Xem", Common.Constants.PERMISSION_INSTRUMENT_CATEGORY_VIEW),
                    ("Thêm mới", Common.Constants.PERMISSION_INSTRUMENT_CATEGORY_ADD),
                    ("Sửa", Common.Constants.PERMISSION_INSTRUMENT_CATEGORY_EDIT),
                    ("Xóa", Common.Constants.PERMISSION_INSTRUMENT_CATEGORY_DELETE)
                ]);

            AddGroupFunctionWithFunctions(
                Enums.GroupFunctionType.ProduceToolCategory,
                "Danh mục công cụ sản xuất",
                27,
                [
                    ("Xem", Common.Constants.PERMISSION_PRODUCETOOL_CATEGORY_VIEW),
                    ("Thêm mới", Common.Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD),
                    ("Sửa", Common.Constants.PERMISSION_PRODUCETOOL_CATEGORY_EDIT),
                    ("Xóa", Common.Constants.PERMISSION_PRODUCETOOL_CATEGORY_DELETE)
                ]);

            AddGroupFunctionWithFunctions(
                Enums.GroupFunctionType.ProduceTool,
                "Công cụ sản xuất",
                8,
                [
                    ("Xem", Common.Constants.PERMISSION_PRODUCETOOL_VIEW),
                    ("Thêm mới", Common.Constants.PERMISSION_PRODUCETOOL_ADD),
                    ("Sửa", Common.Constants.PERMISSION_PRODUCETOOL_EDIT),
                    ("Xóa", Common.Constants.PERMISSION_PRODUCETOOL_DELETE)
                ]);


            AddGroupFunctionWithFunctions(
                Enums.GroupFunctionType.CustomsTradition,
                "Phong tục tập quán",
                9,
                [
                    ("Xem", Common.Constants.PERMISSION_CUSTOMSTRADITION_VIEW),
                    ("Thêm mới", Common.Constants.PERMISSION_CUSTOMSTRADITION_ADD),
                    ("Sửa", Common.Constants.PERMISSION_CUSTOMSTRADITION_EDIT),
                    ("Xóa", Common.Constants.PERMISSION_CUSTOMSTRADITION_DELETE)
                ]);
        }

        private void AddGroupFunctionWithFunctions(Enums.GroupFunctionType type, string groupName, int order, List<(string Name, string FunctionCode)> functions)
        {
            if (!_context.GroupFunctions.Any(o => o.Type == type))
            {
                var groupFunction = new GroupFunction { Name = groupName, Order = order, Type = type };
                _context.GroupFunctions.Add(groupFunction);
                _context.SaveChanges();

                var lstFunction = functions.Select(f => new Function { Name = f.Name, FunctionCode = f.FunctionCode, Group = groupFunction }).ToList();
                _context.Functions.AddRange(lstFunction);
                _context.SaveChanges();

                var adminRole = _context.Roles.Include("RoleFunctions").FirstOrDefault(o => o.Id == 1);
                if (adminRole != null)
                {
                    foreach (var item in lstFunction)
                    {
                        adminRole.RoleFunctions.Add(item);
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void GenerateDatabase()
        {
            var company = new Company
            {
                Name = "Quản trị phần mềm"
            };
            _context.Companies.Add(company);
            var dePartment = new Department { Name = "Quản trị hệ thống", DepartmentCompany = company };
            _context.Departments.Add(dePartment);
            var position = new Position { Name = "Admin" };
            _context.Positions.Add(position);
            _context.SaveChanges();
            var user = new User
            {
                UserName = "admin",
                FullName = "Administrator",
                UserCompany = company,
                DepartmentCompany = dePartment,
                UserPosition = position,
                Password = Utilities.CalculateMD5Hash("Admin@123"),
                SuperAdmin = true
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            var lstGroupFunction = new List<GroupFunction>
            {
                new GroupFunction{Name="Đơn vị",Order=50, Type=Enums.GroupFunctionType.Company},
                new GroupFunction{Name="Phòng tổ",Order=49, Type=Enums.GroupFunctionType.Department},
                new GroupFunction{Name="Người dùng",Order=48, Type=Enums.GroupFunctionType.User},
                new GroupFunction{Name="Nhóm quyền",Order=47, Type=Enums.GroupFunctionType.Role},
                new GroupFunction{Name="Chức vụ",Order=46, Type=Enums.GroupFunctionType.Position},
                new GroupFunction{Name="Danh mục tin tức",Order=20, Type=Enums.GroupFunctionType.NewsCategory},
                new GroupFunction{Name="Tin tức",Order=0, Type=Enums.GroupFunctionType.News},
                new GroupFunction{Name="Cài đặt",Order=4, Type=Enums.GroupFunctionType.Setting},
                new GroupFunction{Name="Loại dân tộc",Order=21, Type=Enums.GroupFunctionType.PeopleCategory},
                new GroupFunction{Name="Dân tộc",Order=1, Type=Enums.GroupFunctionType.People},
                new GroupFunction{Name="Loại trang phục",Order=22, Type=Enums.GroupFunctionType.NationalCostumeCategory},
                new GroupFunction{Name="Trang phục",Order=2, Type=Enums.GroupFunctionType.NationalCostume},
                new GroupFunction{Name="Ảnh 360",Order=3, Type=Enums.GroupFunctionType.Image},
                new GroupFunction{Name="Liên hệ",Order=4, Type=Enums.GroupFunctionType.Contact},
                new GroupFunction{Name="Báo cáo",Order=6, Type=Enums.GroupFunctionType.Report},
                new GroupFunction{Name="Thành phố - Huyện",Order=23, Type=Enums.GroupFunctionType.District},
                new GroupFunction{Name="Phường - Xã",Order=24, Type=Enums.GroupFunctionType.Ward},

            };
            _context.GroupFunctions.AddRange(lstGroupFunction);
            _context.SaveChanges();
            var lstFunction = new List<Function> {
                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_COMPANY_VIEW,Group = lstGroupFunction[0]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_COMPANY_ADD,Group = lstGroupFunction[0]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_COMPANY_EDIT,Group = lstGroupFunction[0]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_COMPANY_DELETE,Group = lstGroupFunction[0]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_DEPARTMENT_VIEW,Group = lstGroupFunction[1]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_DEPARTMENT_ADD,Group = lstGroupFunction[1]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_DEPARTMENT_EDIT,Group = lstGroupFunction[1]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_DEPARTMENT_DELETE,Group = lstGroupFunction[1]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_USER_VIEW,Group = lstGroupFunction[2]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_USER_ADD,Group = lstGroupFunction[2]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_USER_EDIT,Group = lstGroupFunction[2]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_USER_DELETE,Group = lstGroupFunction[2]},
                new Function{Name="Khóa",FunctionCode=Common.Constants.PERMISSION_USER_LOCK,Group = lstGroupFunction[2]},
                new Function{Name="Reset mật khẩu",FunctionCode=Common.Constants.PERMISSION_USER_RESETPASSWORD,Group = lstGroupFunction[2]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_ROLE_VIEW,Group = lstGroupFunction[3]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_ROLE_ADD,Group = lstGroupFunction[3]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_ROLE_EDIT,Group = lstGroupFunction[3]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_ROLE_DELETE,Group = lstGroupFunction[3]},
                new Function{Name="Phân quyền",FunctionCode=Common.Constants.PERMISSION_ROLE_ASSIGN,Group = lstGroupFunction[3]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_POSITION_VIEW,Group = lstGroupFunction[4]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_POSITION_ADD,Group = lstGroupFunction[4]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_POSITION_EDIT,Group = lstGroupFunction[4]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_POSITION_DELETE,Group = lstGroupFunction[4]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_NEWS_CATEGORY_VIEW,Group = lstGroupFunction[5]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_NEWS_CATEGORY_ADD,Group = lstGroupFunction[5]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_NEWS_CATEGORY_EDIT,Group = lstGroupFunction[5]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_NEWS_CATEGORY_DELETE,Group = lstGroupFunction[5]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_NEWS_VIEW,Group = lstGroupFunction[6]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_NEWS_ADD,Group = lstGroupFunction[6]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_NEWS_EDIT,Group = lstGroupFunction[6]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_NEWS_DELETE,Group = lstGroupFunction[6]},
                new Function{Name="Hiển thị",FunctionCode=Common.Constants.PERMISSION_NEWS_SHOW,Group = lstGroupFunction[6]},

                 new Function{Name="Cấu hình",FunctionCode=Common.Constants.PERMISSION_SETTING,Group = lstGroupFunction[7]},

                 new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_PEOPLE_CATEGORY_VIEW,Group = lstGroupFunction[8]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_PEOPLE_CATEGORY_ADD,Group = lstGroupFunction[8]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_PEOPLE_CATEGORY_EDIT,Group = lstGroupFunction[8]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_PEOPLE_CATEGORY_DELETE,Group = lstGroupFunction[8]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_PEOPLE_VIEW,Group = lstGroupFunction[9]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_PEOPLE_ADD,Group = lstGroupFunction[9]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_PEOPLE_EDIT,Group = lstGroupFunction[9]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_PEOPLE_DELETE,Group = lstGroupFunction[9]},
                new Function{Name="Hiển thị",FunctionCode=Common.Constants.PERMISSION_PEOPLE_SHOW,Group = lstGroupFunction[9]},

                 new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_VIEW,Group = lstGroupFunction[10]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_ADD,Group = lstGroupFunction[10]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_EDIT,Group = lstGroupFunction[10]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_DELETE,Group = lstGroupFunction[10]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_VIEW,Group = lstGroupFunction[11]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_ADD,Group = lstGroupFunction[11]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_EDIT,Group = lstGroupFunction[11]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_DELETE,Group = lstGroupFunction[11]},
                new Function{Name="Hiển thị",FunctionCode=Common.Constants.PERMISSION_NATIONAL_COSTUME_SHOW,Group = lstGroupFunction[11]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_IMAGE_VIEW,Group = lstGroupFunction[12]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_IMAGE_ADD,Group = lstGroupFunction[12]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_IMAGE_EDIT,Group = lstGroupFunction[12]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_IMAGE_DELETE,Group = lstGroupFunction[12]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_CONTACT_VIEW,Group = lstGroupFunction[13]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_CONTACT_DELETE,Group = lstGroupFunction[13] },

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_REPORT_VIEW,Group = lstGroupFunction[14] },

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_DISTRICT_VIEW,Group = lstGroupFunction[15]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_DISTRICT_ADD,Group = lstGroupFunction[15]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_DISTRICT_EDIT,Group = lstGroupFunction[15]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_DISTRICT_DELETE,Group = lstGroupFunction[15]},

                new Function{Name="Xem",FunctionCode=Common.Constants.PERMISSION_WARD_VIEW,Group = lstGroupFunction[16]},
                new Function{Name="Thêm mới",FunctionCode=Common.Constants.PERMISSION_WARD_ADD,Group = lstGroupFunction[16]},
                new Function{Name="Sửa",FunctionCode=Common.Constants.PERMISSION_WARD_EDIT,Group = lstGroupFunction[16]},
                new Function{Name="Xóa",FunctionCode=Common.Constants.PERMISSION_WARD_DELETE,Group = lstGroupFunction[16]},


            };
            _context.Functions.AddRange(lstFunction);
            _context.SaveChanges();
            var adminRole = new Role { Name = "Admin", RoleUsers = new List<User> { user }, RoleFunctions = lstFunction };
            _context.Roles.Add(adminRole);
            _context.SaveChanges();

        }
    }
}
