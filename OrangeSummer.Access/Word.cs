using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using MLib.Util;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// Wrod이벤트 Access
    /// </summary>
    public class Word
    {
        private string _connection = string.Empty;

        /// <summary>
        /// Wrod이벤트 생성자
        /// </summary>
        public Word(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// Wrod이벤트 리스트
        /// </summary>
        public List<Model.Word> List(int page, int size)
        {
            List<Model.Word> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_WORD_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Word>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Word word = new Model.Word()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            Vote = dr["VOTE"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Level = dr["LEVEL"].ToString(),
                                Code = dr["CODE"].ToString(),
                                Name = dr["MEMBER_NAME"].ToString(),
                                Mobile = dr["MOBILE"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(word);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// Wrod이벤트 리스트
        /// </summary>
        public List<Model.Word> Excel()
        {
            List<Model.Word> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_WORD_EXCEL"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Word>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Word word = new Model.Word()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            Vote = dr["VOTE"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Level = dr["LEVEL"].ToString(),
                                Code = dr["CODE"].ToString(),
                                Name = dr["MEMBER_NAME"].ToString(),
                                Mobile = dr["MOBILE"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(word);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// Wrod이벤트 랭킹
        /// </summary>
        public List<Model.Word> Ranking()
        {
            List<Model.Word> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_WORD_RANKING"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Word>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Word word = new Model.Word()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Row = Convert.ToInt32(dr["ROW"].ToString().ToUpper()),
                            Vote = dr["VOTE"].ToString(),
                        };

                        lists.Add(word);
                    }
                }
            }

            return lists;
        }
        #endregion

        #region [ 사용자 ]
        public string UserVote(string member, string vote)
        {
            string result = string.Empty;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            parameters.Add(new SqlParameter("@VOTE", vote));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_WORD_VOTE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    result = dr["RESULT"].ToString();
                }
            }

            return result;
        }
        #endregion
    }
}
