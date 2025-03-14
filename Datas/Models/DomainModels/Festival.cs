using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class Festival : BaseModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên lễ hội văn hóa")]
        [Display(Name = "Tên lễ hội văn hóa")]
        public string Name { set; get; }

        public string? TenGoiKhac { get; set; }
        public string? VungMien { get; set; }
        public string? ThoiGianToChuc { get; set; }
        public string? Purpose { get; set; }
        public string? OrganizationForm { get; set; }
        public string? Participants { get; set; }
        public string? MainEvents { get; set; }
        public string? Origin { get; set; }
        public string? ChangesOverTime { get; set; }
        public string? RelatedBeliefs { get; set; }
        public string? CulturalSocialImpact { get; set; }
        public string? MainRituals { get; set; }
        public string? TraditionalCostumes { get; set; }
        public string? OfferingsAndWorship { get; set; }
        public string? FolkGames { get; set; }
        public string? ConservationStatus { get; set; }
        public string? OrganizingAgency { get; set; }
        public string? ConservationActivities { get; set; }
        public string? TTBaoTon { get; set; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(Festival model)
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
            TenGoiKhac = model.TenGoiKhac;
            VungMien = model.VungMien;
            ThoiGianToChuc = model.ThoiGianToChuc;
            Purpose = model.Purpose;
            OrganizationForm = model.OrganizationForm;
            Participants = model.Participants;
            MainEvents = model.MainEvents;
            Origin = model.Origin;
            ChangesOverTime = model.ChangesOverTime;
            RelatedBeliefs = model.RelatedBeliefs;
            CulturalSocialImpact = model.CulturalSocialImpact;
            MainRituals = model.MainRituals;
            TraditionalCostumes = model.TraditionalCostumes;
            OfferingsAndWorship = model.OfferingsAndWorship;
            FolkGames = model.FolkGames;
            ConservationStatus = model.ConservationStatus;
            OrganizingAgency = model.OrganizingAgency;
            ConservationActivities = model.ConservationActivities;
            TTBaoTon = model.TTBaoTon;
        }

        public void SetNewData(FestivalModel model)
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
            TenGoiKhac = model.TenGoiKhac;
            VungMien = model.VungMien;
            ThoiGianToChuc = model.ThoiGianToChuc;
            Purpose = model.Purpose;
            OrganizationForm = model.OrganizationForm;
            Participants = model.Participants;
            MainEvents = model.MainEvents;
            Origin = model.Origin;
            ChangesOverTime = model.ChangesOverTime;
            RelatedBeliefs = model.RelatedBeliefs;
            CulturalSocialImpact = model.CulturalSocialImpact;
            MainRituals = model.MainRituals;
            TraditionalCostumes = model.TraditionalCostumes;
            OfferingsAndWorship = model.OfferingsAndWorship;
            FolkGames = model.FolkGames;
            ConservationStatus = model.ConservationStatus;
            OrganizingAgency = model.OrganizingAgency;
            ConservationActivities = model.ConservationActivities;
            TTBaoTon = model.TTBaoTon;
        }

        public FestivalModel ToModel()
        {
            var data = new FestivalModel
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
                TenGoiKhac = TenGoiKhac,
                VungMien = VungMien,
                ThoiGianToChuc = ThoiGianToChuc,
                Purpose = Purpose,
                OrganizationForm = OrganizationForm,
                Participants = Participants,
                MainEvents = MainEvents,
                Origin = Origin,
                ChangesOverTime = ChangesOverTime,
                RelatedBeliefs = RelatedBeliefs,
                CulturalSocialImpact = CulturalSocialImpact,
                MainRituals = MainRituals,
                TraditionalCostumes = TraditionalCostumes,
                OfferingsAndWorship = OfferingsAndWorship,
                FolkGames = FolkGames,
                ConservationStatus = ConservationStatus,
                OrganizingAgency = OrganizingAgency,
                ConservationActivities = ConservationActivities,
                TTBaoTon = TTBaoTon
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
