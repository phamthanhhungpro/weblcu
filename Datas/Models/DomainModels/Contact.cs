using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class Contact : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Chủ đề")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập nội dung")]
        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public void SetNewData(Contact data)
        {
            FullName = data.FullName;
            Email = data.Email;
            Phone = data.Phone;
            Title = data.Title;
            Content = data.Content;
        }
        public void SetNewData(ContactModel data)
        {
            FullName = data.FullName;
            Email = data.Email;
            Phone = data.Phone;
            Title = data.Title;
            Content = data.Content;
        }

        public ContactModel ToContactModel()
        {
            var model = new ContactModel
            {
                Id = Id,
                FullName = FullName,
                Email = Email,
                Title = Title,
                Content = Content,
                Status = Status,
                Phone = Phone
            };
            return model;
        }
    }
}
