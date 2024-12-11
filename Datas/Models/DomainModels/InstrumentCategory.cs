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
    public class InstrumentCategory : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục nhạc cụ")]
        [Display(Name = "Tên danh mục nhạc cụ")]
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
        public virtual InstrumentCategory? Parent { get; set; }

        [Display(Name = "Danh mục con")]
        public virtual ICollection<InstrumentCategory>? Childrens { get; set; }

        public virtual ICollection<Instrument>? Instrument { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(InstrumentCategory newCategory)
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
        public void SetNewData(InstrumentCategoryModel data)
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
            return (Childrens!=null && Childrens.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal)) || (Instrument!=null &&  Instrument.Any(o => o.DeleteStatus == Common.Enums.DeleteStatus.Normal));
        }
        public InstrumentCategoryModel ToInstrumentCategoryModel()
        {
            var companyModel = new InstrumentCategoryModel
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