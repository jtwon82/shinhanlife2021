using System;
using System.Collections.Generic;

namespace OrangeSummer.Business
{
    /// <summary>
    /// 전윤기 - 2020.06.16
    /// 관리자 Business
    /// </summary>
    public class Admin : IDisposable
    {
        private Access.Admin _admin = null;

        /// <summary>
        /// 관리자 생성자
        /// </summary>
        public Admin(string connection)
        {
            _admin = new Access.Admin(connection);
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        public List<Model.Admin> List(int page, int size)
        {
            return _admin.List(page, size);
        }

        /// <summary>
        /// 관리자 등록
        /// </summary>
        public bool Regist(Model.Admin admin)
        {
            return _admin.Regist(admin);
        }

        /// <summary>
        /// 관리자 수정
        /// </summary>
        public bool Modify(Model.Admin admin)
        {
            return _admin.Modify(admin);
        }

        /// <summary>
        /// 관리자 비밀번호 재설정
        /// </summary>
        public bool Reset(string id)
        {
            return _admin.Reset(id);
        }

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        public bool Delete(string id)
        {
            return _admin.Delete(id);
        }

        /// <summary>
        /// 관리자 조회
        /// </summary>
        public Model.Admin Detail(string id)
        {
            return _admin.Detail(id);
        }

        /// <summary>
        /// 아이디 체크
        /// </summary>
        public Model.Admin Check(string usr)
        {
            return _admin.Check(usr);
        }

        /// <summary>
        /// 관리자 로그인
        /// </summary>
        public Model.Admin Login(string usr, string pwd)
        {
            return _admin.Login(usr, pwd);
        }

        /// <summary>
        /// 관리자 비밀번호 수정
        /// </summary>
        public bool Pwd(string id, string pwd)
        {
            return _admin.Pwd(id, pwd);
        }

        /// <summary>
        /// 관리자 소멸자
        /// </summary>
        public void Dispose()
        {
            _admin = null;
        }
    }
}
