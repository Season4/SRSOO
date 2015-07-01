using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;

namespace SRSOO.SqlServerDAL
{
   public class CourseDAO: DataBase, ISection
    {
       public void Insert(Course course)
       {
           throw new NotImplementedException();
       }

 
       public Section GetSection(int sectionNumber)
       {
           //应该从数据库读取section数据
           string sql = "select * from Section where SectionNumber={0}".FormatWith(sectionNumber);
           if (dr.HasRows == false) return null;
           dr.Read();
           var courseDAO = new CourseDAO();
           var sec = new Section(dr["SectionNumber"].ConvertToIntBaseZero(),
                                 dr["DayOfWeek"].ToString(),
                                 dr["TimeOfDay"].ToString(),
                                 courseDAO.GetCourse(dr["RepresentedCourse"].ConverToString()),
                                 dr["Room"].ToString(),
                                 dr["Capacity"].ConvertToIntBaseZero());
           dr.Close();
           dr.Dispose();

           return sec;


       }

       public void GetPreRequisites(Course course)
       {
           string sql = "select * from Prerequisite where CourseNumber='{0}'".FormatWith(course.CourseNumber);
           DataTable dt = SqlHelper.ExecuteDataset(ConStr, CommandType.Text, sql).Tables[0];
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               course.AddPrerequisite(GetCourse(dt.Rows[i]["Prerequisite"].ConvertToString()));
           }
       }
    }
}
