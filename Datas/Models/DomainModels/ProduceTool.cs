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
    public class ProduceTool : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Tác giả")]
        public string? Author { get; set; }

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
        public virtual NewsCategory? Category { get; set; }

        [Display(Name = "Nội dung bài viết")]
        public string? Content { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Location { set; get; }

        [Display(Name = "Vĩ độ (lat)")]
        public string? Lat { set; get; }

        [Display(Name = "Kinh độ (long)")]
        public string? Long { set; get; }

        [Display(Name = "Điện thoại")]
        public string? Phone { set; get; }

        [Display(Name = "Lịch")]
        public string? Calendar { set; get; }

        //public override bool IsExistAnother()
        //{
        //    return ;
        //}
        public int View { get; set; }

        public string? Url { set; get; }

        public void ReNewUrl()
        {
            var newUrl = ExtensionMethod.RemoveUnicode(Title);
            newUrl = newUrl.Replace(" ", "-").ToLower();
            newUrl = Regex.Replace(newUrl, @"[^a-zA-Z0-9 -]", string.Empty);
            Url = newUrl;
        }

        public void SetNewData(News data)
        {

            Title = data.Title;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Category = data.Category;
            Top = data.Top;
            Author = data.Author;
            PostDate = data.PostDate;
            Content = data.Content;
            Lat = data.Lat;
            Long = data.Long;
            Calendar = data.Calendar;
            Location = data.Location;
            CategoryId = data.CategoryId;
            Phone = data.Phone;
        }
        public void SetNewData(NewsModel data)
        {
            Title = data.Title;
            Image = data.Image;
            KeyWord = data.KeyWord;
            Details = data.Details;
            Status = data.Status;
            Top = data.Top;
            Author = data.Author;
            PostDate = data.PostDate;
            Content = data.Content;
            Status = data.Status;
            Lat = data.Lat;
            Long = data.Long;
            Calendar = data.Calendar;
            Location = data.Location;
            CategoryId = data.CategoryId;
            Phone = data.Phone;
        }

        public NewsModel ToNewsModel()
        {
            var model = new NewsModel
            {
                Id = Id,
                Image = Image,
                Author = Author,
                Content = Content,
                PostDate = PostDate,
                Title = Title,
                Top = Top,
                KeyWord = KeyWord,
                Details = Details,
                Status = Status,
                View = View,
                Url = Url,
                Lat = Lat,
                Long = Long,
                Calendar = Calendar,
                Location = Location,
                Phone = Phone,
                CategoryId = CategoryId
            };
            return model;
        }
    }
}
