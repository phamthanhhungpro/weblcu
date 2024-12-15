using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class JewelryCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục trang sức")]
        [Display(Name = "Tên danh mục trang sức")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
        public virtual JewelryCategory? Parent { get; set; }

        [Display(Name = "Danh mục con")]
        public virtual ICollection<JewelryCategory>? Childrens { get; set; }

        public virtual ICollection<Jewelry>? Jewelrys { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(JewelryCategory model)
        {

            Name = model.Name;
            Image = model.Image;
            KeyWord = model.KeyWord;
            Details = model.Details;
            Status = model.Status;
            ParentId = model.ParentId;
            Parent = model.Parent;
            Childrens = model.Childrens;
        }
        public void SetNewData(JewelryCategoryModel data)
        {
            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            ParentId = data.ParentId;
            Status = data.Status;
        }
        public override bool IsExistAnother()
        {
            return (Childrens!=null && Childrens.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal))
                || (Jewelrys != null
                && Jewelrys.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public JewelryCategoryModel ToModel()
        {
            var companyModel = new JewelryCategoryModel
            {
                Id = Id,
                Image = Image,
                Name = Name,
                KeyWord = KeyWord,
                Details = Details,
                Status = Status,
                ParentId = ParentId,
            };
            return companyModel;
        }
    }
}
