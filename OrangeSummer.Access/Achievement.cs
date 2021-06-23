﻿using MLib.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using MLib.Util;

namespace OrangeSummer.Access
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 업적관리 Access
    /// </summary>
    public class Achievement
    {
        private string _connection = string.Empty;

        /// <summary>
        /// 업적관리 생성자
        /// </summary>
        public Achievement(string connection)
        {
            _connection = connection;
        }

        #region [ 관리자 ]
        /// <summary>
        /// 업적관리(임시) 등록
        /// </summary>
        public DataTable Regist(DataTable dt)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            DBHelper.ExecuteNonInQuery(_connection, "ADM_ACHIEVEMENT_DELETE");
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    parameters.Clear();
                    parameters.Add(new SqlParameter("@ORDERBY", index));
                    parameters.Add(new SqlParameter("@DATE", dr[0].ToString()));
                    parameters.Add(new SqlParameter("@SECTOR_NAME", dr[1].ToString()));
                    parameters.Add(new SqlParameter("@BRANCH_NAME", dr[2].ToString()));
                    parameters.Add(new SqlParameter("@CODE", dr[3].ToString()));
                    parameters.Add(new SqlParameter("@NAME", dr[4].ToString()));
                    parameters.Add(new SqlParameter("@LEVEL", dr[5].ToString()));
                    parameters.Add(new SqlParameter("@PERSON_CMIP", dr[6].ToString()));
                    parameters.Add(new SqlParameter("@PERSON_CAMP", dr[7].ToString()));
                    parameters.Add(new SqlParameter("@SL_CMIP", dr[8].ToString()));
                    parameters.Add(new SqlParameter("@BRANCH_CMIP", dr[9].ToString()));
                    parameters.Add(new SqlParameter("@PERSON_RANK", dr[10].ToString()));
                    parameters.Add(new SqlParameter("@PERSON_RANK2", dr[11].ToString()));
                    parameters.Add(new SqlParameter("@SL_RANK", dr[12].ToString()));
                    parameters.Add(new SqlParameter("@BRANCH_RANK", dr[13].ToString()));

                    //parameters.Add(new SqlParameter("@PART", dr[1].ToString()));
                    //parameters.Add(new SqlParameter("@BRANCH", dr[2].ToString()));
                    //parameters.Add(new SqlParameter("@CODE", dr[3].ToString()));
                    //parameters.Add(new SqlParameter("@NAME", dr[4].ToString()));
                    //parameters.Add(new SqlParameter("@LEVEL", dr[5].ToString()));
                    //parameters.Add(new SqlParameter("@PERSON_SUM", dr[6].ToString()));
                    //parameters.Add(new SqlParameter("@PERSON_CMIP", dr[7].ToString()));
                    //parameters.Add(new SqlParameter("@LEADER_CMIP", "0"));
                    //parameters.Add(new SqlParameter("@BRANCH_CMIP", dr[9].ToString()));
                    //parameters.Add(new SqlParameter("@PERSON_RANK", dr[10].ToString()));
                    //parameters.Add(new SqlParameter("@LEADER_RANK", "0"));
                    //parameters.Add(new SqlParameter("@BRANCH_RANK", dr[12].ToString()));
                    //parameters.Add(new SqlParameter("@SL_CMIP", dr[8].ToString()));
                    //parameters.Add(new SqlParameter("@SL_RANK", dr[11].ToString()));

                    bool result = DBHelper.ExecuteNonQuery(_connection, "ADM_ACHIEVEMENT_REGIST", parameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                index++;
            }

            return DBHelper.ExecuteDataTable(_connection, "ADM_ACHIEVEMENT_CHECK");
        }

        /// <summary>
        /// 업적관리 리스트
        /// </summary>
        public List<Model.Achievement> List(int page, int size, string orderby, string branch, string level, string code, string name)
        {
            List<Model.Achievement> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@ORDERBY", orderby));
            parameters.Add(new SqlParameter("@KEY_BRANCH", branch));
            parameters.Add(new SqlParameter("@KEY_LEVEL", level));
            parameters.Add(new SqlParameter("@KEY_CODE", code));
            parameters.Add(new SqlParameter("@KEY_NAME", name));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "ADM_ACHIEVEMENT_LIST", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Achievement>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Achievement achievement = new Model.Achievement()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            Date = dr["DATE"].ToString(),
                            Part = dr["PART"].ToString(),
                            FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                            Code = dr["CODE"].ToString(),
                            Name = dr["NAME"].ToString(),
                            Level = dr["LEVEL"].ToString(),
                            PersonSum = dr["PERSON_SUM"].ToString(),
                            PersonCmip = Check.IsNone(dr["PERSON_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CMIP"].ToString()).ToString("#,##0"),
                            PersonCamp = Check.IsNone(dr["PERSON_CAMP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CAMP"].ToString()).ToString("#,##0"),
                            PersonRank = Check.IsNone(dr["PERSON_RANK"].ToString()) ? "" : dr["PERSON_RANK"].ToString(),
                            PersonRank2 = Check.IsNone(dr["PERSON_RANK2"].ToString()) ? "" : dr["PERSON_RANK2"].ToString(),
                            LeaderCmip = Check.IsNone(dr["LEADER_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["LEADER_CMIP"].ToString()).ToString("#,##0"),
                            LeaderRank = Check.IsNone(dr["LEADER_RANK"].ToString()) ? "" : dr["LEADER_RANK"].ToString(),
                            BranchCmip = Check.IsNone(dr["BRANCH_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["BRANCH_CMIP"].ToString()).ToString("#,##0"),
                            BranchRank = Check.IsNone(dr["BRANCH_RANK"].ToString()) ? "" : dr["BRANCH_RANK"].ToString(),
                            SlCmip = Check.IsNone(dr["SL_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["SL_CMIP"].ToString()).ToString("#,##0"),
                            SlRank = Check.IsNone(dr["SL_RANK"].ToString()) ? "" : dr["SL_RANK"].ToString(),
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(achievement);
                    }
                }
            }

            return lists;
        }
        #endregion

        #region [ 사용자 ]
        /// <summary>
        /// 업적관리 리스트
        /// </summary>
        public Model.Achievement UserList(string code)
        {
            Model.Achievement achievement = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CODE", code));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_ACHIEVEMENT_LIST", parameters))
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    achievement = new Model.Achievement()
                    {
                        Id = dr["ID"].ToString().ToUpper(),
                        Sort = Convert.ToInt32(dr["SORT"].ToString()),
                        Date = dr["DATE"].ToString(),
                        Part = dr["PART"].ToString(),
                        FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                        Code = dr["CODE"].ToString(),
                        Name = dr["NAME"].ToString(),
                        Level = dr["LEVEL"].ToString(),
                        PersonSum = dr["PERSON_SUM"].ToString(),
                        PersonCmip = Check.IsNone(dr["PERSON_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CMIP"].ToString()).ToString("#,##0"),
                        PersonRank = Check.IsNone(dr["PERSON_RANK"].ToString()) ? "" : dr["PERSON_RANK"].ToString(),
                        LeaderCmip = Check.IsNone(dr["LEADER_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["LEADER_CMIP"].ToString()).ToString("#,##0"),
                        LeaderRank = Check.IsNone(dr["LEADER_RANK"].ToString()) ? "" : dr["LEADER_RANK"].ToString(),
                        BranchCmip = Check.IsNone(dr["BRANCH_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["BRANCH_CMIP"].ToString()).ToString("#,##0"),
                        BranchRank = Check.IsNone(dr["BRANCH_RANK"].ToString()) ? "" : dr["BRANCH_RANK"].ToString(),
                        SlCmip = Check.IsNone(dr["SL_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["SL_CMIP"].ToString()).ToString("#,##0"),
                        SlRank = Check.IsNone(dr["SL_RANK"].ToString()) ? "" : dr["SL_RANK"].ToString()
                    };
                }
            }

            return achievement;
        }
        #endregion

        #region [ 사용자 List2 ]
        /// <summary>
        /// 업적관리 리스트 List 
        /// </summary>
        public List<Model.Achievement> UserList2(string code, string level)
        {
            List<Model.Achievement> achievement = new List<Model.Achievement>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CODE", code));
            parameters.Add(new SqlParameter("@LEVEL", level));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_ACHIEVEMENT_LIST2", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Achievement T = new Model.Achievement()
                        {
                            //Id = dr["ID"].ToString().ToUpper(),
                            //Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            Date = dr["DATE"].ToString(),
                            //Part = dr["PART"].ToString(),
                            //FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                            //Code = dr["CODE"].ToString(),
                            //Name = dr["NAME"].ToString(),
                            //Level = dr["LEVEL"].ToString(),
                            PersonSum = dr["PERSON_SUM"].ToString(),
                            PersonCmip = Check.IsNone(dr["PERSON_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CMIP"].ToString()).ToString("#,##0"),
                            PersonCamp = Check.IsNone(dr["PERSON_CAMP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CAMP"].ToString()).ToString("#,##0"),
                            PersonRank = Check.IsNone(dr["PERSON_RANK"].ToString()) ? "" : dr["PERSON_RANK"].ToString(),
                            PersonRank2 = Check.IsNone(dr["PERSON_RANK2"].ToString()) ? "" : dr["PERSON_RANK2"].ToString(),
                            //LeaderCmip = Check.IsNone(dr["LEADER_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["LEADER_CMIP"].ToString()).ToString("#,##0"),
                            //LeaderRank = Check.IsNone(dr["LEADER_RANK"].ToString()) ? "" : dr["LEADER_RANK"].ToString(),
                            BranchCmip = Check.IsNone(dr["BRANCH_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["BRANCH_CMIP"].ToString()).ToString("#,##0"),
                            BranchRank = Check.IsNone(dr["BRANCH_RANK"].ToString()) ? "" : dr["BRANCH_RANK"].ToString(),
                            SlCmip = Check.IsNone(dr["SL_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["SL_CMIP"].ToString()).ToString("#,##0"),
                            SlRank = Check.IsNone(dr["SL_RANK"].ToString()) ? "" : dr["SL_RANK"].ToString()
                        };
                        achievement.Add(T);
                    }
                }
            }

            return achievement;
        }
        #endregion

        //#region [ 사용자 List2 ]
        ///// <summary>
        ///// 업적관리 리스트 List 
        ///// </summary>
        //public List<Model.Achievement> UserList2FC(string code)
        //{
        //    List<Model.Achievement> achievement = new List<Model.Achievement>();
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("@CODE", code));
        //    using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_ACHIEVEMENT_LIST2_FC", parameters))
        //    {
        //        if (dt.Rows.Count >0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                Model.Achievement T = new Model.Achievement()
        //                {
        //                    Id = dr["ID"].ToString().ToUpper(),
        //                    Sort = Convert.ToInt32(dr["SORT"].ToString()),
        //                    Date = dr["DATE"].ToString(),
        //                    Part = dr["PART"].ToString(),
        //                    FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
        //                    Code = dr["CODE"].ToString(),
        //                    Name = dr["NAME"].ToString(),
        //                    Level = dr["LEVEL"].ToString(),
        //                    PersonSum = dr["PERSON_SUM"].ToString(),
        //                    PersonCmip = Check.IsNone(dr["PERSON_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CMIP"].ToString()).ToString("#,##0"),
        //                    PersonCamp = Check.IsNone(dr["PERSON_CAMP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CAMP"].ToString()).ToString("#,##0"),
        //                    PersonRank = Check.IsNone(dr["PERSON_RANK"].ToString()) ? "" : dr["PERSON_RANK"].ToString(),
        //                    PersonRank2 = Check.IsNone(dr["PERSON_RANK2"].ToString()) ? "" : dr["PERSON_RANK2"].ToString(),
        //                    LeaderCmip = Check.IsNone(dr["LEADER_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["LEADER_CMIP"].ToString()).ToString("#,##0"),
        //                    LeaderRank = Check.IsNone(dr["LEADER_RANK"].ToString()) ? "" : dr["LEADER_RANK"].ToString(),
        //                    BranchCmip = Check.IsNone(dr["BRANCH_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["BRANCH_CMIP"].ToString()).ToString("#,##0"),
        //                    BranchRank = Check.IsNone(dr["BRANCH_RANK"].ToString()) ? "" : dr["BRANCH_RANK"].ToString(),
        //                    SlCmip = Check.IsNone(dr["SL_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["SL_CMIP"].ToString()).ToString("#,##0"),
        //                    SlRank = Check.IsNone(dr["SL_RANK"].ToString()) ? "" : dr["SL_RANK"].ToString()
        //                };
        //                achievement.Add(T);
        //            }
        //        }
        //    }

        //    return achievement;
        //}
        //#endregion

        /// <summary>
        /// 업적 랭킹
        /// </summary>
        public List<Model.Achievement> UserRanking(int page, int size, string part)
        {
            List<Model.Achievement> lists = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PAGE", page));
            parameters.Add(new SqlParameter("@SIZE", size));
            parameters.Add(new SqlParameter("@PART", part));
            using (DataTable dt = DBHelper.ExecuteDataTable(_connection, "USP_ACHIEVEMENT_RANKING", parameters))
            {
                if (dt.Rows.Count > 0)
                {
                    lists = new List<Model.Achievement>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.Achievement achievement = new Model.Achievement()
                        {
                            Total = Convert.ToInt32(dr["TOTAL"].ToString()),
                            Id = dr["ID"].ToString().ToUpper(),
                            Sort = Convert.ToInt32(dr["SORT"].ToString()),
                            Date = dr["DATE"].ToString(),
                            Part = dr["PART"].ToString(),
                            FkBranch = dr["FK_BRANCH"].ToString().ToUpper(),
                            Code = dr["CODE"].ToString(),
                            Name = dr["NAME"].ToString(),
                            Level = dr["LEVEL"].ToString(),
                            PersonSum = dr["PERSON_SUM"].ToString(),
                            PersonCmip = Check.IsNone(dr["PERSON_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["PERSON_CMIP"].ToString()).ToString("#,##0"),
                            PersonRank = Check.IsNone(dr["PERSON_RANK"].ToString()) ? "" : dr["PERSON_RANK"].ToString(),
                            LeaderCmip = Check.IsNone(dr["LEADER_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["LEADER_CMIP"].ToString()).ToString("#,##0"),
                            LeaderRank = Check.IsNone(dr["LEADER_RANK"].ToString()) ? "" : dr["LEADER_RANK"].ToString(),
                            BranchCmip = Check.IsNone(dr["BRANCH_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["BRANCH_CMIP"].ToString()).ToString("#,##0"),
                            BranchRank = Check.IsNone(dr["BRANCH_RANK"].ToString()) ? "" : dr["BRANCH_RANK"].ToString(),
                            SlCmip = Check.IsNone(dr["SL_CMIP"].ToString()) ? "" : Convert.ToDecimal(dr["SL_CMIP"].ToString()).ToString("#,##0"),
                            SlRank = Check.IsNone(dr["SL_RANK"].ToString()) ? "" : dr["SL_RANK"].ToString(),
                            Branch = new Model.Branch()
                            {
                                Name = dr["BRANCH_NAME"].ToString()
                            }
                        };

                        lists.Add(achievement);
                    }
                }
            }

            return lists;
        }
    }
}
