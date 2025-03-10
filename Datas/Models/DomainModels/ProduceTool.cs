using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class ProduceTool : BaseModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên công cụ sản xuất")]
        [Display(Name = "Tên công cụ sản xuất")]
        public string Name { set; get; }

        [Display(Name = "Loại công cụ sản xuất")]
        public int? CategoryId { get; set; }
        [Display(Name = "Loại công cụ sản xuất")]
        public virtual ProduceToolCategory Category { get; set; }

        public string? Structure { get; set; }
        public string? Dimensions { get; set; }
        public string? Usage { get; set; }
        public string? MainFunction { get; set; }
        public string? RelatedTraditionalCrafts { get; set; }
        public string? UsageEfficiency { get; set; }
        public string? Variants { get; set; }
        public string? Origin { get; set; }
        public string? CulturalSignificance { get; set; }
        public string? FamousArtisans { get; set; }
        public string? TinhTrangHienTai { get; set; }
        public string? Custodian { get; set; }
        public string? EconomicValue { get; set; }
        public string? VungMien { get; set; }
        public string? NganhNgheSuDung { get; set; }
        public string? TenGoiKhac { get; set; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(ProduceTool model)
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
            Structure = model.Structure;
            Dimensions = model.Dimensions;
            Usage = model.Usage;
            MainFunction = model.MainFunction;
            RelatedTraditionalCrafts = model.RelatedTraditionalCrafts;
            UsageEfficiency = model.UsageEfficiency;
            Variants = model.Variants;
            Origin = model.Origin;
            CulturalSignificance = model.CulturalSignificance;
            FamousArtisans = model.FamousArtisans;
            TinhTrangHienTai = model.TinhTrangHienTai;
            Custodian = model.Custodian;
            EconomicValue = model.EconomicValue;
            VungMien = model.VungMien;
            NganhNgheSuDung = model.NganhNgheSuDung;
            TenGoiKhac = model.TenGoiKhac;
        }

        public void SetNewData(ProduceToolModel model)
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
            Structure = model.Structure;
            Dimensions = model.Dimensions;
            Usage = model.Usage;
            MainFunction = model.MainFunction;
            RelatedTraditionalCrafts = model.RelatedTraditionalCrafts;
            UsageEfficiency = model.UsageEfficiency;
            Variants = model.Variants;
            Origin = model.Origin;
            CulturalSignificance = model.CulturalSignificance;
            FamousArtisans = model.FamousArtisans;
            TinhTrangHienTai = model.TinhTrangHienTai;
            Custodian = model.Custodian;
            EconomicValue = model.EconomicValue;
            VungMien = model.VungMien;
            NganhNgheSuDung = model.NganhNgheSuDung;
            TenGoiKhac = model.TenGoiKhac;
        }

        public ProduceToolModel ToModel()
        {
            var data = new ProduceToolModel
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
                CategoryId = CategoryId,
                PeopleId = PeopleId,
                Structure = Structure,
                Dimensions = Dimensions,
                Usage = Usage,
                MainFunction = MainFunction,
                RelatedTraditionalCrafts = RelatedTraditionalCrafts,
                UsageEfficiency = UsageEfficiency,
                Variants = Variants,
                Origin = Origin,
                CulturalSignificance = CulturalSignificance,
                FamousArtisans = FamousArtisans,
                TinhTrangHienTai = TinhTrangHienTai,
                Custodian = Custodian,
                EconomicValue = EconomicValue,
                VungMien = VungMien,
                NganhNgheSuDung = NganhNgheSuDung,
                TenGoiKhac = TenGoiKhac,
            };

            if (People != null)
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
