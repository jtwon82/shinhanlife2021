using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// UCC이벤트 Business
    /// </summary>
    public class Ucc : IDisposable
    {
        private Access.Ucc _ucc = null;

        /// <summary>
        /// UCC이벤트 생성자
        /// </summary>
        public Ucc(string connection)
        {
            _ucc = new Access.Ucc(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// UCC이벤트 리스트
        /// </summary>
        public List<Model.Ucc> List(int page, int size)
        {
            return _ucc.List(page, size);
        }

        /// <summary>
        /// UCC이벤트 리스트
        /// </summary>
        public List<Model.Ucc> Excel()
        {
            return _ucc.Excel();
        }

        /// <summary>
        /// UCC이벤트 랭킹
        /// </summary>
        public List<Model.Ucc> Ranking()
        {
            return _ucc.Ranking();
        }
        #endregion

        #region [ 사용자 ]
        public string UserVote(string member, string vote)
        {
            return _ucc.UserVote(member, vote);
        }
        #endregion

        /// <summary>
        /// UCC이벤트 소멸자
        /// </summary>
        public void Dispose()
        {
            _ucc = null;
        }
    }
}
