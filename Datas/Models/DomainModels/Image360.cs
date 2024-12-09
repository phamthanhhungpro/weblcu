using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class Image360 : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { get; set; } = true;

        [Required(ErrorMessage = "Bạn cần nhập ảnh đại diện")]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Display(Name = "Ảnh 360")]
        public string? ImageLink { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string? Details { get; set; }

        public void SetNewData(Image360 data)
        {
            Title = data.Title;
            IsDisplay = data.IsDisplay;
            Image = data.Image;
            ImageLink = data.ImageLink;
            Details = data.Details;
            Status = data.Status;
        }
        public void SetNewData(Image360Model data)
        {
            Title = data.Title;
            IsDisplay = data.IsDisplay;
            Image = data.Image;
            ImageLink = data.ImageLink;
            Details = data.Details;
            Status = data.Status;
        }

        public Image360Model ToImage360Model()
        {
            var model = new Image360Model
            {
                Id = Id,
                Title = Title,
                Image = Image,
                IsDisplay = IsDisplay,
                ImageLink = ImageLink,
                Status = Status,
                Details = Details
            };
            return model;
        }
    }
}
