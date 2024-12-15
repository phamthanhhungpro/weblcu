using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class DailyItemCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục vật dụng hàng ngày")]
        [Display(Name = "Tên danh mục vật dụng hàng ngày")]
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
        public virtual DailyItemCategory? Parent { get; set; }

        [Display(Name = "Danh mục con")]
        public virtual ICollection<DailyItemCategory>? Childrens { get; set; }

        public virtual ICollection<DailyItem>? DailyItems { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(DailyItemCategory model)
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
        public void SetNewData(DailyItemCategoryModel data)
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
                || (DailyItems != null
                && DailyItems.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public DailyItemCategoryModel ToModel()
        {
            var companyModel = new DailyItemCategoryModel
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