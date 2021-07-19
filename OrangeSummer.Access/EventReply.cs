using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 이벤트 댓글 Access
    /// </summary>
    public class EventReply
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 이벤트 댓글 생성자
        /// </summary>
        public EventReply(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> Excel(string fkevent)
        {
            List<Model.EventReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FK_EVENT", fkevent));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_EVENT_REPLY_EXCEL", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.EventReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.EventReply evtReply = new Model.EventReply()
                        {
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkEvent = dr["FK_EVENT"].ToString().ToUpper(),
                            FkMember = dr["FK_MEMBER"].ToString().ToUpper(),
                            DepthGid = Convert.ToInt32(dr["DEPTH_GID"].ToString()),
                            DepthSeq = Convert.ToInt32(dr["DEPTH_SEQ"].ToString()),
                            Depth = Convert.ToInt32(dr["DEPTH"].ToString()),
                            Contents = dr["CONTENTS"].ToString(),
                            LikeCount = Convert.ToInt32(dr["LIKE_COUNT"].ToString()),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Member = new Model.Member()
                            {
                                Name = dr["MEMBER_NAME"].ToString(),
                                Code= dr["CODE"].ToString()
                            },
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(evtReply);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> List(int page, int size, string id)
        {
            List<Model.EventReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@FK_EVENT", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_EVENT_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.EventReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.EventReply reply = new Model.EventReply()
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
                                Code = dr["CODE"].ToString(),
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
        /// 이벤트 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_EVENT_REPLY_DELETE", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> UserList(int page, int size, string id, string member)
        {
            List<Model.EventReply> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@FK_EVENT", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_REPLY_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.EventReply>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.EventReply reply = new Model.EventReply()
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
        /// 이벤트 댓글 좋아요
        /// </summary>
        public Model.EventReplyLike UserLike(string id, string member)
        {
            Model.EventReplyLike like = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_EVENT_REPLY_LIKE", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    like = new Model.EventReplyLike()
                    {
                        Result = dr["RESULT"].ToString(),
                        LikeCount = Convert.ToInt32(dr["COUNT"].ToString()),
                    };
                }
            }

            return like;
        }

        /// <summary>
        /// 이벤트 댓글 등록
        /// </summary>
        public bool UserRegist(Model.EventReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_EVENT", reply.FkEvent));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_EVENT_REPLY_REGIST", parameters);
        }

        /// <summary>
        /// 이벤트 댓글 수정
        /// </summary>
        public bool UserModify(Model.EventReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_EVENT_REPLY_MODIFY", parameters);
        }

        /// <summary>
        /// 이벤트 댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_MEMBER", member));

            return DBHelper.ExecuteNonQuery(_connection, "USP_EVENT_REPLY_DELETE", parameters);
        }

        /// <summary>
        /// 이벤트 댓글 답글
        /// </summary>
        public bool UserAnswer(Model.EventReply reply)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", reply.Id));
            parameters.Add(new SqlParameter("@FK_MEMBER", reply.FkMember));
            parameters.Add(new SqlParameter("@CONTENTS", reply.Contents));

            return DBHelper.ExecuteNonQuery(_connection, "USP_EVENT_REPLY_ANSWER", parameters);
        }
        #endregion
    }
}
