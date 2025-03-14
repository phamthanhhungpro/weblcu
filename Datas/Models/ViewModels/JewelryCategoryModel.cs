﻿using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class JewelryCategoryModel : AbstractData
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
    }
}
