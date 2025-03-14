using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class Jewelry : BaseModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên trang sức")]
        [Display(Name = "Tên trang sức")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
        [Display(Name = "Mã trang sức")]
        public string IdentityCode { get; set; }

        [Display(Name = "Tên gọi khác")]
        public string? AlternativeNames { get; set; }

        [Display(Name = "Loại trang sức")]
        public int? CategoryId { get; set; }

        [Display(Name = "Loại trang sức")]
        public virtual JewelryCategory Category { get; set; }

        [Display(Name = "Giới tính sử dụng")]
        public string? Gender { get; set; }

        [Display(Name = "Độ tuổi sử dụng")]
        public string? AgeGroup { get; set; }

        [Display(Name = "Link ảnh scan 3D")]
        public string? Scan3DLink { get; set; }

        [Display(Name = "Kích thước & Trọng lượng")]
        public string? SizeWeight { get; set; }

        [Display(Name = "Mục đích sử dụng")]

        public string? Purpose { get; set; }

        [Display(Name = "Ý nghĩa biểu tượng")]
        public string? SymbolicMeaning { get; set; }

        [Display(Name = "Sự kiện liên quan")]
        public string? RelatedEvents { get; set; }

        [Display(Name = "Ảnh hưởng văn hóa - xã hội")]
        public string? CulturalImpact { get; set; }

        [Display(Name = "Nguồn gốc")]
        public string? Origin { get; set; }

        [Display(Name = "Sự thay đổi theo thời gian")]
        public string? HistoricalChanges { get; set; }

        [Display(Name = "Khu vực phổ biến")]
        public string? CommonRegions { get; set; }

        [Display(Name = "Cách bảo quản")]
        public string? PreservationMethod { get; set; }

        [Display(Name = "Hiện vật trưng bày")]
        public string? DisplayedItems { get; set; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(Jewelry model)
        {
            Name = model.Name;
            IdentityCode = model.IdentityCode;
            AlternativeNames = model.AlternativeNames;
            CategoryId = model.CategoryId;
            Gender = model.Gender;
            AgeGroup = model.AgeGroup;
            Scan3DLink = model.Scan3DLink;
            SizeWeight = model.SizeWeight;
            Purpose = model.Purpose;
            SymbolicMeaning = model.SymbolicMeaning;
            RelatedEvents = model.RelatedEvents;
            CulturalImpact = model.CulturalImpact;
            Origin = model.Origin;
            HistoricalChanges = model.HistoricalChanges;
            CommonRegions = model.CommonRegions;
            PreservationMethod = model.PreservationMethod;
            DisplayedItems = model.DisplayedItems;

            // BaseModel fields
            Code = model.Code;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Image360 = model.Image360;
            Shape = model.Shape;
            CurrentStatus = model.CurrentStatus;
            Technique = model.Technique;
            Classify = model.Classify;
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

        public void SetNewData(JewelryModel model)
        {
            Name = model.Name;
            IdentityCode = model.IdentityCode;
            AlternativeNames = model.AlternativeNames;
            CategoryId = model.CategoryId;
            Gender = model.Gender;
            AgeGroup = model.AgeGroup;
            Scan3DLink = model.Scan3DLink;
            SizeWeight = model.SizeWeight;
            Purpose = model.Purpose;
            SymbolicMeaning = model.SymbolicMeaning;
            RelatedEvents = model.RelatedEvents;
            CulturalImpact = model.CulturalImpact;
            Origin = model.Origin;
            HistoricalChanges = model.HistoricalChanges;
            CommonRegions = model.CommonRegions;
            PreservationMethod = model.PreservationMethod;
            DisplayedItems = model.DisplayedItems;

            // BaseModel fields
            Code = model.Code;
            IsDisplay = model.IsDisplay;
            Top = model.Top;
            Image360 = model.Image360;
            Shape = model.Shape;
            CurrentStatus = model.CurrentStatus;
            Technique = model.Technique;
            Classify = model.Classify;
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

        public JewelryModel ToModel()
        {
            var data = new JewelryModel
            {
                Id = Id,
                Name = Name,
                IdentityCode = IdentityCode,
                AlternativeNames = AlternativeNames,
                CategoryId = CategoryId,
                Gender = Gender,
                AgeGroup = AgeGroup,
                Scan3DLink = Scan3DLink,
                SizeWeight = SizeWeight,
                Purpose = Purpose,
                SymbolicMeaning = SymbolicMeaning,
                RelatedEvents = RelatedEvents,
                CulturalImpact = CulturalImpact,
                Origin = Origin,
                HistoricalChanges = HistoricalChanges,
                CommonRegions = CommonRegions,
                PreservationMethod = PreservationMethod,
                DisplayedItems = DisplayedItems,

                // BaseModel fields
                Code = Code,
                IsDisplay = IsDisplay,
                Top = Top,
                Image360 = Image360,
                Shape = Shape,
                CurrentStatus = CurrentStatus,
                Technique = Technique,
                Classify = Classify,
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
                PeopleId = PeopleId
            };

            if (People != null)
            {
                data.SetPeopleName(People.Name);
            }

            if (Category != null)
            {
                data.SetCategoryName(Category.Name);
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
