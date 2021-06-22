using MLib.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MLib.DataBase
{
    public static class DBHelper
    {
        #region [ ExecuteNonQuery ]
        /// <summary>
        /// DB 입력, 수정, 삭제 레코드셋이 없는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>bool 적용여부</returns>
        public static bool ExecuteNonQuery(string connection, string spName, SqlParameter[] parameters)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    result = cmd.ExecuteNonQuery();

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output
                            || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return (result > 0);
        }

        /// <summary>
        /// DB 입력, 수정, 삭제 레코드셋이 없는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>bool 적용여부</returns>
        public static bool ExecuteNonQuery(string connection, string spName, List<SqlParameter> parameters)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    result = cmd.ExecuteNonQuery();

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return (result > 0);
        }

        /// <summary>
        /// DB 입력, 수정, 삭제 레코드셋이 없는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <returns>bool 적용여부</returns>
        public static bool ExecuteNonQuery(string connection, string spName)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }

            return (result > 0);
        }

        /// <summary>
        /// DB 입력, 수정, 삭제 레코드셋이 없는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="query">쿼리</param>
        /// <returns>bool 적용여부</returns>
        public static bool ExecuteNonInQuery(string connection, string query)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = query;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }

            return (result > 0);
        }
        #endregion

        #region [ DataTable ]
        /// <summary>
        /// DB 레코드셋이 있는 SQL(날 쿼리용)
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="text">쿼리</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTableInQuery(string connection, string text)
        {
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = text;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string connection, string spName)
        {
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string connection, string spName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output
                            || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteDataTable(string connection, string spName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output
                            || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return dt;
        }
        #endregion

        #region [ DataSet ]
        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string connection, string spName, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output
                            || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string connection, string spName, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;
                    cmd.Parameters.AddRange(parameters.ToArray());

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }

                    // OUTPUT or RETURN 셋팅
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output
                            || parameter.Direction == ParameterDirection.ReturnValue)
                        {
                            cmd.Parameters[parameter.ParameterName].Value.ToString();
                        }
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// DB 레코드셋이 있는 SQL
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="spName">프로시져 명</param>
        /// <param name="parameters">파라메터</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string connection, string spName)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Set Identity on, update insert into, Set Identity on";
                    cmd.CommandText = spName;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            return ds;
        }
        #endregion
    }
}
