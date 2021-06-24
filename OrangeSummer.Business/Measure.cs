using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeSummer.Business
{
    public class Measure : IDisposable
    {
        private Access.Measure _member = null;

        /// <summary>
        /// 회원관리 생성자
        /// </summary>
        public Measure(string connection)
        {
            _member = new Access.Measure(connection);
        }

        #region [ 관리자 ]
        /// <summary>
        /// 회원관리 리스트
        /// </summary>
        public List<Model.Measure> List(int page, int size, string gubun, string title, string useYn, string sdate, string edate)
        {
            return _member.List(page, size, gubun, title, useYn, sdate, edate);
        }

        /// <summary>
        /// 회원관리 엑셀
        /// </summary>
        public List<Model.Measure> Excel(string branch, string level, string code, string mobile, string sdate, string edate)
        {
            return null; // _member.Excel(branch, level, code, mobile, sdate, edate);
        }

        /// <summary>
        /// 회원관리 조회
        /// </summary>
        public Model.Measure Detail(string id)
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
        public bool Reset(string id)
        {
            return _member.Reset(id);
        }

        /// <summary>
        /// 회원관리 수정
        /// </summary>
        public bool Modify(Model.Measure member)
        {
            return _member.Modify(member);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 회원 조회
        /// </summary>
        public Model.Measure UserDetail(string id)
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
        public Model.Measure UserLogin(string code, string pwd)
        {
            return _member.UserLogin(code, pwd);
        }

        /// <summary>
        /// 회원 등록
        /// </summary>
        public bool Regist(Model.Measure member)
        {
            return _member.Regist(member);
        }

        /// <summary>
        /// 회원 수정
        /// </summary>
        public bool UserModify(Model.Measure member)
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