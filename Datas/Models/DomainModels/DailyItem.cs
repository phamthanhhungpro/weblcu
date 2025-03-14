using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common;
using Datas.Models.ViewModels;

namespace Datas.Models.DomainModels;

public class DailyItem : BaseModel
{
    [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
    [Display(Name = "Mã vật dụng")]
    public string IdentityCode { get; set; }

    [Required(ErrorMessage = "Bạn cần nhập tên Vật dụng hàng ngày")]
    [Display(Name = "Tên vật dụng")]
    public string Name { set; get; }

    [Display(Name = "Tên gọi khác")]
    public string? AlternativeNames { get; set; }

    [Display(Name = "Loại vật dụng")]
    public int? CategoryId { get; set; }

    [Display(Name = "Loại vật dụng")]
    public virtual DailyItemCategory Category { get; set; }

    [Display(Name = "Khu vực phổ biến")]
    public string? CommonRegions { get; set; }

    [Display(Name = "Link ảnh scan 3D")]
    public string? Scan3DLink { get; set; }

    [Display(Name = "Chất liệu chính")]
    public string? MainMaterial { get; set; }

    [Display(Name = "Kích thước & Trọng lượng")]
    public string? SizeWeight { get; set; }

    [Display(Name = "Cấu tạo")]
    public string? Structure { get; set; }

    [Display(Name = "Mục đích sử dụng")]
    public string? Purpose { get; set; }

    [Display(Name = "Cách sử dụng")]
    public string? UsageMethod { get; set; }

    [Display(Name = "Đối tượng sử dụng")]
    public string? UserGroup { get; set; }

    [Display(Name = "Sự kiện liên quan")]
    public string? RelatedEvents { get; set; }

    [Display(Name = "Nguồn gốc & Xuất xứ")]
    public string? Origin { get; set; }

    [Display(Name = "Giá trị văn hóa")]
    public string? CulturalValue { get; set; }

    [Display(Name = "Sự thay đổi theo thời gian")]
    public string? HistoricalChanges { get; set; }

    [Display(Name = "Phong tục tập quán liên quan")]
    public string? RelatedCustoms { get; set; }

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

    public void SetNewData(DailyItem model)
    {
        IdentityCode = model.IdentityCode;
        Name = model.Name;
        AlternativeNames = model.AlternativeNames;
        CommonRegions = model.CommonRegions;
        Scan3DLink = model.Scan3DLink;
        MainMaterial = model.MainMaterial;
        SizeWeight = model.SizeWeight;
        Structure = model.Structure;
        Purpose = model.Purpose;
        UsageMethod = model.UsageMethod;
        UserGroup = model.UserGroup;
        RelatedEvents = model.RelatedEvents;
        Origin = model.Origin;
        CulturalValue = model.CulturalValue;
        HistoricalChanges = model.HistoricalChanges;
        RelatedCustoms = model.RelatedCustoms;
        PreservationMethod = model.PreservationMethod;
        DisplayedItems = model.DisplayedItems;

        // Original fields from base model
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

    public void SetNewData(DailyItemModel model)
    {
        IdentityCode = model.IdentityCode;
        Name = model.Name;
        AlternativeNames = model.AlternativeNames;
        CategoryId = model.CategoryId;
        CommonRegions = model.CommonRegions;
        Scan3DLink = model.Scan3DLink;
        MainMaterial = model.MainMaterial;
        SizeWeight = model.SizeWeight;
        Structure = model.Structure;
        Purpose = model.Purpose;
        UsageMethod = model.UsageMethod;
        UserGroup = model.UserGroup;
        RelatedEvents = model.RelatedEvents;
        Origin = model.Origin;
        CulturalValue = model.CulturalValue;
        HistoricalChanges = model.HistoricalChanges;
        RelatedCustoms = model.RelatedCustoms;
        PreservationMethod = model.PreservationMethod;
        DisplayedItems = model.DisplayedItems;

        // Original fields from model
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
        PeopleId = model.PeopleId;
    }

    public DailyItemModel ToModel()
    {
        var data = new DailyItemModel
        {
            Id = Id,
            IdentityCode = IdentityCode,
            Name = Name,
            AlternativeNames = AlternativeNames,
            CategoryId = CategoryId,
            CommonRegions = CommonRegions,
            Scan3DLink = Scan3DLink,
            MainMaterial = MainMaterial,
            SizeWeight = SizeWeight,
            Structure = Structure,
            Purpose = Purpose,
            UsageMethod = UsageMethod,
            UserGroup = UserGroup,
            RelatedEvents = RelatedEvents,
            Origin = Origin,
            CulturalValue = CulturalValue,
            HistoricalChanges = HistoricalChanges,
            RelatedCustoms = RelatedCustoms,
            PreservationMethod = PreservationMethod,
            DisplayedItems = DisplayedItems,

            // Original fields
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
