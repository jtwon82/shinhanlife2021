using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 배너관리 Access
    /// </summary>
    public class Banner
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 배너관리 생성자
        /// </summary>
        public Banner(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 배너관리 리스트
        /// </summary>
        public List<Model.Banner> List(int page, int size, string type)
        {
            List<Model.Banner> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BANNER_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Banner>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Banner banner = new Model.Banner()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Section = Convert.ToInt32(dr["SECTION"].ToString()),
                            Title = dr["TITLE"].ToString(),
                            AttPc = dr["ATT_PC"].ToString(),
                            AttMobile = dr["ATT_MOBILE"].ToString(),
                            Link = dr["LINK"].ToString(),
                            Sdate = dr["SDATE"].ToString(),
                            Edate = dr["EDATE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Name = dr["ADMIN_NAME"].ToString()
                            }
                        };

                        lists.Add(banner);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 배너관리 조회
        /// </summary>
        public Model.Banner Detail(string id, string type)
        {
            Model.Banner banner = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_BANNER_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    banner = new Model.Banner()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Section = Convert.ToInt32(dr["SECTION"].ToString()),
                        Title = dr["TITLE"].ToString(),
                        AttPc = dr["ATT_PC"].ToString(),
                        AttMobile = dr["ATT_MOBILE"].ToString(),
                        Link = dr["LINK"].ToString(),
                        Sdate = dr["SDATE"].ToString(),
                        Edate = dr["EDATE"].ToString(),
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

            return banner;
        }

        /// <summary>
        /// 배너관리 등록
        /// </summary>
        public bool Regist(Model.Banner banner)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", banner.Id));
            parameters.Add(new SqlParameter("@FK_ADMIN", banner.FkAdmin));
            parameters.Add(new SqlParameter("@TYPE", banner.Type));
            parameters.Add(new SqlParameter("@SECTION", banner.Section));
            parameters.Add(new SqlParameter("@TITLE", banner.Title));
            parameters.Add(new SqlParameter("@ATT_PC", banner.AttPc));
            parameters.Add(new SqlParameter("@ATT_MOBILE", banner.AttMobile));
            parameters.Add(new SqlParameter("@LINK", banner.Link));
            parameters.Add(new SqlParameter("@SDATE", banner.Sdate));
            parameters.Add(new SqlParameter("@EDATE", banner.Edate));
            parameters.Add(new SqlParameter("@USE_YN", banner.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BANNER_REGIST", parameters);
        }

        /// <summary>
        /// 배너관리 수정
        /// </summary>
        public bool Modify(Model.Banner banner)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", banner.Id));
            parameters.Add(new SqlParameter("@TYPE", banner.Type));
            parameters.Add(new SqlParameter("@SECTION", banner.Section));
            parameters.Add(new SqlParameter("@TITLE", banner.Title));
            parameters.Add(new SqlParameter("@ATT_PC", banner.AttPc));
            parameters.Add(new SqlParameter("@ATT_MOBILE", banner.AttMobile));
            parameters.Add(new SqlParameter("@LINK", banner.Link));
            parameters.Add(new SqlParameter("@SDATE", banner.Sdate));
            parameters.Add(new SqlParameter("@EDATE", banner.Edate));
            parameters.Add(new SqlParameter("@USE_YN", banner.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BANNER_MODIFY", parameters);
        }

        /// <summary>
        /// 배너관리 삭제
        /// </summary>
        public bool Delete(string id, string type)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_BANNER_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 배너관리 리스트
        /// </summary>
        public List<Model.Banner> UserList(string type)
        {
            List<Model.Banner> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_BANNER_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Banner>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Banner banner = new Model.Banner()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Section = Convert.ToInt32(dr["SECTION"].ToString()),
                            Title = dr["TITLE"].ToString(),
                            AttPc = dr["ATT_PC"].ToString(),
                            AttMobile = dr["ATT_MOBILE"].ToString(),
                            Link = dr["LINK"].ToString(),
                            Sdate = dr["SDATE"].ToString(),
                            Edate = dr["EDATE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString()
                        };

                        lists.Add(banner);
                    }
                }
            }

            return lists;
        }
        #endregion
    }
}