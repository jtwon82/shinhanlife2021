using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 공지사항 Access
    /// </summary>
    public class Notice
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 공지사항 생성자
        /// </summary>
        public Notice(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 공지사항 리스트
        /// </summary>
        public List<Model.Notice> List(int page, int size, string type, string title, string use, string sdate, string edate)
        {
            List<Model.Notice> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@KEY_TYPE", type));
            parameters.Add(new SqlParameter("@KEY_TITLE", title));
            parameters.Add(new SqlParameter("@KEY_USE", use));
            parameters.Add(new SqlParameter("@KEY_SDATE", sdate));
            parameters.Add(new SqlParameter("@KEY_EDATE", edate));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_NOTICE_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Notice>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Notice notice = new Model.Notice()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Title = dr["TITLE"].ToString(),
                            Contents = dr["CONTENTS"].ToString(),
                            Url = dr["URL"].ToString(),
                            AttImage = dr["ATT_IMAGE"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Name = dr["ADMIN_NAME"].ToString()
                            }
                        };

                        lists.Add(notice);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 공지사항 메인 리스트
        /// </summary>
        public List<Model.Notice> Main()
        {
            List<Model.Notice> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_NOTICE_MAIN"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Notice>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Notice notice = new Model.Notice()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Title = dr["TITLE"].ToString(),
                            Contents = dr["CONTENTS"].ToString(),
                            Url = dr["URL"].ToString(),
                            AttImage = dr["ATT_IMAGE"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Name = dr["ADMIN_NAME"].ToString()
                            }
                        };

                        lists.Add(notice);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 공지사항 조회
        /// </summary>
        public Model.Notice Detail(string id)
        {
            Model.Notice notice = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_NOTICE_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    notice = new Model.Notice()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        Url = dr["URL"].ToString(),
                        AttImage = dr["ATT_IMAGE"].ToString(),
                        AttFile = dr["ATT_FILE"].ToString(),
                        AttFileName = dr["ATT_FILENAME"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                        Admin = new Model.Admin()
                        {
                            Name = dr["ADMIN_NAME"].ToString()
                        }
                    };
                }
            }

            return notice;
        }

        /// <summary>
        /// 공지사항 등록
        /// </summary>
        public bool Regist(Model.Notice notice)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", notice.Id));
            parameters.Add(new SqlParameter("@FK_ADMIN", notice.FkAdmin));
            parameters.Add(new SqlParameter("@TYPE", notice.Type));
            parameters.Add(new SqlParameter("@TITLE", notice.Title));
            parameters.Add(new SqlParameter("@CONTENTS", notice.Contents));
            parameters.Add(new SqlParameter("@URL", notice.Url));
            parameters.Add(new SqlParameter("@ATT_IMAGE", notice.AttImage));
            parameters.Add(new SqlParameter("@ATT_FILE", notice.AttFile));
            parameters.Add(new SqlParameter("@ATT_FILENAME", notice.AttFileName));
            parameters.Add(new SqlParameter("@USE_YN", notice.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_NOTICE_REGIST", parameters);
        }

        /// <summary>
        /// 공지사항 수정
        /// </summary>
        public bool Modify(Model.Notice notice)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", notice.Id));
            parameters.Add(new SqlParameter("@TYPE", notice.Type));
            parameters.Add(new SqlParameter("@TITLE", notice.Title));
            parameters.Add(new SqlParameter("@CONTENTS", notice.Contents));
            parameters.Add(new SqlParameter("@URL", notice.Url));
            parameters.Add(new SqlParameter("@ATT_IMAGE", notice.AttImage));
            parameters.Add(new SqlParameter("@ATT_FILE", notice.AttFile));
            parameters.Add(new SqlParameter("@ATT_FILENAME", notice.AttFileName));
            parameters.Add(new SqlParameter("@USE_YN", notice.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_NOTICE_MODIFY", parameters);
        }

        /// <summary>
        /// 공지사항 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_NOTICE_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 공지사항 리스트
        /// </summary>
        public List<Model.Notice> UserList(int page, int size, string type)
        {
            List<Model.Notice> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Notice>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Notice notice = new Model.Notice()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Title = dr["TITLE"].ToString(),
                            Contents = dr["CONTENTS"].ToString(),
                            Url = dr["URL"].ToString(),
                            AttImage = dr["ATT_IMAGE"].ToString(),
                            AttFile = dr["ATT_FILE"].ToString(),
                            ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
                            UseYn = dr["USE_YN"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Name = dr["ADMIN_NAME"].ToString()
                            }
                        };

                        lists.Add(notice);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 공지사항 조회
        /// </summary>
        public Model.Notice UserDetail(string id)
        {
            Model.Notice notice = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    notice = new Model.Notice()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        Url = dr["URL"].ToString(),
                        AttImage = dr["ATT_IMAGE"].ToString(),
                        AttFile = dr["ATT_FILE"].ToString(),
                        AttFileName = dr["ATT_FILENAME"].ToString(),
                        ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                        ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

            return notice;
        }

        /// <summary>
        /// 공지사항 이전글
        /// </summary>
        public Model.Notice UserBefore(string id, string type)
        {
            Model.Notice notice = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_BEFORE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    notice = new Model.Notice()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString()
                    };
                }
            }

            return notice;
        }

        /// <summary>
        /// 공지사항 다음글
        /// </summary>
        public Model.Notice UserNext(string id, string type)
        {
            Model.Notice notice = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_NEXT", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    notice = new Model.Notice()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString()
                    };
                }
            }

            return notice;
        }
        #endregion
    }
}