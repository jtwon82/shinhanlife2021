using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 약관내용 Business
    /// </summary>
    public class Agreement : IDisposable
    {
        private Access.Agreement _agreement = null;

        /// <summary>
        /// 약관내용 생성자
        /// </summary>
        public Agreement(string connection)
        {
            _agreement = new Access.Agreement(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 약관내용 조회
        /// </summary>
        public Model.Agreement Detail()
        {
            return _agreement.Detail();
        }

        /// <summary>
        /// 약관내용 수정
        /// </summary>
        public bool Modify(Model.Agreement agreement)
        {
            return _agreement.Modify(agreement);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 약관내용 조회
        /// </summary>
        public Model.Agreement UserDetail()
        {
            return _agreement.UserDetail();
        }
        #endregion

        /// <summary>
        /// 약관내용 소멸자
        /// </summary>
        public void Dispose()
        {
            _agreement = null;
        }
    }
}
