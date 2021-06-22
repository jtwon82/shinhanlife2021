using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.17
    /// 여행지 Business
    /// </summary>
    public class Travel : IDisposable
    {
        private Access.Travel _travel = null;

        /// <summary>
        /// 여행지 생성자
        /// </summary>
        public Travel(string connection)
        {
            _travel = new Access.Travel(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 여행지 리스트
        /// </summary>
        public List<Model.Travel> List(int page, int size)
        {
            return _travel.List(page, size);
        }

        /// <summary>
        /// 여행지 리스트(일렬용)
        /// </summary>
        public List<Model.Travel> Line()
        {
            return _travel.Line();
        }

        /// <summary>
        /// 여행지 등록
        /// </summary>
        public bool Regist(Model.Travel travel)
        {
            return _travel.Regist(travel);
        }

        /// <summary>
        /// 여행지 수정
        /// </summary>
        public bool Modify(Model.Travel travel)
        {
            return _travel.Modify(travel);
        }

        /// <summary>
        /// 여행지 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _travel.Delete(id);
        }

        /// <summary>
        /// 여행지 조회
        /// </summary>
        public Model.Travel Detail(string id)
        {
            return _travel.Detail(id);
        }

        /// <summary>
        /// 여행지 구분 조회
        /// </summary>
        public bool Check(int section)
        {
            return _travel.Check(section);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 여행지 리스트(일렬용)
        /// </summary>
        public List<Model.Travel> UserLine()
        {
            return _travel.UserLine();
        }
        #endregion

        /// <summary>
        /// 여행지 소멸자
        /// </summary>
        public void Dispose()
        {
            _travel = null;
        }
    }
}
