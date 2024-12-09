using Datas.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class AssignUserModel
    {
        public int Id { get; set; }

        [DisplayName("Tên nhóm quyền")]
        public string? RoleName { get; set; }

        [DisplayName("Ghi chú")]
        public string? Title { get; set; }

        [DisplayName("Trạng thái")]
        public Common.Enums.ActiveStatus Status { get; set; }

        [DisplayName("Tài khoản")]
        public string Assign { get; set; }

        public virtual List<AssignUser>? Users { get {
                var data = new List<AssignUser>();
                if (!string.IsNullOrEmpty(Assign))
                {
                    var array = Assign.Split("|").Distinct().Where(o => !string.IsNullOrEmpty(o)).ToList();
                    foreach (var item in array)
                    {
                        int intValue = 0;
                        if (int.TryParse(item, out intValue))
                        {
                            data.Add(new AssignUser { UserId = intValue, IsSelect = true });
                        }
                    }
                }
                return data;
            }
            set
            {
                if (value != null)
                    Assign = string.Join("|", value.Select(o => o.UserId).ToList());
                else
                    Assign = string.Empty;
            }
        }
    }
}
