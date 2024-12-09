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
    public class NationalCostumeCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên loại trang phục dân tộc")]
        [Display(Name = "Loại trang phục dân tộc")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Loại trang phục dân tộc cha")]
        public int? ParentId { get; set; }
        public virtual NationalCostumeCategory? Parent { get; set; }

        [Display(Name = "Loại trang phục dân tộc con")]
        public virtual ICollection<NationalCostumeCategory>? Childrens { get; set; }

        public virtual ICollection<NationalCostume>? NationalCostumes { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(NationalCostumeCategory data)
        {

            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            ParentId = data.ParentId;
            Parent = data.Parent;
            Childrens = data.Childrens;
        }
        public void SetNewData(NationalCostumeCategoryModel data)
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
            return (Childrens!=null && Childrens.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal)) || (NationalCostumes != null && NationalCostumes.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public NationalCostumeCategoryModel ToNationalCostumeCategoryModel()
        {
            var companyModel = new NationalCostumeCategoryModel
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
