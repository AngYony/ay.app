﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.MockSchool
{
    /// <summary>
    /// 系部
    /// </summary>
    public class Department
    {
        public int DepartmentID { get; set; }
        [Display(Name = "院系名称")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// 预算
        /// </summary>
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "预算")]

        public decimal Budget { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "成立时间")]
        public DateTime StartDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Display(Name = "负责人")]

        public int? TeacherID { get; set; }
        /// <summary>
        /// 系部主任
        /// </summary
        public virtual Teacher Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
