using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common;
using Datas.Models.ViewModels;

namespace Datas.Models.DomainModels;

public class CeremonialTool : BaseModel
{
    [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
    [Display(Name = "Mã dụng cụ")]
    public string IdentityCode { get; set; }

    [Required(ErrorMessage = "Bạn cần nhập tên Dụng cụ nghi lễ")]
    [Display(Name = "Tên dụng cụ")]
    public string Name { set; get; }

    [Display(Name = "Tên gọi khác")]
    public string? AlternativeNames { get; set; }

    [Display(Name = "Loại dụng cụ")]
    public int? CategoryId { get; set; }

    [Display(Name = "Loại dụng cụ")]
    public virtual CeremonialToolCategory Category { get; set; }

    [Display(Name = "Tôn giáo/Tín ngưỡng liên quan")]
    public string? Religion { get; set; }

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

    [Display(Name = "Nghi lễ liên quan")]
    public string? RelatedRituals { get; set; }

    [Display(Name = "Nhân vật sử dụng")]
    public string? UsageCharacters { get; set; }

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

    public void SetNewData(CeremonialTool model)
    {
        IdentityCode = model.IdentityCode;
        Name = model.Name;
        AlternativeNames = model.AlternativeNames;
        Religion = model.Religion;
        CommonRegions = model.CommonRegions;
        Scan3DLink = model.Scan3DLink;
        MainMaterial = model.MainMaterial;
        SizeWeight = model.SizeWeight;
        Structure = model.Structure;
        Purpose = model.Purpose;
        UsageMethod = model.UsageMethod;
        RelatedRituals = model.RelatedRituals;
        UsageCharacters = model.UsageCharacters;
        Origin = model.Origin;
        CulturalValue = model.CulturalValue;
        HistoricalChanges = model.HistoricalChanges;
        RelatedCustoms = model.RelatedCustoms;
        PreservationMethod = model.PreservationMethod;
        DisplayedItems = model.DisplayedItems;

        // Original fields
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

    public void SetNewData(CeremonialToolModel model)
    {
        IdentityCode = model.IdentityCode;
        Name = model.Name;
        AlternativeNames = model.AlternativeNames;
        CategoryId = model.CategoryId;
        Religion = model.Religion;
        CommonRegions = model.CommonRegions;
        Scan3DLink = model.Scan3DLink;
        MainMaterial = model.MainMaterial;
        SizeWeight = model.SizeWeight;
        Structure = model.Structure;
        Purpose = model.Purpose;
        UsageMethod = model.UsageMethod;
        RelatedRituals = model.RelatedRituals;
        UsageCharacters = model.UsageCharacters;
        Origin = model.Origin;
        CulturalValue = model.CulturalValue;
        HistoricalChanges = model.HistoricalChanges;
        RelatedCustoms = model.RelatedCustoms;
        PreservationMethod = model.PreservationMethod;
        DisplayedItems = model.DisplayedItems;

        // Original fields
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

    public CeremonialToolModel ToModel()
    {
        var data = new CeremonialToolModel
        {
            Id = Id,
            IdentityCode = IdentityCode,
            Name = Name,
            AlternativeNames = AlternativeNames,
            CategoryId = CategoryId,
            Religion = Religion,
            CommonRegions = CommonRegions,
            Scan3DLink = Scan3DLink,
            MainMaterial = MainMaterial,
            SizeWeight = SizeWeight,
            Structure = Structure,
            Purpose = Purpose,
            UsageMethod = UsageMethod,
            RelatedRituals = RelatedRituals,
            UsageCharacters = UsageCharacters,
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
