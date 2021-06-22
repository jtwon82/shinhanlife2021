using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// Wrod이벤트 Business
    /// </summary>
    public class Word : IDisposable
    {
        private Access.Word _word = null;

        /// <summary>
        /// Wrod이벤트 생성자
        /// </summary>
        public Word(string connection)
        {
            _word = new Access.Word(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// Wrod이벤트 리스트
        /// </summary>
        public List<Model.Word> List(int page, int size)
        {
            return _word.List(page, size);
        }

        /// <summary>
        /// Wrod이벤트 리스트
        /// </summary>
        public List<Model.Word> Excel()
        {
            return _word.Excel();
        }

        /// <summary>
        /// Wrod이벤트 랭킹
        /// </summary>
        public List<Model.Word> Ranking()
        {
            return _word.Ranking();
        }
        #endregion

        #region [ 사용자 ]
        public string UserVote(string member, string vote)
        {
            return _word.UserVote(member, vote);
        }
        #endregion

        /// <summary>
        /// Wrod이벤트 소멸자
        /// </summary>
        public void Dispose()
        {
            _word = null;
        }
    }
}
