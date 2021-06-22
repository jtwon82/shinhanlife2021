using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// Wrod이벤트 댓글 Business
    /// </summary>
    public class WordReply : IDisposable
    {
        private Access.WordReply _reply = null;

        /// <summary>
        /// Wrod이벤트 댓글 생성자
        /// </summary>
        public WordReply(string connection)
        {
            _reply = new Access.WordReply(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// Wrod이벤트 댓글 리스트
        /// </summary>
        public List<Model.WordReply> Excel()
        {
            return _reply.Excel();
        }

        /// <summary>
        /// Wrod이벤트 댓글 리스트
        /// </summary>
        public List<Model.WordReply> List(int page, int size)
        {
            return _reply.List(page, size);
        }

        /// <summary>
        /// Wrod이벤트 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _reply.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// UCC댓글 리스트
        /// </summary>
        public List<Model.WordReply> UserList(int page, int size, string member)
        {
            return _reply.UserList(page, size, member);
        }

        /// <summary>
        /// UCC댓글 등록
        /// </summary>
        public bool UserRegist(Model.WordReply reply)
        {
            return _reply.UserRegist(reply);
        }

        /// <summary>
        /// UCC댓글 수정
        /// </summary>
        public bool UserModify(Model.WordReply reply)
        {
            return _reply.UserModify(reply);
        }

        /// <summary>
        /// UCC댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            return _reply.UserDelete(id, member);
        }

        /// <summary>
        /// UCC댓글 좋아요
        /// </summary>
        public Model.WordReplyLike UserLike(string id, string member)
        {
            return _reply.UserLike(id, member);
        }

        /// <summary>
        /// UCC댓글 답글
        /// </summary>
        public bool UserAnswer(Model.WordReply reply)
        {
            return _reply.UserAnswer(reply);
        }
        #endregion

        /// <summary>
        /// Wrod이벤트 댓글 소멸자
        /// </summary>
        public void Dispose()
        {
            _reply = null;
        }
    }
}
