using Common;
using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Datas.Models.DomainModels
{
    public class ProduceTool : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        [Display(Name = "Tên")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name = "Chuyên mục")]
        public  int? CategoryId { get; set; }

        [Display(Name = "Chuyên mục")]
        public virtual ProduceToolCategory? Category { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(ProduceTool data)
        {

            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Category = data.Category;
            CategoryId = data.CategoryId;
        }
        public void SetNewData(ProduceToolModel data)
        {
            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Category = data.Category;
            CategoryId = data.CategoryId;
        }

        public ProduceToolModel ToModel()
        {
            var model = new ProduceToolModel
            {
                Id = Id,
                Image = Image,
                Name = Name,
                KeyWord = KeyWord,
                Details = Details,
                Status = Status,
                View = View,
                Url = Url,
                CategoryId = CategoryId
            };
            return model;
        }
    }
}
