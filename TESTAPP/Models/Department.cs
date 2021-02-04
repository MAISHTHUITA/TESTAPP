﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SHOPLITE.Models
{
    public class Department
    {
        public string DeptCd { get; set; }
        public string DeptNm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        bool AddDepartment(Department department);
        bool EditDepartment(Department department);
        Department GetDepartment(string departmrntcode);
        bool DeleteDepartment(string departmentcode);
    }
    internal class DepartmentRepository : IDepartmentRepository
    {
        public bool AddDepartment(Department department)
        {
            using (SqlConnection con =new SqlConnection(DbCon.connection))
            {
                try
                {
                    
                    string query = "Insert into tblDept(DeptCd,DeptNm,CreatedBy) values(@DeptCd,@DeptNm,@CreatedBy)";
                    SqlCommand sql = new SqlCommand(query,con);
                    sql.Parameters.AddWithValue("@DeptCd", department.DeptCd);
                    sql.Parameters.AddWithValue("@DeptNm", department.DeptNm);
                    sql.Parameters.AddWithValue("@CreatedBy", department.CreatedBy);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                       SqlTransaction transaction= con.BeginTransaction();
                        sql.Transaction = transaction;
                        try
                        {
                            sql.ExecuteNonQuery();
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception exe)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                   
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

        public bool DeleteDepartment(string departmentcode)
        {
            throw new NotImplementedException();
        }

        public bool EditDepartment(Department department)
        {
            using (SqlConnection con=new SqlConnection(DbCon.connection))
            {
                SqlCommand cmd = new SqlCommand("spUpdateDept", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeptCd", department.DeptCd);
                cmd.Parameters.AddWithValue("@DeptNm", department.DeptNm);
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

                
            }
        }

        public Department GetDepartment(string departmentcode)
        {
            Department dept = new Department();
            using (SqlConnection con=new SqlConnection(DbCon.connection))
            {
                string query = "Select * from tbldept where deptcd = @deptcd";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@deptcd", departmentcode);
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                  
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (!rdr.HasRows)
                    {
                        return null;
                    }
                    while (rdr.Read())
                    {
                        dept.DeptCd = rdr["DeptCd"].ToString();
                        dept.DeptNm = rdr["DeptNm"].ToString();
                    }
                    return dept;
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public IEnumerable<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            using (SqlConnection con = new SqlConnection(DbCon.connection))
            {
                SqlCommand cmd = new SqlCommand("Select * from tbldept", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    Department department = new Department();
                    department.DeptCd = reader["DeptCd"].ToString();
                    department.DeptNm = reader["DeptNm"].ToString();
                    departments.Add(department);
                }
            }
            return departments;
        }
    }
}
