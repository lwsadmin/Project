using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Domain.Base
{
    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }


        /// <summary>
        /// 租户Id. 0表示宿主
        /// </summary>
        [Display(Name = "租户Id")]
        [Required]
        public int TenantId { get; set; } = 0;

        [Display(Name = "添加时间")]
        [Required]
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
