using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 공지사항 Business
    /// </summary>
    public class Notice : IDisposable
    {
        private Access.Notice _notice = null;

        /// <summary>
        /// 공지사항 생성자
        /// </summary>
        public Notice(string connection)
        {
            _notice = new Access.Notice(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 공지사항 리스트
        /// </summary>
        public List<Model.Notice> List(int page, int size, string type, string title, string use, string sdate, string edate)
        {
            return _notice.List(page, size, type, title, use, sdate, edate);
        }

        /// <summary>
        /// 공지사항 메인 리스트
        /// </summary>
        public List<Model.Notice> Main()
        {
            return _notice.Main();
        }

        /// <summary>
        /// 공지사항 조회
        /// </summary>
        public Model.Notice Detail(string id)
        {
            return _notice.Detail(id);
        }

        /// <summary>
        /// 공지사항 등록
        /// </summary>
        public bool Regist(Model.Notice notice)
        {
            return _notice.Regist(notice);
        }

        /// <summary>
        /// 공지사항 수정
        /// </summary>
        public bool Modify(Model.Notice notice)
        {
            return _notice.Modify(notice);
        }

        /// <summary>
        /// 공지사항 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _notice.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 공지사항 리스트
        /// </summary>
        public List<Model.Notice> UserList(int page, int size, string type)
        {
            return _notice.UserList(page, size, type);
        }

        /// <summary>
        /// 공지사항 조회
        /// </summary>
        public Model.Notice UserDetail(string id)
        {
            return _notice.UserDetail(id);
        }

        /// <summary>
        /// 공지사항 이전글
        /// </summary>
        public Model.Notice UserBefore(string id, string type)
        {
            return _notice.UserBefore(id, type);
        }

        /// <summary>
        /// 공지사항 다음글
        /// </summary>
        public Model.Notice UserNext(string id, string type)
        {
            return _notice.UserNext(id, type);
        }
        #endregion

        /// <summary>
        /// 공지사항 소멸자
        /// </summary>
        public void Dispose()
        {
            _notice = null;
        }
    }
}