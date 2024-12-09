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
    public class NewsCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục tin tức")]
        [Display(Name = "Tên danh mục tin tức")]
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
        public virtual NewsCategory? Parent { get; set; }

        [Display(Name = "Danh mục con")]
        public virtual ICollection<NewsCategory>? Childrens { get; set; }

        public virtual ICollection<News>? News { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(NewsCategory newCategory)
        {

            Name = newCategory.Name;
            Image = newCategory.Image;
            KeyWord = newCategory.KeyWord;
            Details = newCategory.Details;
            Status = newCategory.Status;
            ParentId = newCategory.ParentId;
            Parent = newCategory.Parent;
            Childrens = newCategory.Childrens;
        }
        public void SetNewData(NewsCategoryModel data)
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
            return (Childrens!=null && Childrens.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal)) || (News!=null &&  News.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public NewsCategoryModel ToNewsCategoryModel()
        {
            var companyModel = new NewsCategoryModel
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
