using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.16
    /// 관리자 Access
    /// </summary>
    public class Admin
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 관리자 생성자
        /// </summary>
        public Admin(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        public List<Model.Admin> List(int page, int size)
        {
            List<Model.Admin> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_ADMIN_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Admin>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Admin admin = new Model.Admin()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            Usr = dr["USR"].ToString(),
                            Pwd = dr["PWD"].ToString(),
                            Name = dr["NAME"].ToString(),
                            Reset = dr["RESET"].ToString(),
                            Phone = dr["PHONE"].ToString(),
                            Email = dr["EMAIL"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(admin);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 관리자 조회
        /// </summary>
        public Model.Admin Detail(string id)
        {
            Model.Admin admin = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_ADMIN_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    admin = new Model.Admin()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        Usr = dr["USR"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Phone = dr["PHONE"].ToString(),
                        Email = dr["EMAIL"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Adm = new Model.Admin()
                        {
                            Name = dr["ADMIN_NAME"].ToString()
                        }
                    };
                }
            }

            return admin;
        }

        /// <summary>
        /// 아이디 체크
        /// </summary>
        public Model.Admin Check(string usr)
        {
            Model.Admin admin = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@USR", usr));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_ADMIN_CHECK", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    admin = new Model.Admin()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        Usr = dr["USR"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Phone = dr["PHONE"].ToString(),
                        Email = dr["EMAIL"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString()
                    };
                }
            }

            return admin;
        }

        /// <summary>
        /// 관리자 로그인
        /// </summary>
        public Model.Admin Login(string usr, string pwd)
        {
            Model.Admin admin = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@USR", usr));
            parameters.Add(new SqlParameter("@PWD", pwd));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_ADMIN_LOGIN", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    admin = new Model.Admin()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        Usr = dr["USR"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Phone = dr["PHONE"].ToString(),
                        Email = dr["EMAIL"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString()
                    };
                }
            }

            return admin;
        }

        /// <summary>
        /// 관리자 등록
        /// </summary>
        public bool Regist(Model.Admin admin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", admin.Id));
            parameters.Add(new SqlParameter("@FK_ADMIN", admin.FkAdmin));
            parameters.Add(new SqlParameter("@USR", admin.Usr));
            parameters.Add(new SqlParameter("@PWD", admin.Pwd));
            parameters.Add(new SqlParameter("@NAME", admin.Name));
            parameters.Add(new SqlParameter("@RESET", admin.Reset));
            parameters.Add(new SqlParameter("@PHONE", admin.Phone));
            parameters.Add(new SqlParameter("@EMAIL", admin.Email));
            parameters.Add(new SqlParameter("@USE_YN", admin.UseYn));
            parameters.Add(new SqlParameter("@DEL_YN", admin.DelYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_ADMIN_REGIST", parameters);
        }

        /// <summary>
        /// 관리자 수정
        /// </summary>
        public bool Modify(Model.Admin admin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", admin.Id));
            parameters.Add(new SqlParameter("@NAME", admin.Name));
            parameters.Add(new SqlParameter("@PHONE", admin.Phone));
            parameters.Add(new SqlParameter("@EMAIL", admin.Email));
            parameters.Add(new SqlParameter("@USE_YN", admin.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_ADMIN_MODIFY", parameters);
        }

        /// <summary>
        /// 관리자 비밀번호 재설정
        /// </summary>
        public bool Reset(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_ADMIN_RESET", parameters);
        }

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_ADMIN_DELETE", parameters);
        }

        /// <summary>
        /// 관리자 비밀번호 수정
        /// </summary>
        public bool Pwd(string id, string pwd)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@PWD", pwd));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_ADMIN_PWD", parameters);
        }
    }
}