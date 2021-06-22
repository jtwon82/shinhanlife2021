using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 지점관리 Access
    /// </summary>
    public class Branch
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 지점관리 생성자
        /// </summary>
        public Branch(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> List(int page, int size, string branch, string travel)
        {
            List<Model.Branch> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@KEY_BRANCH", branch));
            parameters.Add(new SqlParameter("@KEY_TRAVEL", travel));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BRANCH_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Branch>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Branch model = new Model.Branch()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            FkTravel = dr["FK_TRAVEL"].ToString(),
                            Name = dr["NAME"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(), 
                            Travel = new Model.Travel() 
                            { 
                                Name = dr["TRAVEL_NAME"].ToString()
                            }
                        };

                        lists.Add(model);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> Line()
        {
            List<Model.Branch> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BRANCH_LINE"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Branch>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Branch model = new Model.Branch()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            FkTravel = dr["FK_TRAVEL"].ToString(),
                            Name = dr["NAME"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(model);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 지점관리 조회
        /// </summary>
        public Model.Branch Detail(string id)
        {
            Model.Branch branch = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BRANCH_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    branch = new Model.Branch()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                        Name = dr["NAME"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Admin = new Model.Admin()
                        {
                            Name = dr["ADMIN_NAME"].ToString()
                        }
                    };
                }
            }

            return branch;
        }

        /// <summary>
        /// 지점관리 중복체크
        /// </summary>
        public Model.Branch Check(string name)
        {
            Model.Branch branch = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NAME", name));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BRANCH_CHECK", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    branch = new Model.Branch()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        FkTravel = dr["FK_TRAVEL"].ToString(),
                        Name = dr["NAME"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString()
                    };
                }
            }

            return branch;
        }

        /// <summary>
        /// 지점관리 등록
        /// </summary>
        public bool Regist(Model.Branch branch)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", branch.Id));
            parameters.Add(new SqlParameter("@FK_ADMIN", branch.FkAdmin));
            parameters.Add(new SqlParameter("@FK_TRAVEL", branch.FkTravel));
            parameters.Add(new SqlParameter("@NAME", branch.Name));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BRANCH_REGIST", parameters);
        }

        /// <summary>
        /// 지점관리 수정
        /// </summary>
        public bool Modify(Model.Branch branch)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", branch.Id));
            parameters.Add(new SqlParameter("@FK_TRAVEL", branch.FkTravel));
            parameters.Add(new SqlParameter("@DEL_YN", branch.DelYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BRANCH_MODIFY", parameters);
        }

        /// <summary>
        /// 지점관리 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BRANCH_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> UserLine()
        {
            List<Model.Branch> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_BRANCH_LINE"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Branch>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Branch model = new Model.Branch()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            FkTravel = dr["FK_TRAVEL"].ToString(),
                            Name = dr["NAME"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(model);
                    }
                }
            }

            return lists;
        }
        #endregion
    }
}