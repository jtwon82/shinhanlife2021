using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using MLib.Util;
using MLib.Logger;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 회원관리 Access
    /// </summary>
    public class Member
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 회원관리 생성자
        /// </summary>
        public Member(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 회원관리 리스트
        /// </summary>
        public List<Model.Member> List(int page, int size, string branch, string level, string code, string mobile, string sdate, string edate)
        {
            List<Model.Member> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@KEY_BRANCH", branch));
            parameters.Add(new SqlParameter("@KEY_LEVEL", level));
            parameters.Add(new SqlParameter("@KEY_CODE", code));
            parameters.Add(new SqlParameter("@KEY_MOBILE", mobile));
            parameters.Add(new SqlParameter("@KEY_SDATE", sdate));
            parameters.Add(new SqlParameter("@KEY_EDATE", edate));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_MEMBER_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Member>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Member member = new Model.Member()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                            FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                            Code = dr["CODE"].ToString(),
                            Pwd = dr["PWD"].ToString(),
                            Reset = dr["RESET"].ToString(),
                            Level = dr["LEVEL"].ToString(),
                            Name = dr["NAME"].ToString(),
                            Mobile = dr["MOBILE"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(member);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 회원관리 엑셀
        /// </summary>
        public List<Model.Member> Excel(string branch, string level, string code, string mobile, string sdate, string edate)
        {
            List<Model.Member> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@KEY_BRANCH", branch));
            parameters.Add(new SqlParameter("@KEY_LEVEL", level));
            parameters.Add(new SqlParameter("@KEY_CODE", code));
            parameters.Add(new SqlParameter("@KEY_MOBILE", mobile));
            parameters.Add(new SqlParameter("@KEY_SDATE", sdate));
            parameters.Add(new SqlParameter("@KEY_EDATE", edate));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_MEMBER_EXCEL", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Member>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Member member = new Model.Member()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                            FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                            Code = dr["CODE"].ToString(),
                            Pwd = dr["PWD"].ToString(),
                            Reset = dr["RESET"].ToString(),
                            Level = dr["LEVEL"].ToString(),
                            Name = dr["NAME"].ToString(),
                            Mobile = dr["MOBILE"].ToString(),
                            DelYn = dr["DEL_YN"].ToString(),
                            RegistDate = dr["REGIST_DATE"].ToString(),
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(member);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        /// 회원관리 조회
        /// </summary>
        public Model.Member Detail(string id)
        {
            Model.Member member = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_MEMBER_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    member = new Model.Member()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                        FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                        Code = dr["CODE"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Level = dr["LEVEL"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Mobile = dr["MOBILE"].ToString(),
                        AdvertYn = dr["ADVERT_YN"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        Branch = new Model.Branch()
                        {
                            Name = dr["BRANCH_NAME"].ToString()
                        },
                        Travel = new Model.Travel()
                        {
                            Name = dr["TRAVEL_NAME"].ToString()
                        },
                        ProfileImg = dr["PROFILE_IMG"].ToString(),
                        BackgroundImg = dr["BACKGROUND_IMG"].ToString()
                    };
                }
            }

            return member;
        }

        /// <summary>
        /// 회원관리 삭제
        /// </summary>
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_MEMBER_DELETE", parameters);
        }

        /// <summary>
        /// 회원관리 비밀번호 재설정
        /// </summary>
        public bool Reset(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_MEMBER_RESET", parameters);
        }

        /// <summary>
        /// 회원 수정
        /// </summary>
        public bool Modify(Model.Member member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", member.Id));
            parameters.Add(new SqlParameter("@FK_BRANCH", member.FkBranch));
            parameters.Add(new SqlParameter("@LEVEL", member.Level));
            parameters.Add(new SqlParameter("@DEL_YN", member.DelYn));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_MEMBER_MODIFY", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 회원 조회
        /// </summary>
        public Model.Member UserDetail(string id)
        {
            Model.Member member = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_DETAIL", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    member = new Model.Member()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                        FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                        Code = dr["CODE"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Level = dr["LEVEL"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Mobile = dr["MOBILE"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        ProfileImg = dr["PROFILE_IMG"].ToString(),
                        BackgroundImg = dr["BACKGROUND_IMG"].ToString(),
                        Branch = new Model.Branch()
                        {
                            Name = dr["BRANCH_NAME"].ToString()
                        },
                        Travel = new Model.Travel()
                        {
                            AttPc = dr["BRANCH_ATT_PC"].ToString(),
                            AttMobile = dr["BRANCH_ATT_MOBILE"].ToString()
                        }
                    };
                }
            }

            return member;
        }

        /// <summary>
        /// 회원 전화번호 중복체크
        /// </summary>
        public string UserCheckPno(string pno)
        {
            string result = "FAIL";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MOBILE", pno));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_CHECK_PNO", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    result = dr["RESULT"].ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 회원 코드 중복체크
        /// </summary>
        public string UserCheck(string code, string name)
        {
            string result = "FAIL";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NAME", name));
            parameters.Add(new SqlParameter("@CODE", code));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_CHECKV2", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    result = dr["RESULT"].ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 회원 로그인
        /// </summary>
        public Model.Member UserLogin(string code, string pwd)
        {
            Model.Member member = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CODE", code));
            parameters.Add(new SqlParameter("@PWD", pwd));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_MEMBER_LOGIN", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];

                    member = new Model.Member()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                        FkTravel = dr["FK_TRAVEL"].ToString().ToUpper(),
                        Code = dr["CODE"].ToString(),
                        Pwd = dr["PWD"].ToString(),
                        Reset = dr["RESET"].ToString(),
                        Level = dr["LEVEL"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Mobile = dr["MOBILE"].ToString(),
                        DelYn = dr["DEL_YN"].ToString(),
                        RegistDate = dr["REGIST_DATE"].ToString(),
                        ProfileImg = dr["PROFILE_IMG"].ToString(),
                        BackgroundImg = dr["BACKGROUND_IMG"].ToString(),
                        Branch = new Model.Branch()
                        {
                            Id = dr["BRANCH_ID"].ToString().ToUpper(),
                            Name = dr["BRANCH_NAME"].ToString()
                        }
                    };
                }
            }

            return member;
        }

        /// <summary>
        /// 회원 등록
        /// </summary>
        public bool UserRegist(Model.Member member)
        {
            //List<SqlParameter> parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter("@ID", member.Id));
            //parameters.Add(new SqlParameter("@FK_BRANCH", ""));
            //parameters.Add(new SqlParameter("@FK_TRAVEL", ""));
            //parameters.Add(new SqlParameter("@CODE", member.Code));
            //parameters.Add(new SqlParameter("@PWD", member.Pwd));
            //parameters.Add(new SqlParameter("@RESET", "N"));
            //parameters.Add(new SqlParameter("@LEVEL", ""));
            //parameters.Add(new SqlParameter("@NAME", member.Name));
            //parameters.Add(new SqlParameter("@MOBILE", member.Mobile));
            //parameters.Add(new SqlParameter("@ADVERT_YN", member.AdvertYn));
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", member.Id));
            parameters.Add(new SqlParameter("@FK_BRANCH", member.Id));
            parameters.Add(new SqlParameter("@FK_TRAVEL", member.Id));
            parameters.Add(new SqlParameter("@CODE", member.Code));
            parameters.Add(new SqlParameter("@PWD", member.Pwd));
            parameters.Add(new SqlParameter("@RESET", member.Reset));
            parameters.Add(new SqlParameter("@LEVEL", member.Level));
            parameters.Add(new SqlParameter("@NAME", member.Name));
            parameters.Add(new SqlParameter("@MOBILE", member.Mobile));
            parameters.Add(new SqlParameter("@ADVERT_YN", member.AdvertYn));

            return DBHelper.ExecuteNonQuery(_connection, "USP_MEMBER_REGIST", parameters);
        }

        /// <summary>
        /// 회원 수정
        /// </summary>
        public bool UserModify(Model.Member member)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", member.Id));
            parameters.Add(new SqlParameter("@FK_BRANCH", member.FkBranch));
            parameters.Add(new SqlParameter("@PWD", member.Pwd));
            parameters.Add(new SqlParameter("@LEVEL", member.Level));
            parameters.Add(new SqlParameter("@NAME", member.Name));
            parameters.Add(new SqlParameter("@MOBILE", member.Mobile));
            parameters.Add(new SqlParameter("@PROFILE_IMG", member.ProfileImg));
            parameters.Add(new SqlParameter("@BACKGROUND_IMG", member.BackgroundImg));

            return DBHelper.ExecuteNonQuery(_connection, "USP_MEMBER_MODIFY", parameters);
        }

        /// <summary>
        /// 회원 여행지 수정
        /// </summary>
        public bool UserTravel(string id, string travel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@FK_TRAVEL", travel));

            return DBHelper.ExecuteNonQuery(_connection, "USP_MEMBER_TRAVEL", parameters);
        }
        #endregion
    }
}
