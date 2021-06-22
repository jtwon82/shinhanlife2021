using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 이벤트 Business
    /// </summary>
    public class Event : IDisposable
    {
        private Access.Event _event = null;

        /// <summary>
        /// 이벤트 생성자
        /// </summary>
        public Event(string connection)
        {
            _event = new Access.Event(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 이벤트 리스트
        /// </summary>
        public List<Model.Event> List(int page, int size, string type, string title, string use, string sdate, string edate)
        {
            return _event.List(page, size, type, title, use, sdate, edate);
        }

        /// <summary>
        /// 이벤트 메인 리스트
        /// </summary>
        public List<Model.Event> Main()
        {
            return _event.Main();
        }

        /// <summary>
        /// 이벤트 조회
        /// </summary>
        public Model.Event Detail(string id)
        {
            return _event.Detail(id);
        }

        /// <summary>
        /// 이벤트 등록
        /// </summary>
        public bool Regist(Model.Event evt)
        {
            return _event.Regist(evt);
        }

        /// <summary>
        /// 이벤트 수정
        /// </summary>
        public bool Modify(Model.Event evt)
        {
            return _event.Modify(evt);
        }

        /// <summary>
        /// 이벤트 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _event.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 이벤트 리스트
        /// </summary>
        public List<Model.Event> UserList(int page, int size, string type)
        {
            return _event.UserList(page, size, type);
        }

        /// <summary>
        /// 이벤트 조회
        /// </summary>
        public Model.Event UserDetail(string id)
        {
            return _event.UserDetail(id);
        }

        /// <summary>
        /// 이벤트 이전글
        /// </summary>
        public Model.Event UserBefore(string id, string type)
        {
            return _event.UserBefore(id, type);
        }

        /// <summary>
        /// 이벤트 다음글
        /// </summary>
        public Model.Event UserNext(string id, string type)
        {
            return _event.UserNext(id, type);
        }
        #endregion

        /// <summary>
        /// 이벤트 소멸자
        /// </summary>
        public void Dispose()
        {
            _event = null;
        }
    }
}
