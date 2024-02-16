using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.MockSchool
{
    /// <summary>
    /// 课程设置分配
    /// </summary>

    public class CourseAssignment
    {
        public int CourseAssignmentID { get; set; }
        public int TeacherID { get; set; }
        public int CourseID { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
    }
}
