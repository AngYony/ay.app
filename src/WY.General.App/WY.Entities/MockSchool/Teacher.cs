using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.MockSchool
{
    /// <summary>
    /// 教师信息
    /// </summary>
    public class Teacher 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "姓名")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "聘用时间")]
        public DateTime HireDate { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }

        public virtual OfficeLocation OfficeLocation { get; set; }
    }
}
