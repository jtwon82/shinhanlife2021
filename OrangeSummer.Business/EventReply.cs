using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 이벤트 댓글 Business
    /// </summary>
    public class EventReply : IDisposable
    {
        private Access.EventReply _evtReply = null;

        /// <summary>
        /// 이벤트 댓글 생성자
        /// </summary>
        public EventReply(string connection)
        {
            _evtReply = new Access.EventReply(connection);
        }

        #region [ 관리자]
        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> Excel(string fkevent)
        {
            return _evtReply.Excel(fkevent);
        }

        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> List(int page, int size, string id)
        {
            return _evtReply.List(page, size, id);
        }

        /// <summary>
        /// 이벤트 댓글 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _evtReply.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 이벤트 댓글 리스트
        /// </summary>
        public List<Model.EventReply> UserList(int page, int size, string id, string member)
        {
            return _evtReply.UserList(page, size, id, member);
        }

        /// <summary>
        /// 이벤트 댓글 등록
        /// </summary>
        public bool UserRegist(Model.EventReply reply)
        {
            return _evtReply.UserRegist(reply);
        }

        /// <summary>
        /// 이벤트 댓글 수정
        /// </summary>
        public bool UserModify(Model.EventReply reply)
        {
            return _evtReply.UserModify(reply);
        }

        /// <summary>
        /// 이벤트 댓글 삭제
        /// </summary>
        public bool UserDelete(string id, string member)
        {
            return _evtReply.UserDelete(id, member);
        }

        /// <summary>
        /// 이벤트 댓글 좋아요
        /// </summary>
        public Model.EventReplyLike UserLike(string id, string member)
        {
            return _evtReply.UserLike(id, member);
        }

        /// <summary>
        /// 이벤트 댓글 답글
        /// </summary>
        public bool UserAnswer(Model.EventReply reply)
        {
            return _evtReply.UserAnswer(reply);
        }
        #endregion

        /// <summary>
        /// 이벤트 댓글 소멸자
        /// </summary>
        public void Dispose()
        {
            _evtReply = null;
        }
    }
}
