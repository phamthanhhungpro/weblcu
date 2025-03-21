﻿using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class CustomsTradition : BaseModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên phong tục tập quán")]
        [Display(Name = "Tên phong tục tập quán")]
        public string Name { set; get; }

        public string? PurposeMeaning { get; set; }
        public string? TimeOfOccurrence { get; set; }
        public string? FormOfImplementation { get; set; }
        public string? Participants { get; set; }
        public string? Origin { get; set; }
        public string? ChangesOverTime { get; set; }
        public string? RelatedBeliefs { get; set; }
        public string? SocialImpact { get; set; }
        public string? RequiredItems { get; set; }
        public string? TraditionalAttire { get; set; }
        public string? Steps { get; set; }
        public string? ConservationStatus { get; set; }
        public string? ConservationUnit { get; set; }
        public string? PreservationActivities { get; set; }
        public string? TenGoiKhac { get; set; }
        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(CustomsTradition model)
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
            PurposeMeaning = model.PurposeMeaning;
            TimeOfOccurrence = model.TimeOfOccurrence;
            FormOfImplementation = model.FormOfImplementation;
            Participants = model.Participants;
            Origin = model.Origin;
            ChangesOverTime = model.ChangesOverTime;
            RelatedBeliefs = model.RelatedBeliefs;
            SocialImpact = model.SocialImpact;
            RequiredItems = model.RequiredItems;
            TraditionalAttire = model.TraditionalAttire;
            Steps = model.Steps;
            ConservationStatus = model.ConservationStatus;
            ConservationUnit = model.ConservationUnit;
            PreservationActivities = model.PreservationActivities;
            TenGoiKhac = model.TenGoiKhac;
        }

        public void SetNewData(CustomsTraditionModel model)
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
            PurposeMeaning = model.PurposeMeaning;
            TimeOfOccurrence = model.TimeOfOccurrence;
            FormOfImplementation = model.FormOfImplementation;
            Participants = model.Participants;
            Origin = model.Origin;
            ChangesOverTime = model.ChangesOverTime;
            RelatedBeliefs = model.RelatedBeliefs;
            SocialImpact = model.SocialImpact;
            RequiredItems = model.RequiredItems;
            TraditionalAttire = model.TraditionalAttire;
            Steps = model.Steps;
            ConservationStatus = model.ConservationStatus;
            ConservationUnit = model.ConservationUnit;
            PreservationActivities = model.PreservationActivities;
            TenGoiKhac = model.TenGoiKhac;
        }

        public CustomsTraditionModel ToModel()
        {
            var data = new CustomsTraditionModel
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
                PurposeMeaning = PurposeMeaning,
                TimeOfOccurrence = TimeOfOccurrence,
                FormOfImplementation = FormOfImplementation,
                Participants = Participants,
                Origin = Origin,
                ChangesOverTime = ChangesOverTime,
                RelatedBeliefs = RelatedBeliefs,
                SocialImpact = SocialImpact,
                RequiredItems = RequiredItems,
                TraditionalAttire = TraditionalAttire,
                Steps = Steps,
                ConservationStatus = ConservationStatus,
                ConservationUnit = ConservationUnit,
                PreservationActivities = PreservationActivities,
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
