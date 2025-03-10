using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common;
using Datas.Models.ViewModels;

namespace Datas.Models.DomainModels;

public class Instrument : BaseModel
{
    [Required(ErrorMessage = "Bạn cần nhập tên Nhạc cụ")]
    [Display(Name = "Tên Nhạc cụ")]
    public string Name { set; get; }

    [Display(Name = "Loại Nhạc cụ")]
    public int? CategoryId { get; set; }
    [Display(Name = "Loại Nhạc cụ")]
    public virtual InstrumentCategory Category { get; set; }
    public string? Structure { get; set; }

    public string? Dimensions { get; set; }

    public string? PlayingMethod { get; set; }

    public string? PitchRange { get; set; }
    public string? Timbre { get; set; }
    public string? RoleInOrchestra { get; set; }
    public string? PerformanceEvents { get; set; }
    public string? Origin { get; set; }
    public string? CulturalSignificance { get; set; }
    public string? FamousArtisans { get; set; }
    public string? VungMien { get; set; }
    public string? TenGoiKhac { get; set; }

    public void ReNewUrl()
    {
        var newUrl = ExtensionMethod.RemoveUnicode(Name);
        newUrl = newUrl.Replace(" ", "-").ToLower();
        newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
        Url = newUrl;
    }

    public void SetNewData(Instrument model)
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
        PitchRange = model.PitchRange;
        Timbre = model.Timbre;
        RoleInOrchestra = model.RoleInOrchestra;
        PerformanceEvents = model.PerformanceEvents;
        Origin = model.Origin;
        CulturalSignificance = model.CulturalSignificance;
        FamousArtisans = model.FamousArtisans;
        VungMien = model.VungMien;
        PlayingMethod = model.PlayingMethod;
        TenGoiKhac = model.TenGoiKhac;
    }

    public void SetNewData(InstrumentModel model)
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
        PitchRange = model.PitchRange;
        Timbre = model.Timbre;
        RoleInOrchestra = model.RoleInOrchestra;
        PerformanceEvents = model.PerformanceEvents;
        Origin = model.Origin;
        CulturalSignificance = model.CulturalSignificance;
        FamousArtisans = model.FamousArtisans;
        VungMien = model.VungMien;
        PlayingMethod = model.PlayingMethod;
        TenGoiKhac = model.TenGoiKhac;
    }

    public InstrumentModel ToModel()
    {
        var data = new InstrumentModel
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
            PitchRange = PitchRange,
            Timbre = Timbre,
            RoleInOrchestra = RoleInOrchestra,
            PerformanceEvents = PerformanceEvents,
            Origin = Origin,
            CulturalSignificance = CulturalSignificance,
            FamousArtisans = FamousArtisans,
            VungMien = VungMien,
            PlayingMethod = PlayingMethod,
            TenGoiKhac = TenGoiKhac
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