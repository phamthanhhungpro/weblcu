using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class NationalCostumeModel : AbstractData
    {

        private string _peopleName = string.Empty;
        private string _categoryName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên trang phục dân tộc")]
        [Display(Name = "Tên trang phục dân tộc")]
        public string Name { set; get; }

        [Display(Name = "Ký hiệu, mã số ảnh")]
        public string? Code { set; get; }

        [Display(Name = "Dịp sử dụng")]
        public string? Event { set; get; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { set; get; } = true;

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Ảnh 360")]
        public string? Image360 { set; get; }

        [Display(Name = "Phụ kiện đi kèm")]
        public string? Shape { set; get; }

        [Display(Name = "Hiện trạng")]
        public string? CurrentStatus { set; get; }

        [Display(Name = "Kỹ thuật chế tác")]
        public string? Technique { set; get; }

        [Display(Name = "Ý nghĩa văn hóa")]
        public string? Classify { set; get; }

        [Display(Name = "Nguyên tắc thiết kế")]
        public string? Certification { set; get; }

        [Display(Name = "Nguyên liệu sử dụng")]
        public string? Material { set; get; }

        [Display(Name = "Giá cả trung bình")]
        public string? Cost { set; get; }

        [Display(Name = "Màu sắc")]
        public string? Color { set; get; }

        [Display(Name = "Kích thước")]
        [DataType(DataType.MultilineText)]
        public string? Size { set; get; }

        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Nội dung bài viết")]
        public string Content { get; set; }

        [Display(Name = "Loại trang phục dân tộc")]
        public int? CategoryId { get; set; }

        [Display(Name = "Dân tộc")]
        [Required(ErrorMessage = "Bạn cần nhập dân tộc")]
        public int? PeopleId { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? Image0 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image1 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image2 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image3 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image4 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image5 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image6 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image7 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image8 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image9 { set; get; }

        public string? Url { set; get; }

        public List<AttachmentModel>? Attachments { set; get; } = new List<AttachmentModel>();

        public void SetCategoryName(string data)
        {
            _categoryName = data;
        }

        public string GetCategoryName()
        {
            return _categoryName;
        }

        public void SetPeopleName(string data)
        {
            _peopleName = data;
        }

        public string GetPeopleName()
        {
            return _peopleName;
        }
    }
}
