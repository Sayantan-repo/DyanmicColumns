using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebAppDemo.ViewModels;

namespace WebAppDemo.Utility
{
    public static class DataAccessHelper
    {

        public static List<string> GetAllIncludedColumns(string pageName)
        {
            List<string> cols = new List<string>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select DisplayName from MasterView where IsIncluded=1 and PageName='" + pageName + "' order by ColumnIndex", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        cols.Add(Convert.ToString(sdr["DisplayName"]));
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //Log the error..
            }

            return cols;
        }


        public static List<StudentViewModel> GetAllStudents()
        {
            var cs = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            var students = new List<StudentViewModel>();
            using (var con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand("Select * from Students", con) { CommandType = CommandType.Text };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var student = new StudentViewModel
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Email = dr[2].ToString(),
                        ContactNumber = dr[3].ToString(),
                        City = dr[4].ToString(),
                        State = dr[5].ToString(),
                        Country = dr[6].ToString(),
                        Zip = dr[7].ToString(),
                        Gender = dr[8].ToString(),
                        BloodGroup = dr[9].ToString(),

                    };
                    students.Add(student);
                }
                con.Close();
            }

            return students;

        }

        public static string GetDynamicColumns(string pageName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RFP_ID");

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select ColumnName from MasterView where IsIncluded=1 and PageName='" + pageName + "' order by ColumnIndex", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        sb.Append("," + Convert.ToString(sdr["ColumnName"]));
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //Log the error..
            }
            return sb.ToString();
        }


        public static List<AllColumns> GetAllColumns(string pageName)
        {
            List<AllColumns> lstCol = new List<AllColumns>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select Id,DisplayName,IsIncluded,IsExcluded from MasterView where PageName='" + pageName + "' order by ColumnIndex", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        if(Convert.ToBoolean(sdr["IsIncluded"])==true)
                        {
                            lstCol.Add(new AllColumns { Id = Convert.ToInt32(sdr["Id"]), ColumnName = Convert.ToString(sdr["DisplayName"]), ColumnType = "Incl" });
                        }
                        else
                        {
                            lstCol.Add(new AllColumns { Id = Convert.ToInt32(sdr["Id"]), ColumnName = Convert.ToString(sdr["DisplayName"]), ColumnType = "Excl" });
                        }
                        
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //Log the error..
            }

            return lstCol;
        }

        public static void UpdateInculdedColumns(int Id,string pageName)
        {
            string query = "UPDATE MasterView SET IsIncluded=1,IsExcluded=0 WHERE Id=@Id and PageName=@PageName";
            string constr = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PageName", pageName);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void UpdateExcludedColumns(int Id, string pageName)
        {
            string query = "UPDATE MasterView SET IsIncluded=0,IsExcluded=1 WHERE Id=@Id and PageName=@PageName";
            string constr = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PageName", pageName);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}