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
    public class PeopleCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên loại dân tộc")]
        [Display(Name = "Loại dân tộc")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Loại dân tộc cha")]
        public int? ParentId { get; set; }
        public virtual PeopleCategory? Parent { get; set; }

        [Display(Name = "Loại dân tộc con")]
        public virtual ICollection<PeopleCategory>? Childrens { get; set; }

        public virtual ICollection<People>? Peoples { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(PeopleCategory newCategory)
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
        public void SetNewData(PeopleCategoryModel data)
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
            return (Childrens!=null && Childrens.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal)) || (Peoples!=null && Peoples.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public PeopleCategoryModel ToPeopleCategoryModel()
        {
            var companyModel = new PeopleCategoryModel
            {
                Id = Id,
                Image = Image,
                Name = Name,
                KeyWord = KeyWord,
                Details = Details,
                Status = Status,
                ParentId = ParentId,
                Url = Url
            };
            return companyModel;
        }
    }
}
