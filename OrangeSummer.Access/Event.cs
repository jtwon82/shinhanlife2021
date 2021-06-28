using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Text;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 이벤트 Access
    /// </summary>
    public class Event
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 이벤트 생성자
        /// </summary>
        public Event(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 이벤트 리스트
        /// </summary>
        public List<Model.Event> List(int page, int size, string type, string title, string use, string sdate, string edate)
        {
            List<Model.Event> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@KEY_TYPE", type));
            parameters.Add(new SqlParameter("@KEY_TITLE", title));
            parameters.Add(new SqlParameter("@KEY_USE", use));
            parameters.Add(new SqlParameter("@KEY_SDATE", sdate));
            parameters.Add(new SqlParameter("@KEY_EDATE", edate));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_EVENT_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Event>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Event evt = new Model.Event()
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
                            ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

                        lists.Add(evt);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 이벤트 메인 리스트
        /// </summary>
        public List<Model.Event> Main()
        {
            List<Model.Event> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_EVENT_MAIN"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Event>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Event evt = new Model.Event()
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
                            ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

                        lists.Add(evt);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 이벤트 조회
        /// </summary>
        public Model.Event Detail(string id)
        {
            Model.Event evt = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_EVENT_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    evt = new Model.Event()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        Url = dr["URL"].ToString(),
                        AttImage = dr["ATT_IMAGE"].ToString(),
                        ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                        ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

            return evt;
        }

        /// <summary>
        /// 이벤트 등록
        /// </summary>
        public bool Regist(Model.Event m)
        {
            //List<SqlParameter> parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter("@ID", evt.Id));
            //parameters.Add(new SqlParameter("@FK_ADMIN", evt.FkAdmin));
            //parameters.Add(new SqlParameter("@TYPE", evt.Type));
            //parameters.Add(new SqlParameter("@TITLE", evt.Title));
            //parameters.Add(new SqlParameter("@CONTENTS", evt.Contents));
            //parameters.Add(new SqlParameter("@URL", evt.Url));
            //parameters.Add(new SqlParameter("@ATT_IMAGE", evt.AttImage));
            //parameters.Add(new SqlParameter("@VIEW_COUNT", evt.ViewCount));
            //parameters.Add(new SqlParameter("@REPLY_COUNT", evt.ReplyCount));
            //parameters.Add(new SqlParameter("@SDATE", evt.Sdate));
            //parameters.Add(new SqlParameter("@EDATE", evt.Edate));
            //parameters.Add(new SqlParameter("@USE_YN", evt.UseYn));

            //return DBHelper.ExecuteNonQuery(_connection, "ADM_EVENT_REGIST", parameters);

            //evt.Id = Tool.UniqueNewGuid;
            //evt.FkAdmin = Common.Master.Identify.Id;
            //evt.Type = Element.Get(this.type);
            //evt.Title = Element.Get(this.title);
            //evt.AttImage = mobile;
            //evt.Url = "";
            //evt.Contents = Element.Get(this.contents);
            //evt.Sdate = Element.Get(this.edate);
            //evt.Edate = Element.Get(this.sdate);
            //evt.UseYn = Element.Get(this.useyn);

            StringBuilder sb = new StringBuilder();
            sb.Append($"INSERT INTO [dbo].[EVENT](ID, FK_ADMIN, TYPE, TITLE, CONTENTS, URL, ATT_IMAGE, VIEW_COUNT, REPLY_COUNT, SDATE, EDATE, USE_YN, DEL_YN, REGIST_DATE) ");
            sb.Append($" VALUES ('{m.Id}', '{m.FkAdmin}', '{m.Type}', '{m.Title}', '{m.Contents}', '{m.Url}', '{m.AttImage}', 0, 0, '{m.Sdate}', '{m.Edate}', '{m.UseYn}', 'N', GETDATE())");
            //sb.Append($" VALUES ('{m.Id}', null, '{m.Gubun}', '{m.UseYn}', '{m.Title}', '{m.attMobile}', '{m.Contents}', '{m.sdate}', '{m.edate}', 0, GETDATE())");

            return DBHelper.ExecuteNonInQuery(_connection, sb.ToString());
        }

        /// <summary>
        /// 이벤트 수정
        /// </summary>
        public bool Modify(Model.Event evt)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", evt.Id));
            parameters.Add(new SqlParameter("@TYPE", evt.Type));
            parameters.Add(new SqlParameter("@TITLE", evt.Title));
            parameters.Add(new SqlParameter("@CONTENTS", evt.Contents));
            parameters.Add(new SqlParameter("@URL", evt.Url));
            parameters.Add(new SqlParameter("@ATT_IMAGE", evt.AttImage));
            parameters.Add(new SqlParameter("@SDATE", evt.Sdate));
            parameters.Add(new SqlParameter("@EDATE", evt.Edate));
            parameters.Add(new SqlParameter("@USE_YN", evt.UseYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_EVENT_MODIFY", parameters);
        }

        /// <summary>
        /// 이벤트 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_EVENT_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 이벤트 리스트
        /// </summary>
        public List<Model.Event> UserList(int page, int size, string type)
        {
            List<Model.Event> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Event>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Event evt = new Model.Event()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                            Type = dr["TYPE"].ToString(),
                            Title = dr["TITLE"].ToString(),
                            Contents = dr["CONTENTS"].ToString(),
                            Url = dr["URL"].ToString(),
                            AttImage = "/upload/" + dr["ATT_IMAGE"].ToString(),
                            ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

                        lists.Add(evt);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 이벤트 조회
        /// </summary>
        public Model.Event UserDetail(string id)
        {
            Model.Event evt = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    evt = new Model.Event()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkAdmin = dr["FK_ADMIN"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        Url = dr["URL"].ToString(),
                        AttImage = dr["ATT_IMAGE"].ToString(),
                        ViewCount = Convert.ToInt32(dr["VIEW_COUNT"].ToString()),
                        ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
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

            return evt;
        }

        /// <summary>
        /// 이벤트 이전글
        /// </summary>
        public Model.Event UserBefore(string id, string type)
        {
            Model.Event evt = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_BEFORE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    evt = new Model.Event()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Url = dr["URL"].ToString(),
                        Title = dr["TITLE"].ToString()
                    };
                }
            }

            return evt;
        }

        /// <summary>
        /// 이벤트 다음글
        /// </summary>
        public Model.Event UserNext(string id, string type)
        {
            Model.Event evt = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TYPE", type));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_NEXT", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    evt = new Model.Event()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Type = dr["TYPE"].ToString(),
                        Url = dr["URL"].ToString(),
                        Title = dr["TITLE"].ToString()
                    };
                }
            }

            return evt;
        }
        #endregion
    }
}
