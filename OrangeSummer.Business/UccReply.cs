using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// UCC이벤트 댓글 Business
    /// </summary>
    public class UccReply : IDisposable
    {
        private Access.UccReply _uccReply = null;

        /// <summary>
        /// UCC이벤트 댓글 생성자
        /// </summary>
        public UccReply(string connection)
        {
            _uccReply = new Access.UccReply(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// UCC이벤트 댓글 리스트
        /// </summary>
        public List<Model.UccReply> Excel()
        {
            return _uccReply.Excel();
        }

        /// <summary>
        /// UCC이벤트 댓글 리스트
        /// </summary>
        public List<Model.UccReply> List(int page, int size)
        {
            return _uccReply.List(page, size);
        }

        /// <summary>
        /// UCC이벤트 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _uccReply.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// UCC댓글 리스트
        /// </summary>
        public List<Model.UccReply> UserList(int page, int size, string member)
        {
            return _uccReply.UserList(page, size, member);
        }

        /// <summary>
        /// UCC댓글 등록
        /// </summary>
        public bool UserRegist(Model.UccReply reply)
        {
            return _uccReply.UserRegist(reply);
        }

        /// <summary>
        /// UCC댓글 수정
        /// </summary>
        public bool UserModify(Model.UccReply reply)
        {
            return _uccReply.UserModify(reply);
        }

        /// <summary>
        /// UCC댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            return _uccReply.UserDelete(id, member);
        }

        /// <summary>
        /// UCC댓글 좋아요
        /// </summary>
        public Model.UccReplyLike UserLike(string id, string member)
        {
            return _uccReply.UserLike(id, member);
        }

        /// <summary>
        /// UCC댓글 답글
        /// </summary>
        public bool UserAnswer(Model.UccReply reply)
        {
            return _uccReply.UserAnswer(reply);
        }
        #endregion

        /// <summary>
        /// UCC이벤트 댓글 소멸자
        /// </summary>
        public void Dispose()
        {
            _uccReply = null;
        }
    }
}
