using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 회원관리 Business
    /// </summary>
    public class Member : IDisposable
    {
        private Access.Member _member = null;

        /// <summary>
        /// 회원관리 생성자
        /// </summary>
        public Member(string connection)
        {
            _member = new Access.Member(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 회원관리 리스트
        /// </summary>
        public List<Model.Member> List(int page, int size, string branch, string level, string code, string mobile, string sdate, string edate, string name)
        {
            return _member.List(page, size, branch, level, code, mobile, sdate, edate, name );
        }

        /// <summary>
        /// 회원관리 엑셀
        /// </summary>
        public List<Model.Member> Excel(string branch, string level, string code, string mobile, string sdate, string edate, string name)
        {
            return _member.Excel(branch, level, code, mobile, sdate, edate, name);
        }

        /// <summary>
        /// 회원관리 조회
        /// </summary>
        public Model.Member Detail(string id)
        {
            return _member.Detail(id);
        }

        /// <summary>
        /// 회원관리 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _member.Delete(id);
        }

        /// <summary>
        /// 회원관리 비밀번호 재설정
        /// </summary>
        public bool Reset(string id, string change_pwd)
        {
            return _member.Reset(id, change_pwd);
        }

        /// <summary>
        /// 회원관리 수정
        /// </summary>
        public bool Modify(Model.Member member)
        {
            return _member.Modify(member);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 회원 조회
        /// </summary>
        public Model.Member UserDetail(string id)
        {
            return _member.UserDetail(id);
        }

        /// <summary>
        /// 회원 코드 중복체크
        /// </summary>
        public string UserCheckPno(string pno)
        {
            return _member.UserCheckPno(pno);
        }

        /// <summary>
        /// 회원 코드 중복체크
        /// </summary>
        public string UserCheck(string code, string name)
        {
            return _member.UserCheck(code, name);
        }

        /// <summary>
        /// 회원 로그인
        /// </summary>
        public Model.Member UserLogin(string code, string pwd)
        {
            return _member.UserLogin(code, pwd);
        }

        /// <summary>
        /// 회원 등록
        /// </summary>
        public bool UserRegist(Model.Member member)
        {
            return _member.UserRegist(member);
        }

        /// <summary>
        /// 회원 수정
        /// </summary>
        public bool UserModify(Model.Member member)
        {
            return _member.UserModify(member);
        }

        /// <summary>
        /// 회원 여행지 수정
        /// </summary>
        public bool UserTravel(string id, string travel)
        {
            return _member.UserTravel(id, travel);
        }
        #endregion

        /// <summary>
        /// 회원관리 소멸자
        /// </summary>
        public void Dispose()
        {
            _member = null;
        }
    }
}