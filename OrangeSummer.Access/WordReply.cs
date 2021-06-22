using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// Wrod이벤트 댓글 Access
    /// </summary>
    public class WordReply
    {
        private string _connection = string.Empty;

        /// <summary>
        /// Wrod이벤트 댓글 생성자
        /// </summary>
        public WordReply(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// Wrod이벤트 댓글 리스트
        /// </summary>
        public List<Model.WordReply> Excel()
        {
            List<Model.WordReply> lists = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_WORD_REPLY_EXCEL"))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.WordReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.WordReply reply = new Model.WordReply()
                        {
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            DepthGid = Convert.ToInt32(dr["DEPTH_GID"].ToString()),
                            DepthSeq = Convert.ToInt32(dr["DEPTH_SEQ"].ToString()),
                            Depth = Convert.ToInt32(dr["DEPTH"].ToString()),
                            Contents = dr["CONTENTS"].ToString(),
                            LikeCount = Convert.ToInt32(dr["LIKE_COUNT"].ToString()),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(reply);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// Wrod이벤트 댓글 리스트
        /// </summary>
        public List<Model.WordReply> List(int page, int size)
        {
            List<Model.WordReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_WORD_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.WordReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.WordReply reply = new Model.WordReply()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            DepthGid = Convert.ToInt32(dr["DEPTH_GID"].ToString()),
                            DepthSeq = Convert.ToInt32(dr["DEPTH_SEQ"].ToString()),
                            Depth = Convert.ToInt32(dr["DEPTH"].ToString()),
                            Contents = dr["CONTENTS"].ToString(),
                            LikeCount = Convert.ToInt32(dr["LIKE_COUNT"].ToString()),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(reply);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// Wrod이벤트 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_WORD_REPLY_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// UCC댓글 리스트
        /// </summary>
        public List<Model.WordReply> UserList(int page, int size, string member)
        {
            List<Model.WordReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_WORD_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.WordReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.WordReply reply = new Model.WordReply()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            DepthGid = Convert.ToInt32(dr["DEPTH_GID"].ToString()),
                            DepthSeq = Convert.ToInt32(dr["DEPTH_SEQ"].ToString()),
                            Depth = Convert.ToInt32(dr["DEPTH"].ToString()),
                            Contents = dr["CONTENTS"].ToString(),
                            LikeCount = Convert.ToInt32(dr["LIKE_COUNT"].ToString()),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Like = Convert.ToInt32(dr["LIKE"].ToString()),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(reply);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// UCC댓글 좋아요
        /// </summary>
        public Model.WordReplyLike UserLike(string id, string member)
        {
            Model.WordReplyLike like = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_WORD_REPLY_LIKE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    like = new Model.WordReplyLike()
                    {
                        Result = dr["RESULT"].ToString(),
                        LikeCount = Convert.ToInt32(dr["COUNT"].ToString()),
                    };
                }
            }

            return like;
        }

        /// <summary>
        /// UCC댓글 등록
        /// </summary>
        public bool UserRegist(Model.WordReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_WORD_REPLY_REGIST", parameters);
        }

        /// <summary>
        /// UCC댓글 수정
        /// </summary>
        public bool UserModify(Model.WordReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_WORD_REPLY_MODIFY", parameters);
        }

        /// <summary>
        /// UCC댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));

            return DBHelper.ExecuteNonQuery(_connection, "USP_WORD_REPLY_DELETE", parameters);
        }

        /// <summary>
        /// UCC댓글 답글
        /// </summary>
        public bool UserAnswer(Model.WordReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_WORD_REPLY_ANSWER", parameters);
        }
        #endregion
    }
}
