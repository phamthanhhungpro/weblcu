using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Common
{
    public class Enums
    {
        [DefaultValue(ActiveStatus.Active)]
        public enum ActiveStatus
        {
            [Display(Name = "Đang hoạt động", Order = 0)]
            Active = 0,
            [Display(Name = "Ngừng hoạt động", Order = 1)]
            Inactive = 1
        }

        public enum AccessStatus
        {
            [Display(Name = "Tất cả", Order = 0)]
            Public = 0,
            [Display(Name = "Giới hạn", Order = 1)]
            Private = 1
        }

        [DefaultValue(Update)]
        public enum PeopleConfirmStatus
        {
            [Display(Name = "Mới tạo", Order = 0)]
            Update = 0,
            [Display(Name = "Gửi duyệt", Order = 1)]
            Request = 1,
            [Display(Name = "Xuất bản", Order = 2)]
            Confirm = 2,
            [Display(Name = "Hủy duyệt", Order = 3)]
            Reject = 3,
        }

        public enum DeleteStatus
        {
            [Display(Name = "Hoạt động", Order = 0)]
            Normal = 0,
            [Display(Name = "Đã xóa", Order = 1)]
            IsDelete = 1,
        }

        public enum GroupFunctionType
        {
            User,
            Company,
            Department,
            Role,
            Position,
            NewsCategory,
            News,
            PeopleCategory,
            People,
            NationalCostumeCategory,
            NationalCostume,
            Image,
            Setting,
            Contact,
            Report,
            District,
            Ward,
            Language,
            File,
            Log,
            InstrucmentCategory,
            ProduceToolCategory
        }

        public enum ErrorCode
        {
            OK = 0,
            Error = 1,
            Validate = 11,

            InternalServerError = 2,
            UserUnauthorized = 1012,
            LoginError = 9901,

            UserNotActive = 0002,
            UserNotExist = 0003,
            UserExistAnother = 0004,
            UserNotPermission = 0005,


            CompanyNotExit = 1000,
            CompanyNotActive = 1001,
            CompanyExitsAnother = 1002,
            CompanyNotPermission = 1003,

            DepartmentNotExit = 2000,
            DepartmentNotActive = 2001,
            DepartmentExitsAnother = 2002,
            DepartmentNotPermission = 2003,

            RoleNotExist = 3000,
            RoleNotActive = 3001,
            RoleExitsAnother = 3002,
            RoleNotPermission = 3003,

            PositionNotExit = 4000,
            PositionNotActive = 4001,
            PositionNotPermisson = 4003,
            PositionExitsAnother = 4002,

            OtherNotExit = 9990,
            OtherNotActive = 9991,
            OtherNotPermisson = 9993,
            OtherExitsAnother = 9992,

        }

    }
}
