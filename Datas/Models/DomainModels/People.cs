using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Common.Enums;

namespace Datas.Models.DomainModels
{
    public class People : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên dân tộc")]
        [Display(Name = "Tên dân tộc")]
        public string Name { set; get; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { set; get; } = true;

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Địa chỉ")]
        public string? Address { set; get; }

        [Display(Name = "Ảnh 360")]
        public string? Image360 { set; get; }

        [Display(Name = "Vị trí phân bố")]
        public virtual ICollection<Location> Locations { set; get; }

        [Display(Name = "Tài liệu")]
        public virtual ICollection<Attachment> Attachments { set; get; }

        [Display(Name = "Dân số")]
        public string? Population { set; get; }

        [Display(Name = "Lịch sử")]
        [DataType(DataType.MultilineText)]
        public string? History { set; get; }


        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Tổng quan chung")]
        public string Content { get; set; }

        [Display(Name = "Không gian bản")]
        public string? Content1 { get; set; } = string.Empty;

        [Display(Name = "Không gian sống")]
        public string? Content2 { get; set; } = string.Empty;

        [Display(Name = "Hoạt động sản xuất trồng trọt")]
        public string? Content3 { get; set; } = string.Empty;

        [Display(Name = "Hoạt động sản xuất chăn nuôi")]
        public string? Content4 { get; set; } = string.Empty;

        [Display(Name = "Ẩm thực")]
        public string? Content5 { get; set; } = string.Empty;

        [Display(Name = "Loại dân tộc")]
        public int? PeopleCategoryId { get; set; }
        [Display(Name = "Loại dân tộc")]
        public virtual PeopleCategory PeopleCategory { get; set; }


        [Display(Name = "Trực thuộc dân tộc")]
        public int? ParentId { get; set; }
        public virtual People? Parent { get; set; }

        [Display(Name = "Dân tộc phụ thuộc")]
        public virtual ICollection<People>? Childrens { get; set; }

        public virtual ICollection<NationalCostume> NationalCostumes { set; get; }

        public virtual ICollection<PeopleConfirm> PeopleConfirmes { set; get; }

        //[Display(Name = "Ngôn ngữ")]
        //public int? LanguageId { get; set; }
        //[Display(Name = "Ngôn ngữ")]
        //public virtual Language Language { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public virtual ICollection<Language> Languages { get; set; }

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

        //[Display(Name = "Thành phố/huyện")]
        //public int? DistrictId { get; set; }
        //[Display(Name = "Thành phố/huyện")]
        //public virtual District District { set; get; }

        //[Display(Name = "Phường/xã")]
        //public int? WardId { get; set; }
        //[Display(Name = "Phường/xã")]
        //public virtual Ward Ward { set; get; }

        [Display(Name = "Trạng thái xuất bản")]
        public PeopleConfirmStatus ConfirmStatus { set; get; } = PeopleConfirmStatus.Update;

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(People model)
        {
            Name = model.Name;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Address = model.Address;
            Image360 = model.Image360;
            Population = model.Population;
            History = model.History;
            Details = model.Details;
            Content = model.Content;
            Content1 = model.Content1;
            Content2 = model.Content2;
            Content3 = model.Content3;
            Content4 = model.Content4;
            Content5 = model.Content5;
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

        public void SetNewData(PeopleModel model)
        {
            Name = model.Name;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Image360 = model.Image360;
            Address = model.Address;
            Population = model.Population;
            History = model.History;
            Details = model.Details;
            Content = model.Content;
            Content1 = model.Content1;
            Content2 = model.Content2;
            Content3 = model.Content3;
            Content4 = model.Content4;
            Content5 = model.Content5;
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

        public override bool IsExistAnother()
        {
            return NationalCostumes.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal) || (Childrens != null && Childrens.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal));
        }

        public PeopleModel ToPeopleModel()
        {
            var data = new PeopleModel
            {
                Id = Id,
                Name = Name,
                IsDisplay = IsDisplay,
                Top = Top,
                Image360 = Image360,
                Address = Address,
                Details = Details,
                Content = Content,
                Content1 = Content1,
                Content2 = Content2,
                Content3 = Content3,
                Content4 = Content4,
                Content5 = Content5,
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
                //DistrictId = DistrictId,
                //LanguageId = LanguageId,
                //WardId = WardId,
                ConfirmStatus = ConfirmStatus
            };
            if (PeopleCategory != null)
            {
                data.PeopleCategoryId = PeopleCategory.Id;
            }
            data.ParentId = ParentId;
            var lstLocation = new List<LocationModel>();
            if(Locations !=null)
            {
                foreach(var item in Locations)
                {
                    if(item.DeleteStatus == Enums.DeleteStatus.Normal)
                    {
                        lstLocation.Add(item.ToLocationModel());
                    }    
                }    
            }
            data.Locations = lstLocation;

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

            data.LanguageIds = Languages.Where(o=>o.DeleteStatus == DeleteStatus.Normal && o.Status == ActiveStatus.Active).Select(o=>o.Id).ToList();
            return data;
        }
    }
}
