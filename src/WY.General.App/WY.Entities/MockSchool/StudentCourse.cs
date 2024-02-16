using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.MockSchool
{
    /// <summary>
    /// 成绩
    /// </summary>
    public enum Grade
    {
        A, B, C, D, F
    }

    /// <summary>
    /// 学生的课程学分，存储Student与Course多对多的关系
    /// </summary>
    public class StudentCourse
    {
        [Key]
        public int StudentCourseId { get; set; }


        public int CourseID { get; set; }
        public int StudentID { get; set; }


        [DisplayFormat(NullDisplayText = "无成绩")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        /// <summary>
        /// 引用导航属性
        /// </summary>
        public virtual Student Student { get; set; }

    }
}
