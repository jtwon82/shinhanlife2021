using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 롤렛이벤트 Business
    /// </summary>
    public class Roulette : IDisposable
    {
        private Access.Roulette _roulette = null;

        /// <summary>
        /// 롤렛이벤트 생성자
        /// </summary>
        public Roulette(string connection)
        {
            _roulette = new Access.Roulette(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 롤렛이벤트 리스트
        /// </summary>
        public List<Model.Roulette> List(int page, int size)
        {
            return _roulette.List(page, size);
        }

        /// <summary>
        /// 롤렛이벤트 리스트
        /// </summary>
        public List<Model.Roulette> Excel()
        {
            return _roulette.Excel();
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 롤렛 담첨자 카운트
        /// </summary>
        public bool UserSuccess()
        {
            return _roulette.UserSuccess();
        }

        /// <summary>
        /// 룰렛 사용자 참여 조회
        /// </summary>
        public bool UserCheck(string member)
        {
            return _roulette.UserCheck(member);
        }

        /// <summary>
        /// 룰렛 등록
        /// </summary>
        public bool UserRegist(Model.Roulette roulette)
        {
            return _roulette.UserRegist(roulette);
        }
        #endregion

        /// <summary>
        /// 롤렛이벤트 소멸자
        /// </summary>
        public void Dispose()
        {
            _roulette = null;
        }
    }
}
