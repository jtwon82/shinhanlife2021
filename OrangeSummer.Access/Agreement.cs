using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 약관내용 Access
    /// </summary>
    public class Agreement
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 약관내용 생성자
        /// </summary>
        public Agreement(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 약관내용 조회
        /// </summary>
        public Model.Agreement Detail()
        {
            Model.Agreement agreement = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_AGREEMENT_DETAIL"))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    agreement = new Model.Agreement()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        Service = dr["SERVICE"].ToString(),
                        Person = dr["PERSON"].ToString(),
                        Marketing = dr["MARKETING"].ToString()
                    };
                }
            }

            return agreement;
        }

        /// <summary>
        /// 약관내용 수정
        /// </summary>
        public bool Modify(Model.Agreement agreement)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SERVICE", agreement.Service));
            parameters.Add(new SqlParameter("@PERSON", agreement.Person));
            parameters.Add(new SqlParameter("@MARKETING", agreement.Marketing));

            return DBHelper.ExecuteNonQuery(_connection, "ADM_AGREEMENT_MODIFY", parameters);
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 약관내용 조회
        /// </summary>
        public Model.Agreement UserDetail()
        {
            Model.Agreement agreement = null;
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_AGREEMENT_DETAIL"))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    agreement = new Model.Agreement()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        Service = dr["SERVICE"].ToString(),
                        Person = dr["PERSON"].ToString()
                    };
                }
            }

            return agreement;
        }
        #endregion
    }
}
