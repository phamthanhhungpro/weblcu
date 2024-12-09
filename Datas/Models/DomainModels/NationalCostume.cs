using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class NationalCostume : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên trang phục dân tộc")]
        [Display(Name = "Tên trang phục dân tộc")]
        public string Name { set; get; }

        [Display(Name = "Ký hiệu, mã số ảnh")]
        public string? Code { set; get; }

        [Display(Name = "Dịp sử dụng")]
        public string? Event { set; get; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { set; get; } = true;

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Ảnh 360")]
        public string? Image360 { set; get; }

        [Display(Name = "Phụ kiện đi kèm")]
        public string? Shape { set; get; }

        [Display(Name = "Hiện trạng")]
        public string? CurrentStatus { set; get; }

        [Display(Name = "Kỹ thuật chế tác")]
        public string? Technique { set; get; }

        [Display(Name = "Ý nghĩa văn hóa")]
        public string? Classify { set; get; }

        [Display(Name = "Nguyên tắc thiết kế")]
        public string? Certification { set; get; }

        [Display(Name = "Nguyên liệu sử dụng")]
        public string? Material { set; get; }

        [Display(Name = "Giá cả trung bình")]
        public string? Cost { set; get; }

        [Display(Name = "Màu sắc")]
        public string? Color { set; get; }

        [Display(Name = "Kích thước")]
        [DataType(DataType.MultilineText)]
        public string? Size { set; get; }

        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Nội dung bài viết")]
        public string Content { get; set; }

        [Display(Name = "Loại trang phục dân tộc")]
        public int? CategoryId { get; set; }
        [Display(Name = "Loại trang phục dân tộc")]
        public virtual NationalCostumeCategory Category { get; set; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { get; set; }
        [Display(Name = "Dân tộc")]
        public virtual People People { get; set; }


        [Display(Name = "Ảnh đại diện")]
        public string? Image0 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image1 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image2 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image3 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image4 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image5 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image6 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image7 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image8 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image9 { set; get; }

        public string Url { set; get; }

        public int View { set; get; } = 0;

        [Display(Name = "Tài liệu")]
        public virtual ICollection<Attachment> Attachments { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(NationalCostume model)
        {
            Name = model.Name;
            Code = model.Code;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Image360 = model.Image360;
            Shape = model.Shape;
            CurrentStatus = model.CurrentStatus;
            Technique = model.Technique;
            Classify = model.Classify;
            Certification = model.Certification;
            Material = model.Material;
            Cost = model.Cost;
            Color = model.Color;
            Size = model.Size;
            Details = model.Details;
            Content = model.Content;
            Status = model.Status;
            Image0 = model.Image0;
            Image1 = model.Image1;
            Image2 = model.Image2;
            Image3 = model.Image3;
            Image4 = model.Image4;
            Image5 = model.Image5;
            Image6 = model.Image6;
            Image7 = model.Image7;
            Image8 = model.Image8;
            Image9 = model.Image9;
           
        }

        public void SetNewData(NationalCostumeModel model)
        {
            Name = model.Name;
            Code = model.Code;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Image360 = model.Image360;
            Shape = model.Shape;
            CurrentStatus = model.CurrentStatus;
            Technique = model.Technique;
            Classify = model.Classify;
            Certification = model.Certification;
            Material = model.Material;
            Cost = model.Cost;
            Color = model.Color;
            Size = model.Size;
            Details = model.Details;
            Content = model.Content;
            Status = model.Status;
            Image0 = model.Image0;
            Image1 = model.Image1;
            Image2 = model.Image2;
            Image3 = model.Image3;
            Image4 = model.Image4;
            Image5 = model.Image5;
            Image6 = model.Image6;
            Image7 = model.Image7;
            Image8 = model.Image8;
            Image9 = model.Image9;
        }

        //public override bool IsExistAnother()
        //{
        //    return Artifactes.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        //}

        public NationalCostumeModel ToNationalCostumeModel()
        {
            var data = new NationalCostumeModel
            {
                Id = Id,
                Name = Name,
                Code = Code,
                IsDisplay = IsDisplay,
                Top = Top,
                Image360 = Image360,
                Shape = Shape,
                CurrentStatus = CurrentStatus,
                Technique = Technique,
                Classify = Classify,
                Certification = Certification,
                Material = Material,
                Cost = Cost,
                Color = Color,
                Size = Size,
                Details = Details,
                Content = Content,
                Status = Status,
                Image0 = Image0,
                Image1 = Image1,
                Image2 = Image2,
                Image3 = Image3,
                Image4 = Image4,
                Image5 = Image5,
                Image6 = Image6,
                Image7 = Image7,
                Image8 = Image8,
                Image9 = Image9,
                Url = Url,
                CategoryId =CategoryId,
                PeopleId = PeopleId
            };
            if(People !=null)
            {
                data.SetPeopleName(People.Name);
            }
            var lstAttach = new List<AttachmentModel>();
            if (Attachments != null)
            {
                foreach (var item in Attachments)
                {
                    if (item.DeleteStatus == Enums.DeleteStatus.Normal)
                    {
                        lstAttach.Add(item.ToAttachmentModel());
                    }
                }
            }
            data.Attachments = lstAttach;
            return data;
        }
    }
}
