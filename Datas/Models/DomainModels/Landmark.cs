using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common;
using Datas.Models.ViewModels;

namespace Datas.Models.DomainModels;

public class Landmark : BaseModel
{
    [Required(ErrorMessage = "Bạn cần nhập tên điểm đến di tích, danh lam")]
    [Display(Name = "Tên điểm đến di tích, danh lam")]
    public string Name { set; get; }

    [Display(Name = "Tên gọi khác")]
    public string? AlternativeNames { get; set; }

    [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
    [Display(Name = "Mã di tích")]
    public string IdentityCode { get; set; }

    [Display(Name = "Loại di tích")]
    public string? LandmarkType { get; set; }

    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Display(Name = "Tọa độ GPS")]
    public string? GpsCoordinates { get; set; }

    [Display(Name = "Nguồn gốc & Lịch sử")]
    [DataType(DataType.MultilineText)]
    public string? History { get; set; }

    [Display(Name = "Sự kiện liên quan")]
    [DataType(DataType.MultilineText)]
    public string? RelatedEvents { get; set; }

    [Display(Name = "Tín ngưỡng & Tâm linh")]
    [DataType(DataType.MultilineText)]
    public string? SpiritualBelief { get; set; }

    [Display(Name = "Giá trị văn hóa - xã hội")]
    [DataType(DataType.MultilineText)]
    public string? CulturalValue { get; set; }

    [Display(Name = "Kiến trúc/Tự nhiên")]
    [DataType(DataType.MultilineText)]
    public string? Architecture { get; set; }

    [Display(Name = "Chất liệu xây dựng")]
    public string? BuildingMaterial { get; set; }

    [Display(Name = "Tình trạng hiện tại")]
    [DataType(DataType.MultilineText)]
    public string? CurrentCondition { get; set; }

    [Display(Name = "Diện tích & Quy mô")]
    public string? SizeScale { get; set; }

    [Display(Name = "Hiện vật trưng bày")]
    [DataType(DataType.MultilineText)]
    public string? Exhibits { get; set; }

    [Display(Name = "Thời gian mở cửa")]
    public string? OpeningHours { get; set; }

    [Display(Name = "Giá vé")]
    public string? TicketPrice { get; set; }

    [Display(Name = "Các dịch vụ hỗ trợ")]
    [DataType(DataType.MultilineText)]
    public string? SupportServices { get; set; }

    [Display(Name = "Hoạt động trải nghiệm")]
    [DataType(DataType.MultilineText)]
    public string? ExperienceActivities { get; set; }

    [Display(Name = "Lượng khách tham quan hàng năm")]
    public int? AnnualVisitors { get; set; }

    [Display(Name = "Xếp hạng di tích")]
    public string? HeritageRanking { get; set; }

    [Display(Name = "Cơ quan quản lý")]
    public string? ManagementAuthority { get; set; }

    [Display(Name = "Dự án bảo tồn & Phục dựng")]
    [DataType(DataType.MultilineText)]
    public string? ConservationProject { get; set; }

    // Update SetNewData methods to include new fields
    public void SetNewData(Landmark model)
    {
        // Existing fields
        Name = model.Name;
        Code = model.Code;
        AlternativeNames = model.AlternativeNames;
        IdentityCode = model.IdentityCode;
        LandmarkType = model.LandmarkType;
        Address = model.Address;
        GpsCoordinates = model.GpsCoordinates;

        // New fields
        History = model.History;
        RelatedEvents = model.RelatedEvents;
        SpiritualBelief = model.SpiritualBelief;
        CulturalValue = model.CulturalValue;
        Architecture = model.Architecture;
        BuildingMaterial = model.BuildingMaterial;
        CurrentCondition = model.CurrentCondition;
        SizeScale = model.SizeScale;
        Exhibits = model.Exhibits;
        OpeningHours = model.OpeningHours;
        TicketPrice = model.TicketPrice;
        SupportServices = model.SupportServices;
        ExperienceActivities = model.ExperienceActivities;
        AnnualVisitors = model.AnnualVisitors;
        HeritageRanking = model.HeritageRanking;
        ManagementAuthority = model.ManagementAuthority;
        ConservationProject = model.ConservationProject;

        // Existing base fields
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

    public void SetNewData(LandmarkModel model)
    {
        // Existing fields
        Name = model.Name;
        Code = model.Code;
        AlternativeNames = model.AlternativeNames;
        IdentityCode = model.IdentityCode;
        LandmarkType = model.LandmarkType;
        Address = model.Address;
        GpsCoordinates = model.GpsCoordinates;

        // New fields
        History = model.History;
        RelatedEvents = model.RelatedEvents;
        SpiritualBelief = model.SpiritualBelief;
        CulturalValue = model.CulturalValue;
        Architecture = model.Architecture;
        BuildingMaterial = model.BuildingMaterial;
        CurrentCondition = model.CurrentCondition;
        SizeScale = model.SizeScale;
        Exhibits = model.Exhibits;
        OpeningHours = model.OpeningHours;
        TicketPrice = model.TicketPrice;
        SupportServices = model.SupportServices;
        ExperienceActivities = model.ExperienceActivities;
        AnnualVisitors = model.AnnualVisitors;
        HeritageRanking = model.HeritageRanking;
        ManagementAuthority = model.ManagementAuthority;
        ConservationProject = model.ConservationProject;

        // Base fields
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

    public LandmarkModel ToModel()
    {
        var data = new LandmarkModel
        {
            Id = Id,
            Name = Name,
            Code = Code,
            AlternativeNames = AlternativeNames,
            IdentityCode = IdentityCode,
            LandmarkType = LandmarkType,
            Address = Address,
            GpsCoordinates = GpsCoordinates,
            History = History,
            RelatedEvents = RelatedEvents,
            SpiritualBelief = SpiritualBelief,
            CulturalValue = CulturalValue,
            Architecture = Architecture,
            BuildingMaterial = BuildingMaterial,
            CurrentCondition = CurrentCondition,
            SizeScale = SizeScale,
            Exhibits = Exhibits,
            OpeningHours = OpeningHours,
            TicketPrice = TicketPrice,
            SupportServices = SupportServices,
            ExperienceActivities = ExperienceActivities,
            AnnualVisitors = AnnualVisitors,
            HeritageRanking = HeritageRanking,
            ManagementAuthority = ManagementAuthority,
            ConservationProject = ConservationProject,

            // Base model properties
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
            PeopleId = PeopleId,
            Url = Url
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

    // Existing ReNewUrl method
    public void ReNewUrl()
    {
        var newUrl = ExtensionMethod.RemoveUnicode(Name);
        newUrl = newUrl.Replace(" ", "-").ToLower();
        newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
        Url = newUrl;
    }
}
