﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datas.Models.ViewModels
{
    public class CeremonialToolModel : BaseModelViewModel
    {
        private string _categoryName = string.Empty;
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
        [Display(Name = "Mã dụng cụ")]
        public string IdentityCode { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên dụng cụ nghi lễ")]
        [Display(Name = "Tên dụng cụ")]
        public string Name { set; get; }

        [Display(Name = "Tên gọi khác")]
        public string? AlternativeNames { get; set; }

        [Display(Name = "Loại dụng cụ")]
        public int? CategoryId { get; set; }

        [Display(Name = "Tôn giáo/Tín ngưỡng liên quan")]
        public string? Religion { get; set; }

        [Display(Name = "Khu vực phổ biến")]
        public string? CommonRegions { get; set; }

        [Display(Name = "Link ảnh scan 3D")]
        public string? Scan3DLink { get; set; }

        [Display(Name = "Chất liệu chính")]
        public string? MainMaterial { get; set; }

        [Display(Name = "Kích thước & Trọng lượng")]
        public string? SizeWeight { get; set; }

        [Display(Name = "Cấu tạo")]
        public string? Structure { get; set; }

        [Display(Name = "Mục đích sử dụng")]
        public string? Purpose { get; set; }

        [Display(Name = "Cách sử dụng")]
        public string? UsageMethod { get; set; }

        [Display(Name = "Nghi lễ liên quan")]
        public string? RelatedRituals { get; set; }

        [Display(Name = "Nhân vật sử dụng")]
        public string? UsageCharacters { get; set; }

        [Display(Name = "Nguồn gốc & Xuất xứ")]
        public string? Origin { get; set; }

        [Display(Name = "Giá trị văn hóa")]
        public string? CulturalValue { get; set; }

        [Display(Name = "Sự thay đổi theo thời gian")]
        public string? HistoricalChanges { get; set; }

        [Display(Name = "Phong tục tập quán liên quan")]
        public string? RelatedCustoms { get; set; }

        [Display(Name = "Cách bảo quản")]
        public string? PreservationMethod { get; set; }

        [Display(Name = "Hiện vật trưng bày")]
        public string? DisplayedItems { get; set; }

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

