using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 배너관리 Business
    /// </summary>
    public class Banner : IDisposable
    {
        private Access.Banner _banner = null;

        /// <summary>
        /// 배너관리 생성자
        /// </summary>
        public Banner(string connection)
        {
            _banner = new Access.Banner(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 배너관리 리스트
        /// </summary>
        public List<Model.Banner> List(int page, int size, string type)
        {
            return _banner.List(page, size, type);
        }

        /// <summary>
        /// 배너관리 조회
        /// </summary>
        public Model.Banner Detail(string id, string type)
        {
            return _banner.Detail(id, type);
        }

        /// <summary>
        /// 배너관리 등록
        /// </summary>
        public bool Regist(Model.Banner banner)
        {
            return _banner.Regist(banner);
        }

        /// <summary>
        /// 배너관리 수정
        /// </summary>
        public bool Modify(Model.Banner banner)
        {
            return _banner.Modify(banner);
        }

        /// <summary>
        /// 배너관리 삭제
        /// </summary>
        public bool Delete(string id, string type)
        {
            return _banner.Delete(id, type);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 배너 리스트
        /// </summary>
        public List<Model.Banner> UserList(string type)
        {
            return _banner.UserList(type);
        }
        #endregion

        /// <summary>
        /// 배너관리 소멸자
        /// </summary>
        public void Dispose()
        {
            _banner = null;
        }
    }
}
