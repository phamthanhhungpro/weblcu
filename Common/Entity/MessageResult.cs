using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class MessageResult<T> : MessageResult
    {
        public T Value { get; set; }
    }

    public class MessageResult
    {

        private string _message = string.Empty;

        public Enums.ErrorCode Code { get; set; } = Enums.ErrorCode.OK;

        public string Message
        {
            set { _message = value; }
            get
            {
                if (!string.IsNullOrEmpty(_message))
                {
                    return _message;
                }
                else
                {
                    switch (Code)
                    {
                        case Enums.ErrorCode.OK:
                            _message = "Ok";
                            break;
                        case Enums.ErrorCode.Error:
                            _message = "Lỗi";
                            break;
                        case Enums.ErrorCode.Validate:
                            _message = "Nhập đầy đủ thông tin";
                            break;

                        case Enums.ErrorCode.RoleExitsAnother:
                            _message = "Nhóm quyền đang được sử dụng";
                            break;
                        case Enums.ErrorCode.RoleNotExist:
                            _message = "Nhóm quyền không tồn tại";
                            break;
                        case Enums.ErrorCode.RoleNotPermission:
                            _message = "Không có quyền truy cập nhóm quyền";
                            break;

                        case Enums.ErrorCode.PositionExitsAnother:
                            _message = "Chức vụ đang được sử dụng";
                            break;
                        case Enums.ErrorCode.PositionNotExit:
                            _message = "Chức vụ không tồn tại";
                            break;
                        case Enums.ErrorCode.PositionNotPermisson:
                            _message = "Không có quyền truy cập chức vụ";
                            break;

                        case Enums.ErrorCode.DepartmentExitsAnother:
                            _message = "Phòng tổ đang được sử dụng";
                            break;
                        case Enums.ErrorCode.DepartmentNotExit:
                            _message = "Phòng tổ không tồn tại";
                            break;
                        case Enums.ErrorCode.DepartmentNotPermission:
                            _message = "Không có quyền truy cập phòng tổ";
                            break;

                        case Enums.ErrorCode.CompanyExitsAnother:
                            _message = "Đơn vị đang được sử dụng";
                            break;
                        case Enums.ErrorCode.CompanyNotExit:
                            _message = "Đơn vị không tồn tại";
                            break;
                        case Enums.ErrorCode.CompanyNotPermission:
                            _message = "Không có quyền truy cập đơn vị";
                            break;

                        case Enums.ErrorCode.OtherExitsAnother:
                            _message = "Dữ liệu đang được sử dụng";
                            break;
                        case Enums.ErrorCode.OtherNotExit:
                            _message = "Dữ liệu không tồn tại";
                            break;
                        case Enums.ErrorCode.OtherNotPermisson:
                            _message = "Không có quyền truy cập dữ liệu";
                            break;

                        default:
                            _message = "Không xác định";
                            break;


                    }
                    return _message;
                }
            }
        }

        public bool IsSuccess()
        {
            return Code == Enums.ErrorCode.OK ? true : false;
        }
    }
}
