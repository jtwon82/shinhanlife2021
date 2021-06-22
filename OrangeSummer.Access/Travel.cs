using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.17
    /// 여행지 Access
    /// </summary>
    public class Travel
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 여행지 생성자
        /// </summary>
        public Travel(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 여행지 리스트
        /// </summary>
        public List<Model.Travel> List(int page, int size)
        {
            List<Model.Travel> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_TRAVEL_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Travel>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Travel travel = new Model.Travel()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            Section = Convert.ToInt32(dr["SECTION"].ToString()),
                            Title = dr["TITLE"].ToString(),
                            Name = dr["NAME"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            AttPc = dr["ATT_PC"].ToString(),
                            AttMobile = dr["ATT_MOBILE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Name = dr["ADMIN_NAME"].ToString()
                            }
                        };

                        lists.Add(travel);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 여행지 리스트(일렬용)
        /// </summary>
        public List<Model.Travel> Line()
        {
            List<Model.Travel> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_TRAVEL_LINE"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Travel>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Travel travel = new Model.Travel()
                        {
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            Section = Convert.ToInt32(dr["SECTION"].ToString()),
                            Title = dr["TITLE"].ToString(),
                            Name = dr["NAME"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            AttPc = dr["ATT_PC"].ToString(),
                            AttMobile = dr["ATT_MOBILE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(travel);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 여행지 조회
        /// </summary>
        public Model.Travel Detail(string id)
        {
            Model.Travel travel = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_TRAVEL_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    travel = new Model.Travel()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString(),
                        Section = Convert.ToInt32(dr["SECTION"].ToString()),
                        Title = dr["TITLE"].ToString(),
                        Name = dr["NAME"].ToString(),
                        AttFile = dr["ATT_FILE"].ToString(),
                        AttPc = dr["ATT_PC"].ToString(),
                        AttMobile = dr["ATT_MOBILE"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Admin = new Model.Admin()
                        {
                            Name = dr["ADMIN_NAME"].ToString()
                        }
                    };
                }
            }

            return travel;
        }

        /// <summary>
        /// 여행지 구분 조회
        /// </summary>
        public bool Check(int section)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SECTION", section));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_TRAVEL_CHECK", parameters))
            {
                return (dt.Rows.Count > 0) ? true : false;
            }
        }

        /// <summary>
        /// 여행지 등록
        /// </summary>
        public bool Regist(Model.Travel travel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", travel.Id));
            parameters.Add(new SqlParameter("@FK_ADMIN", travel.FkAdmin));
            parameters.Add(new SqlParameter("@SECTION", travel.Section));
            parameters.Add(new SqlParameter("@TITLE", travel.Title));
            parameters.Add(new SqlParameter("@NAME", travel.Name));
            parameters.Add(new SqlParameter("@ATT_FILE", travel.AttFile));
            parameters.Add(new SqlParameter("@ATT_PC", travel.AttPc));
            parameters.Add(new SqlParameter("@ATT_MOBILE", travel.AttMobile));
            parameters.Add(new SqlParameter("@USE_YN", travel.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_TRAVEL_REGIST", parameters);
        }

        /// <summary>
        /// 여행지 수정
        /// </summary>
        public bool Modify(Model.Travel travel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", travel.Id));
            parameters.Add(new SqlParameter("@SECTION", travel.Section));
            parameters.Add(new SqlParameter("@TITLE", travel.Title));
            parameters.Add(new SqlParameter("@NAME", travel.Name));
            parameters.Add(new SqlParameter("@ATT_FILE", travel.AttFile));
            parameters.Add(new SqlParameter("@ATT_PC", travel.AttPc));
            parameters.Add(new SqlParameter("@ATT_MOBILE", travel.AttMobile));
            parameters.Add(new SqlParameter("@USE_YN", travel.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_TRAVEL_MODIFY", parameters);
        }

        /// <summary>
        /// 여행지 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_TRAVEL_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 여행지 리스트(일렬용)
        /// </summary>
        public List<Model.Travel> UserLine()
        {
            List<Model.Travel> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_TRAVEL_LINE"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Travel>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Travel travel = new Model.Travel()
                        {
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString(),
                            Section = Convert.ToInt32(dr["SECTION"].ToString()),
                            Title = dr["TITLE"].ToString(),
                            Name = dr["NAME"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(travel);
                    }
                }
            }

            return lists;
        }
        #endregion
    }
}