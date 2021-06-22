using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 지점관리 Business
    /// </summary>
    public class Branch : IDisposable
    {
        private Access.Branch _branch = null;

        /// <summary>
        /// 지점관리 생성자
        /// </summary>
        public Branch(string connection)
        {
            _branch = new Access.Branch(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> List(int page, int size, string branch, string travel)
        {
            return _branch.List(page, size, branch, travel);
        }

        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> Line()
        {
            return _branch.Line();
        }

        /// <summary>
        /// 지점관리 조회
        /// </summary>
        public Model.Branch Detail(string id)
        {
            return _branch.Detail(id);
        }

        /// <summary>
        /// 지점관리 중복체크
        /// </summary>
        public Model.Branch Check(string name)
        {
            return _branch.Check(name);
        }

        /// <summary>
        /// 지점관리 등록
        /// </summary>
        public bool Regist(Model.Branch branch)
        {
            return _branch.Regist(branch);
        }

        /// <summary>
        /// 지점관리 수정
        /// </summary>
        public bool Modify(Model.Branch branch)
        {
            return _branch.Modify(branch);
        }

        /// <summary>
        /// 지점관리 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _branch.Delete(id);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 지점관리 리스트
        /// </summary>
        public List<Model.Branch> UserLine()
        {
            return _branch.UserLine();
        }
        #endregion

        /// <summary>
        /// 지점관리 소멸자
        /// </summary>
        public void Dispose()
        {
            _branch = null;
        }
    }
}