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
    /// UCC이벤트 댓글 Access
    /// </summary>
    public class NoticeReply
    {
        private string _connection = string.Empty;

        /// <summary>
        /// UCC이벤트 댓글 생성자
        /// </summary>
        public NoticeReply(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 공지사항 댓글 리스트
        /// </summary>
        public List<Model.NoticeReply> List(int page, int size, string id)
        {
            List<Model.NoticeReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@FK_NOTICE", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_NOTICE_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.NoticeReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.NoticeReply reply = new Model.NoticeReply()
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
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString(),
                                ProfileImg = dr["PROFILE_IMG"].ToString()
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
        /// 공지사항 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_NOTICE_REPLY_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 공지사항 댓글 리스트
        /// </summary>
        public List<Model.NoticeReply> UserList(int page, int size, string id, string member)
        {
            List<Model.NoticeReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@FK_NOTICE", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.NoticeReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.NoticeReply reply = new Model.NoticeReply()
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
                            ReplyCount = Convert.ToInt32(dr["REPLY_COUNT"].ToString()),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Like = Convert.ToInt32(dr["LIKE"].ToString()),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString(),
                                ProfileImg = dr["PROFILE_IMG"].ToString()
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
        /// 공지사항 댓글 좋아요
        /// </summary>
        public Model.NoticeReplyLike UserLike(string id, string member)
        {
            Model.NoticeReplyLike like = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_NOTICE_REPLY_LIKE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    like = new Model.NoticeReplyLike()
                    {
                        Result = dr["RESULT"].ToString(),
                        LikeCount = Convert.ToInt32(dr["COUNT"].ToString()),
                    };
                }
            }

            return like;
        }

        /// <summary>
        /// 공지사항 댓글 등록
        /// </summary>
        public bool UserRegist(Model.NoticeReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_NOTICE", reply.FkNotice));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_NOTICE_REPLY_REGIST", parameters);

            //INSERT INTO[NOTICE_REPLY] ([ID], [FK_NOTICE], [FK_MEMBER], [CONTENTS], [DEPTH_SEQ]) VALUES(@ID, @FK_NOTICE, @FK_MEMBER, @CONTENTS, 1)
            //UPDATE[NOTICE_REPLY] SET[DEPTH_GID] = (SELECT SORT FROM[NOTICE_REPLY] WHERE ID = @ID) WHERE ID = @ID
            //UPDATE[NOTICE] SET REPLY_COUNT = REPLY_COUNT + 1 WHERE[ID] = @FK_NOTICE

            //StringBuilder sql = new StringBuilder();
            //sql.Append($"INSERT INTO[NOTICE_REPLY] ([ID], [FK_NOTICE], [FK_MEMBER], [CONTENTS], [DEPTH_SEQ], [REGIST_DATE], [SORT], [DEPTH_GID])");
            //sql.Append($"VALUES('{reply.Id}', '{reply.FkNotice}', '{reply.FkMember}', '{reply.Contents}', 1, getdate(), 1, 1)");
            //DBHelper.ExecuteNonInQuery(_connection, sql.ToString());

            //StringBuilder sql2 = new StringBuilder();
            //sql2.Append($"UPDATE[NOTICE_REPLY] SET[DEPTH_GID] = (SELECT SORT FROM[NOTICE_REPLY] WHERE ID = '{reply.Id}') WHERE ID = '{reply.Id}'");
            //DBHelper.ExecuteNonInQuery(_connection, sql.ToString());

            //StringBuilder sql3 = new StringBuilder();
            //sql3.Append($"UPDATE[NOTICE] SET REPLY_COUNT = REPLY_COUNT + 1 WHERE[ID] = '{reply.FkNotice}'");
            //DBHelper.ExecuteNonInQuery(_connection, sql.ToString());

            //return true;
        }

        /// <summary>
        /// 공지사항 댓글 수정
        /// </summary>
        public bool UserModify(Model.NoticeReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_NOTICE_REPLY_MODIFY", parameters);
        }

        /// <summary>
        /// 공지사항 댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));

            return DBHelper.ExecuteNonQuery(_connection, "USP_NOTICE_REPLY_DELETE", parameters);
        }

        /// <summary>
        /// 공지사항 댓글 답글
        /// </summary>
        public bool UserAnswer(Model.NoticeReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_NOTICE_REPLY_ANSWER", parameters);
        }
        #endregion
    }
}
