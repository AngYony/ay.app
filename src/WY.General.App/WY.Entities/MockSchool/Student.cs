using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.MockSchool
{

    /// <summary>
    /// 主修科目枚举
    /// </summary>
    public enum MajorEnum
    {
        [Display(Name = "未分配")]
        None,
        [Display(Name = "计算机科学")]
        ComputerScience,
        [Display(Name = "电子商务")]
        ElectronicCommerce,
        [Display(Name = "数学")]
        Mathematics
    }

    public  class Student
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "姓名")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 主修科目
        /// </summary>
        public MajorEnum? Major { get; set; }

        public string PhotoPath { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }

        /// <summary>
        /// 入学时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }


        /// <summary>
        /// 导航属性，ICollection会被转换为HashSet集合，意味着StudentCourses不能包含重复项
        /// </summary>
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
