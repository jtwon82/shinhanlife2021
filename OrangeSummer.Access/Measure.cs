using MLib.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeSummer.Access
{
    public class Measure
    {
        private string _connection = string.Empty;

        /// <summary>
        ///  생성자
        /// </summary>
        public Measure(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        ///  리스트
        /// </summary>
        public List<Model.Measure> List(int page, int size, string gubun, string title, string useYn, string sdate, string edate)
        {
            List<Model.Measure> lists = null;

            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM ( ");
            query.Append($"SELECT COUNT(*) OVER() AS [TOTAL], ROW_NUMBER() OVER(ORDER BY SORT ASC) AS [ROW], ");
            query.Append($" A.* ");
            query.Append($"FROM [MEASURE] A ");
            query.Append($"WHERE 1=1 ");
            if (gubun != "") query.Append($"AND GUBUN = '{gubun}' ");
            if (title != "") query.Append($"AND TITLE LIKE '%{title}%' ");
            if (useYn != "") query.Append($"AND USE_YN = '{useYn}' ");
            if (sdate != "") query.Append($"AND SDATE >= '{sdate}' ");
            if (edate != "") query.Append($"AND EDATE <= '{edate}' ");
            query.Append($") RESULT ");
            query.Append($"WHERE [ROW] BETWEEN(({page} - 1) * {size}) + 1 AND {page} * {size} ");
            
            using (DataTable dt = DBHelper.ExecuteDataTableInQuery(_connection, query.ToString()))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Measure>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Measure member = new Model.Measure()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Gubun = dr["GUBUN"].ToString(),
                            UseYn = dr["USE_YN"].ToString(),
                            Title = dr["TITLE"].ToString(),
                            attMobile = dr["ATT_MOBILE"].ToString(),
                            Contents = dr["CONTENTS"].ToString(),
                            sdate = dr["SDATE"].ToString(),
                            edate = dr["EDATE"].ToString(),
                            HitCnt = dr["HIT_CNT"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Admin = new Model.Admin()
                            {
                                Id=dr["FK_ADMIN"].ToString()
                            },
                            empty = ""
                        };

                        lists.Add(member);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        ///  엑셀
        /// </summary>
        //public List<Model.Measure> Excel(string branch, string level, string code, string mobile, string sdate, string edate)
        //{
        //    List<Model.Measure> lists = null;
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("@KEY_BRANCH", branch));
        //    parameters.Add(new SqlParameter("@KEY_LEVEL", level));
        //    parameters.Add(new SqlParameter("@KEY_CODE", code));
        //    parameters.Add(new SqlParameter("@KEY_MOBILE", mobile));
        //    parameters.Add(new SqlParameter("@KEY_SDATE", sdate));
        //    parameters.Add(new SqlParameter("@KEY_EDATE", edate));
        //    using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_MEMBER_EXCEL", parameters))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            lists = new List<Model.Measure>();
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                Model.Measure member = new Model.Measure()
        //                {
        //                    Total = Convert.ToInt32(dr["TOTAL"].ToString()),
        //                    Id = dr["ID"].ToString().ToUpper(),
        //                    Sort = Convert.ToInt32(dr["SORT"].ToString()),
        //                    FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
        //                    FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
        //                    Code = dr["CODE"].ToString(),
        //                    Pwd = dr["PWD"].ToString(),
        //                    Reset = dr["RESET"].ToString(),
        //                    Level = dr["LEVEL"].ToString(),
        //                    Name = dr["NAME"].ToString(),
        //                    Mobile = dr["MOBILE"].ToString(),
        //                    DelYn = dr["DEL_YN"].ToString(),
        //                    RegistDate = dr["REGIST_DATE"].ToString(),
        //                    Branch = new Model.Branch()
        //                    {
        //                        Name = dr["BRANCH_NAME"].ToString()
        //                    }
        //                };

        //                lists.Add(member);
        //            }
        //        }
        //    }

        //    return lists;
        //}

        /// <summary>
        ///  조회
        /// </summary>
        public Model.Measure Detail(string id)
        {
            Model.Measure member = null;

            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM [MEASURE] ");
            query.Append($"WHERE ID='{id}' ");

            using (DataTable dt = DBHelper.ExecuteDataTableInQuery(_connection, query.ToString()))// DBHelper.ExecuteDataTable(_connection, "ADM_MEMBER_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    member = new Model.Measure()
                    {
                        //Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                        Id = dr["ID"].ToString().ToUpper(),
                        Gubun = dr["GUBUN"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        attMobile = dr["ATT_MOBILE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        sdate = dr["SDATE"].ToString(),
                        edate = dr["EDATE"].ToString(),
                        HitCnt = dr["HIT_CNT"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Admin = new Model.Admin()
                        {
                            Id = dr["FK_ADMIN"].ToString()
                        },

                        empty = ""
                    };
                }
            }

            return member;
        }

        /// <summary>
        ///  삭제
        /// </summary>
        public bool Delete(string id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM [MEASURE] WHERE ID='{id}';");

            return DBHelper.ExecuteNonInQuery(_connection, sb.ToString());
        }

        /// <summary>
        ///  비밀번호 재설정
        /// </summary>
        public bool Reset(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_MEMBER_RESET", parameters);
        }

        /// <summary>
        ///  수정
        /// </summary>
        public bool Modify(Model.Measure m)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE A ");
            sb.Append($"SET UPDATE_DATE = GETDATE() ");
            sb.Append($", TITLE = '{m.Title}' ");
            sb.Append($", GUBUN = '{m.Gubun}' ");
            sb.Append($", ATT_MOBILE = '{m.attMobile}' ");
            sb.Append($", CONTENTS = '{m.Contents}' ");
            sb.Append($", SDATE = '{m.sdate}' ");
            sb.Append($", EDATE = '{m.edate}' ");
            sb.Append($", USE_YN = '{m.UseYn}' ");
            sb.Append($"FROM [MEASURE] A WHERE ID='{m.Id}';");

            return DBHelper.ExecuteNonInQuery(_connection, sb.ToString());
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 상세 조회
        /// </summary>
        public Model.Measure UserDetail(string id)
        {
            Model.Measure member = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    member = new Model.Measure()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Gubun = dr["GUBUN"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        attMobile = dr["ATT_MOBILE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        sdate = dr["SDATE"].ToString(),
                        edate = dr["EDATE"].ToString(),
                        HitCnt = dr["HIT_CNT"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Admin = new Model.Admin()
                        {
                            Id = dr["FK_ADMIN"].ToString()
                        },

                        empty = ""
                    };
                }
            }

            return member;
        }

        /// <summary>
        ///  전화번호 중복체크
        /// </summary>
        public string UserCheckPno(string pno)
        {
            string result = "FAIL";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MOBILE", pno));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_CHECK_PNO", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    result = dr["RESULT"].ToString();
                }
            }

            return result;
        }

        /// <summary>
        ///  코드 중복체크
        /// </summary>
        public string UserCheck(string code, string name)
        {
            string result = "FAIL";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NAME", name));
            parameters.Add(new SqlParameter("@CODE", code));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_CHECKV2", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    result = dr["RESULT"].ToString();
                }
            }

            return result;
        }

        /// <summary>
        ///  로그인
        /// </summary>
        public Model.Measure UserLogin(string code, string pwd)
        {
            Model.Measure member = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CODE", code));
            parameters.Add(new SqlParameter("@PWD", pwd));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_LOGIN", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];

                    member = new Model.Measure()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Gubun = dr["GUBUN"].ToString(),
                        UseYn = dr["USE_YN"].ToString(),
                        Title = dr["TITLE"].ToString(),
                        attMobile = dr["ATT_MOBILE"].ToString(),
                        Contents = dr["CONTENTS"].ToString(),
                        sdate = dr["SDATE"].ToString(),
                        edate = dr["EDATE"].ToString(),
                        HitCnt = dr["HIT_CNT"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Admin = new Model.Admin()
                        {
                            Id = dr["FK_ADMIN"].ToString()
                        },

                        empty = ""
                    };
                }
            }

            return member;
        }

        /// <summary>
        ///  등록
        /// </summary>
        public bool Regist(Model.Measure m)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"INSERT INTO [MEASURE](ID, FK_ADMIN, GUBUN, USE_YN, TITLE, ATT_MOBILE, CONTENTS, SDATE, EDATE, HIT_CNT, REGIST_DATE) ");
            sb.Append($" VALUES ('{m.Id}', null, '{m.Gubun}', '{m.UseYn}', '{m.Title}', '{m.attMobile}', '{m.Contents}', '{m.sdate}', '{m.edate}', 0, GETDATE())");
            
            return DBHelper.ExecuteNonInQuery(_connection, sb.ToString()) ;
        }

        /// <summary>
        ///  수정
        /// </summary>
        public bool UserModify(Model.Measure member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", member.Id));
            return DBHelper.ExecuteNonQuery(_connection, "USP_MEMBER_MODIFY", parameters);
        }

        /// <summary>
        ///  여행지 수정
        /// </summary>
        public bool UserTravel(string id, string travel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_TRAVEL", travel));

            return DBHelper.ExecuteNonQuery(_connection, "USP_MEMBER_TRAVEL", parameters);
        }
        #endregion
    }
}
