using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common;
using Datas.Models.ViewModels;

namespace Datas.Models.DomainModels;

public class Instrument : AbstractData
{
        [Required(ErrorMessage = "Bạn cần nhập Tên nhạc cụ")]
        [Display(Name = "Tên nhạc cụ")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Thời gian đăng")]
        [Required(ErrorMessage = "Bạn cần chọn thời gian")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; } = DateTime.Now;

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name = "Chuyên mục")]
        public  int? CategoryId { get; set; }

        [Display(Name = "Chuyên mục")]
        public virtual InstrumentCategory? Category { get; set; }

        [Display(Name = "Nội dung bài viết")]
        public string? Content { get; set; }
    
        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Name);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(Instrument data)
        {

            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Category = data.Category;
            Top = data.Top;
            PostDate = data.PostDate;
            Content = data.Content;
            CategoryId = data.CategoryId;
        }
        public void SetNewData(InstrumentModel data)
        {
            Name = data.Name;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Top = data.Top;
            PostDate = data.PostDate;
            Content = data.Content;
            Status = data.Status;
            CategoryId = data.CategoryId;
        }

        public InstrumentModel ToInstrumentModel()
        {
            var model = new InstrumentModel
            {
                Id = Id,
                Image = Image,
                Content = Content,
                PostDate = PostDate,
                Name = Name,
                Top = Top,
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