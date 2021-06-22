using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// UCC이벤트 댓글 Business
    /// </summary>
    public class NoticeReply : IDisposable
    {
        private Access.NoticeReply _noticeReply = null;

        /// <summary>
        /// UCC이벤트 댓글 생성자
        /// </summary>
        public NoticeReply(string connection)
        {
            _noticeReply = new Access.NoticeReply(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 공지사항 댓글 리스트
        /// </summary>
        public List<Model.NoticeReply> List(int page, int size, string id)
        {
            return _noticeReply.List(page, size, id);
        }

        /// <summary>
        /// 공지사항 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _noticeReply.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 공지사항 댓글 리스트
        /// </summary>
        public List<Model.NoticeReply> UserList(int page, int size, string id, string member)
        {
            return _noticeReply.UserList(page, size, id, member);
        }

        /// <summary>
        /// 공지사항 댓글 등록
        /// </summary>
        public bool UserRegist(Model.NoticeReply reply)
        {
            return _noticeReply.UserRegist(reply);
        }

        /// <summary>
        /// 공지사항 댓글 수정
        /// </summary>
        public bool UserModify(Model.NoticeReply reply)
        {
            return _noticeReply.UserModify(reply);
        }

        /// <summary>
        /// 공지사항 댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            return _noticeReply.UserDelete(id, member);
        }

        /// <summary>
        /// 공지사항 댓글 좋아요
        /// </summary>
        public Model.NoticeReplyLike UserLike(string id, string member)
        {
            return _noticeReply.UserLike(id, member);
        }

        /// <summary>
        /// 공지사항 댓글 답글
        /// </summary>
        public bool UserAnswer(Model.NoticeReply reply)
        {
            return _noticeReply.UserAnswer(reply);
        }
        #endregion

        /// <summary>
        /// UCC이벤트 댓글 소멸자
        /// </summary>
        public void Dispose()
        {
            _noticeReply = null;
        }
    }
}
