using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class Travel : BaseModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên hoạt động du lịch")]
        [Display(Name = "Tên hoạt động du lịch")]
        public string Name { set; get; }

        public string? DiaDiem { set; get; }
        public string? LoaiHinh { set; get; }
        public string? ThoiGian { set; get; }
        public string? DonViToChuc { set; get; }

        public string? Highlights { get; set; }
        public string? UniqueExperiences { get; set; }
        public string? CulturalSocialImpact { get; set; }
        public string? AccompanyingEvents { get; set; }

        // Fields for Chi phí tham gia, Dịch vụ đi kèm, Ưu đãi & Khuyến mãi
        public string? ParticipationCost { get; set; }
        public string? AccompanyingServices { get; set; }
        public string? DiscountsAndPromotions { get; set; }

        // Fields for Tình trạng hoạt động, Cơ quan quản lý, Hoạt động quảng bá
        public string? OperationalStatus { get; set; }
        public string? ManagingAgency { get; set; }
        public string? PromotionalActivities { get; set; }
        public string? DetailedDescription { get; set; }
        public string? Participants { get; set; }
        public string? Suitability { get; set; }
        public string? RequiredAttireAndEquipment
        {
            get; set;
        }
        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(Travel model)
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
            DiaDiem = model.DiaDiem;
            LoaiHinh = model.LoaiHinh;
            ThoiGian = model.ThoiGian;
            DonViToChuc = model.DonViToChuc;
            Highlights = model.Highlights;
            UniqueExperiences = model.UniqueExperiences;
            CulturalSocialImpact = model.CulturalSocialImpact;
            AccompanyingEvents = model.AccompanyingEvents;
            ParticipationCost = model.ParticipationCost;
            AccompanyingServices = model.AccompanyingServices;
            DiscountsAndPromotions = model.DiscountsAndPromotions;
            OperationalStatus = model.OperationalStatus;
            ManagingAgency = model.ManagingAgency;
            PromotionalActivities = model.PromotionalActivities;
            DetailedDescription = model.DetailedDescription;
            Participants = model.Participants;
            Suitability = model.Suitability;
            RequiredAttireAndEquipment = model.RequiredAttireAndEquipment;


        }

        public void SetNewData(TravelModel model)
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
            DiaDiem = model.DiaDiem;
            LoaiHinh = model.LoaiHinh;
            ThoiGian = model.ThoiGian;
            DonViToChuc = model.DonViToChuc;
            Highlights = model.Highlights;
            UniqueExperiences = model.UniqueExperiences;
            CulturalSocialImpact = model.CulturalSocialImpact;
            AccompanyingEvents = model.AccompanyingEvents;
            ParticipationCost = model.ParticipationCost;
            AccompanyingServices = model.AccompanyingServices;
            DiscountsAndPromotions = model.DiscountsAndPromotions;
            OperationalStatus = model.OperationalStatus;
            ManagingAgency = model.ManagingAgency;
            PromotionalActivities = model.PromotionalActivities;
            DetailedDescription = model.DetailedDescription;
            Participants = model.Participants;
            Suitability = model.Suitability;
            RequiredAttireAndEquipment = model.RequiredAttireAndEquipment;
        }

        public TravelModel ToModel()
        {
            var data = new TravelModel
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
                PeopleId = PeopleId,
                DiaDiem = DiaDiem,
                LoaiHinh = LoaiHinh,
                ThoiGian = ThoiGian,
                DonViToChuc = DonViToChuc,
                Highlights = Highlights,
                UniqueExperiences = UniqueExperiences,
                CulturalSocialImpact = CulturalSocialImpact,
                AccompanyingEvents = AccompanyingEvents,
                ParticipationCost = ParticipationCost,
                AccompanyingServices = AccompanyingServices,
                DiscountsAndPromotions = DiscountsAndPromotions,
                OperationalStatus = OperationalStatus,
                ManagingAgency = ManagingAgency,
                PromotionalActivities = PromotionalActivities,
                DetailedDescription = DetailedDescription,
                Participants = Participants,
                Suitability = Suitability,
                RequiredAttireAndEquipment = RequiredAttireAndEquipment,
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
