using MLib.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace MLib.DataBase
{
    public class Template
    {
        private string _connection = string.Empty;
        private string _table = string.Empty;
        private string _author = string.Empty;
        private string _description = string.Empty;
        private string _prefix = string.Empty;
        private string _instance = string.Empty;
        private string _filename = string.Empty;
        private string _className = string.Empty;
        private string _root = string.Empty;
        private DataSet _ds = null;

        /// <summary>
        /// 기본템플릿 생성자
        /// </summary>
        /// <param name="connection">DB연결 문자열</param>
        /// <param name="author">작업자 이름</param>
        /// <param name="table">테이블 명</param>
        /// <param name="description">테이블 설명</param>
        /// <param name="prefix">프로시져 생성 이니설</param>
        public Template(string connection, string author, string table, string description, string prefix, string root, string instance)
        {
            _connection = connection;
            _author = author;
            _table = table;
            _description = description;
            _prefix = prefix;
            _root = root;
            _instance = instance;
            _className = instance.Substring(0, 1).ToUpper() + instance.Substring(1, instance.Length - 1);
            _filename = new CultureInfo("en-US", false).TextInfo.ToTitleCase(_instance);
            _ds = TableInfo();
        }

        public void All()
        {
            Procedure();
            Model();
            Access();
            Business();
            Source();
        }

        #region [ 프로시져 ]
        public void Procedure()
        {
            if (_ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                string column = string.Empty;
                string type = string.Empty;
                string length = string.Empty;
                string isnull = string.Empty;
                string key = string.Empty;

                #region [ 리스트 ]
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("-- Author: @" + _author);
                sb.AppendLine("-- Create date: @" + DateTime.Now.ToString("yyyy.MM.dd") + "");
                sb.AppendLine("-- Description: @" + _description + " 리스트");
                sb.AppendLine("-- Sample: EXEC " + _prefix + "_LIST");
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("CREATE PROCEDURE [dbo].[" + _prefix + "_LIST]");
                sb.AppendLine("\t@PAGE\tINT,");
                sb.AppendLine("\t@SIZE\tINT");
                sb.AppendLine("AS");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");
                sb.AppendLine("\tWITH CTE_PAGELIST AS  ");
                sb.AppendLine("\t(");
                sb.AppendLine("\t\tSELECT COUNT(*) OVER() AS [TOTAL], ROW_NUMBER() OVER(ORDER BY [SORT] DESC) AS [ROW],");
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString().ToUpper();
                    if (i.Equals(0))
                        sb.Append("\t\t\t[" + column + "]");
                    else
                    {
                        if (type.Equals("VARBINARY"))
                            sb.Append("dbo.UFNC_DECRYPT([" + column + "]) AS [" + column + "]");
                        else if (type.Equals("DATETIME"))
                            sb.Append("CONVERT(VARCHAR(50), [" + column + "], 121) AS [" + column + "]");
                        else
                            sb.Append("[" + column + "]");
                    }

                    if (!i.Equals(_ds.Tables[0].Rows.Count - 1))
                        sb.Append(", ");

                    if (i % 5 == 4)
                    {
                        sb.AppendLine("");
                        sb.Append("\t\t\t");
                    }
                }
                sb.AppendLine("\t");
                sb.AppendLine("\t\tFROM [" + _table +"]");
                sb.AppendLine("\t)");
                sb.AppendLine("\tSELECT * FROM CTE_PAGELIST  ");
                sb.AppendLine("\tWHERE [ROW] BETWEEN ((@PAGE - 1) * @SIZE) + 1 AND @PAGE * @SIZE");
                sb.AppendLine("");
                sb.AppendLine("END");
                sb.AppendLine("GO");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");
                #endregion

                #region [ 조회 ]
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("-- Author: @" + _author);
                sb.AppendLine("-- Create date: @" + DateTime.Now.ToString("yyyy.MM.dd") + "");
                sb.AppendLine("-- Description: @" + _description + " 조회");
                sb.AppendLine("-- Sample: EXEC " + _prefix + "_DETAIL");
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("CREATE PROCEDURE [dbo].[" + _prefix + "_DETAIL]");
                column = _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString();
                type = _ds.Tables[0].Rows[0]["DATA_TYPE"].ToString().ToUpper();
                length = _ds.Tables[0].Rows[0]["CHARACTER_MAXIMUM_LENGTH"].ToString();
                isnull = (length.Equals("0")) ? "" : "(" + length + ")";
                sb.AppendLine("\t@" + column + "\t" + type + isnull);
                sb.AppendLine("AS");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");
                sb.Append("\tSELECT ");
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString().ToUpper();
                    if (type.Equals("VARBINARY"))
                        sb.Append(string.Format("dbo.UFNC_DECRYPT([{0}]) AS [{1}]", column, column));
                    else if (type.Equals("DATETIME"))
                        sb.Append("CONVERT(VARCHAR(50), [" + column + "], 121) AS [" + column + "]");
                    else
                        sb.Append(string.Format("[{0}]", column));

                    if (!i.Equals(_ds.Tables[0].Rows.Count - 1))
                        sb.Append(", ");

                    if (i % 5 == 4)
                    {
                        sb.AppendLine("");
                        sb.Append("\t\t");
                    }
                }
                sb.AppendLine("");
                sb.AppendLine(string.Format("\tFROM [{0}]", _table));
                sb.AppendLine(string.Format("\tWHERE [{0}] = @{1}", _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString(), _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString()));
                sb.AppendLine("");
                sb.AppendLine("END");
                sb.AppendLine("GO");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");
                #endregion

                #region [ 등록 ]
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("-- Author: @" + _author);
                sb.AppendLine("-- Create date: @" + DateTime.Now.ToString("yyyy.MM.dd") + "");
                sb.AppendLine("-- Description: @" + _description + " 등록");
                sb.AppendLine("-- Sample: EXEC " + _prefix + "_REGIST");
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("CREATE PROCEDURE [dbo].[" + _prefix + "_REGIST]");
                sb.AppendLine(Parameter(_ds.Tables[0].Rows));
                sb.AppendLine("AS");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");
                sb.Append("\tINSERT INTO [" + _table + "] (");
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    key = _ds.Tables[0].Rows[i]["KEY"].ToString();

                    if (!key.Equals("IDENTITY"))
                    {
                        sb.Append("[" + column + "]");
                        if (!i.Equals(_ds.Tables[0].Rows.Count - 1))
                            sb.Append(", ");

                        if (i % 5 == 4)
                        {
                            sb.AppendLine("");
                            sb.Append("\t\t");
                        }
                    }
                }
                sb.AppendLine(")");
                sb.Append("\tVALUES (");
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString().ToUpper();
                    key = _ds.Tables[0].Rows[i]["KEY"].ToString();

                    if (!key.Equals("IDENTITY"))
                    {
                        if (type.Equals("VARBINARY"))
                            sb.Append("dbo.UFNC_ENCRYPT(@" + column + ")");
                        else
                            sb.Append("@" + column + "");

                        if (!i.Equals(_ds.Tables[0].Rows.Count - 1))
                            sb.Append(", ");

                        if (i % 5 == 4)
                        {
                            sb.AppendLine("");
                            sb.Append("\t\t");
                        }
                    }
                }
                sb.AppendLine(")");
                sb.AppendLine("");
                sb.AppendLine("END");
                sb.AppendLine("GO");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");
                #endregion

                #region [ 수정 ]
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("-- Author: @" + _author);
                sb.AppendLine("-- Create date: @" + DateTime.Now.ToString("yyyy.MM.dd") + "");
                sb.AppendLine("-- Description: @" + _description + " 수정");
                sb.AppendLine("-- Sample: EXEC " + _prefix + "_MODIFY");
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("CREATE PROCEDURE [dbo].[" + _prefix + "_MODIFY]");
                sb.AppendLine(Parameter(_ds.Tables[0].Rows));
                sb.AppendLine("AS");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");
                sb.AppendLine("\tUPDATE [" + _table + "] SET ");
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                    type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString().ToUpper();
                    key = _ds.Tables[0].Rows[i]["KEY"].ToString().ToUpper();

                    if (!key.Equals("IDENTITY") && !key.Equals("PK") && !key.Equals("FK"))
                    {
                        if (type.Equals("VARBINARY"))
                            sb.Append(string.Format("\t\t[{0}] = dbo.UFNC_ENCRYPT(@{1})", column, column));
                        else
                            sb.Append(string.Format("\t\t[{0}] = @{1}", column, column));

                        if (!i.Equals(_ds.Tables[0].Rows.Count - 1))
                            sb.AppendLine(", ");
                        else
                            sb.AppendLine("");
                    }
                }
                sb.AppendLine(string.Format("\tWHERE {0} = @{1}", _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString(), _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString()));
                sb.AppendLine("END");
                sb.AppendLine("GO");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");
                #endregion

                #region [ 삭제 ]
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("-- Author: @" + _author);
                sb.AppendLine("-- Create date: @" + DateTime.Now.ToString("yyyy.MM.dd") + "");
                sb.AppendLine("-- Description: @" + _description + " 삭제");
                sb.AppendLine("-- Sample: EXEC " + _prefix + "_DELETE");
                sb.AppendLine("-- ===========================================================");
                sb.AppendLine("CREATE PROCEDURE [dbo].[" + _prefix + "_DELETE]");
                column = _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString();
                type = _ds.Tables[0].Rows[0]["DATA_TYPE"].ToString().ToUpper();
                length = _ds.Tables[0].Rows[0]["CHARACTER_MAXIMUM_LENGTH"].ToString();
                isnull = (length.Equals("0")) ? "" : "(" + length + ")";
                sb.AppendLine("\t@" + column + "\t" + type + isnull);
                sb.AppendLine("AS");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");
                sb.AppendLine(string.Format("\tDELETE FROM [{0}] WHERE [{1}] = @{2}", _table, _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString(), _ds.Tables[0].Rows[0]["COLUMN_NAME"].ToString()));
                sb.AppendLine("");
                sb.AppendLine("END");
                sb.AppendLine("GO");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");
                #endregion

                Tool.Print(sb.ToString().Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
            }
        }
        #endregion

        #region [ Model ]
        public void Model()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace "+ _root + ".Model");
            sb.AppendLine("{");
            sb.AppendLine("\t/// &lt;summary&gt;");
            sb.AppendLine("\t/// " + _author + " - " + DateTime.Now.ToString("yyyy.MM.dd"));
            sb.AppendLine("\t/// " + _description + " Model");
            sb.AppendLine("\t/// &lt;/summary&gt;");
            sb.AppendLine("\tpublic class " + _className);
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tpublic int Total { get; set; }");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();
                sb.AppendLine("\t\tpublic " + Type(type) + " " + Pascal(column) + " { get; set; }");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            Tool.Print(sb.ToString().Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
        }
        #endregion

        #region [ Access ]
        public void Access()
        {
            StringBuilder sb = new StringBuilder();
            TextInfo text = new CultureInfo("en-US", false).TextInfo;
            string type = string.Empty;
            string column = string.Empty;
            string key = string.Empty;

            sb.AppendLine("\nusing MLib.DataBase;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + _root + ".Access");
            sb.AppendLine("{");
            sb.AppendLine("\t/// &lt;summary&gt;");
            sb.AppendLine("\t/// " + _author + " - " + DateTime.Now.ToString("yyyy.MM.dd"));
            sb.AppendLine("\t/// " + _description + " Access");
            sb.AppendLine("\t/// &lt;/summary&gt;");
            sb.AppendLine("\tpublic class " + _className);
            sb.AppendLine("\t{");

            sb.AppendLine("\t\tprivate string _connection = string.Empty;");
            sb.AppendLine("");
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 생성자");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic " + _className + "(string connection)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_connection = connection;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");

            #region [ 리스트 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 리스트");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic List&lt;Model." + _className + "&gt; List(int page, int size)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tList&lt;Model." + _className + "&gt; lists = null;");
            sb.AppendLine("\t\t\tList&lt;SqlParameter&gt; parameters = new List&lt;SqlParameter&gt;();");
            sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@PAGE\", page));");
            sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@SIZE\", size));");
            sb.AppendLine("\t\t\tusing (DataTable dt = DBHelper.ExecuteDataTable(_connection, \"" + _prefix + "_LIST\", parameters))");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tif (dt.Rows.Count > 0)");
            sb.AppendLine("\t\t\t\t{");
            sb.AppendLine("\t\t\t\t\tlists = new List&lt;Model." + _className + "&gt;();");
            sb.AppendLine("\t\t\t\t\tforeach (DataRow dr in dt.Rows)");
            sb.AppendLine("\t\t\t\t\t{");
            sb.AppendLine("\t\t\t\t\t\tModel." + _className + " " + _instance + " = new Model." + _className + "()");
            sb.AppendLine("\t\t\t\t\t\t{");
            sb.AppendLine("\t\t\t\t\t\t\tTotal = Convert.ToInt32(dr[\"TOTAL\"].ToString()),");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();
                if (i == (_ds.Tables[0].Rows.Count - 1))
                    sb.AppendLine("\t\t\t\t\t\t\t" + Binding(column, type));
                else
                    sb.AppendLine("\t\t\t\t\t\t\t" + Binding(column, type) +", ");
            }
            sb.AppendLine("\t\t\t\t\t\t};");
            sb.AppendLine("");
            sb.AppendLine("\t\t\t\t\t\tlists.Add(" + _instance + ");");
            sb.AppendLine("\t\t\t\t\t}");
            sb.AppendLine("\t\t\t\t}");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\t");
            sb.AppendLine("\t\t\treturn lists;");
            sb.AppendLine("\t\t}");
            #endregion

            #region [ 조회 ]
            sb.AppendLine("");
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 조회");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic Model." + _className + " Detail(string id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tModel." + _className + " " + _instance + " = null;");
            sb.AppendLine("\t\t\tList&lt;SqlParameter&gt; parameters = new List&lt;SqlParameter&gt;();");
            sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@ID\", id));");
            sb.AppendLine("\t\t\tusing (DataTable dt = DBHelper.ExecuteDataTable(_connection, \"" + _prefix + "_DETAIL\", parameters))");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tif (dt.Rows.Count == 1)");
            sb.AppendLine("\t\t\t\t{");
            sb.AppendLine("\t\t\t\t\tDataRow dr = dt.Rows[0];");
            sb.AppendLine("\t\t\t\t\t" + _instance + " = new Model." + _className + "()");
            sb.AppendLine("\t\t\t\t\t{");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();

                if (i == (_ds.Tables[0].Rows.Count - 1))
                    sb.AppendLine("\t\t\t\t\t\t" + Binding(column, type));
                else
                    sb.AppendLine("\t\t\t\t\t\t" + Binding(column, type) + ", ");
            }
            sb.AppendLine("\t\t\t\t\t};");
            sb.AppendLine("\t\t\t\t}");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("");
            sb.AppendLine("\t\t\treturn " + _instance + ";");
            sb.AppendLine("\t\t}");
            #endregion

            #region [ 등록 ]
            sb.AppendLine("");
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 등록");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Regist(Model." + _className + " " + _instance + ")");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tList&lt;SqlParameter&gt; parameters = new List&lt;SqlParameter&gt;();");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                key = _ds.Tables[0].Rows[i]["KEY"].ToString();
                if (!key.Equals("IDENTITY"))
                {
                    sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@" + column + "\", " + _instance + "." + Pascal(column) + "));");
                }
            }
            sb.AppendLine("");
            sb.AppendLine("\t\t\treturn DBHelper.ExecuteNonQuery(_connection, \"" + _prefix + "_REGIST\", parameters);");
            sb.AppendLine("\t\t}");
            #endregion

            #region [ 수정 ]
            sb.AppendLine("");
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 수정");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Modify(Model." + _className + " " + _instance + ")");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tList&lt;SqlParameter&gt; parameters = new List&lt;SqlParameter&gt;();");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                key = _ds.Tables[0].Rows[i]["KEY"].ToString();
                if (!key.Equals("IDENTITY"))
                {
                    sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@" + column + "\", " + _instance + "." + Pascal(column) + "));");
                }
            }
            sb.AppendLine("");
            sb.AppendLine("\t\t\treturn DBHelper.ExecuteNonQuery(_connection, \"" + _prefix + "_MODIFY\", parameters);");
            sb.AppendLine("\t\t}");
            #endregion

            #region [ 삭제 ]
            sb.AppendLine("");
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 삭제");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Delete(string id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tList&lt;SqlParameter&gt; parameters = new List&lt;SqlParameter&gt;();");
            sb.AppendLine("\t\t\tparameters.Add(new SqlParameter(\"@ID\", id));");
            sb.AppendLine("");
            sb.AppendLine("\t\t\treturn DBHelper.ExecuteNonQuery(_connection, \"" + _prefix + "_DELETE\", parameters);");
            sb.AppendLine("\t\t}");
            #endregion

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            Tool.Print(sb.ToString().Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
        }
        #endregion

        #region [ Business ]
        public void Business()
        {
            StringBuilder sb = new StringBuilder();
            TextInfo text = new CultureInfo("en-US", false).TextInfo;
            string column = string.Empty;

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("");
            sb.AppendLine("namespace "+ _root +".Business");
            sb.AppendLine("{");
            sb.AppendLine("\t/// &lt;summary&gt;");
            sb.AppendLine("\t/// " + _author + " - " + DateTime.Now.ToString("yyyy.MM.dd"));
            sb.AppendLine("\t/// " + _description + " Business");
            sb.AppendLine("\t/// &lt;/summary&gt;");
            sb.AppendLine("\tpublic class " + _className + " : IDisposable");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tprivate Access." + _className + " _" + _instance + " = null;");
            sb.AppendLine("");

            #region [ 생성자 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 생성자");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic " + _className + "(string connection)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_" + _instance + " = new Access." + _className + "(connection);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 리스트 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 리스트");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic List&lt;Model." + _className + "&gt; List(int page, int size)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn _" + _instance + ".List(page, size);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 조회 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 조회");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic Model." + _className + " Detail(string id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn _" + _instance + ".Detail(id);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 등록 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 등록");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Regist(Model." + _className + " " + _instance + ")");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn _" + _instance + ".Regist(" + _instance + ");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 수정 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 수정");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Modify(Model." + _className + " " + _instance + ")");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn _" + _instance + ".Modify(" + _instance + ");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 삭제 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 삭제");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic bool Delete(string id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn _" + _instance + ".Delete(id);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("");
            #endregion

            #region [ 소멸자 ]
            sb.AppendLine("\t\t/// &lt;summary&gt;");
            sb.AppendLine("\t\t/// " + _description + " 소멸자");
            sb.AppendLine("\t\t/// &lt;/summary&gt;");
            sb.AppendLine("\t\tpublic void Dispose()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_" + _instance + " = null;");
            sb.AppendLine("\t\t}");
            #endregion

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");

            Tool.Print(sb.ToString().Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
        }
        #endregion

        #region [ 사용법 ]
        public void Source()
        {
            StringBuilder sb = new StringBuilder();
            #region [ 리스트 ]
            sb.AppendLine("protected string _paging = string.Empty;");
            sb.AppendLine("private int _size = 10;");
            sb.AppendLine("private int _block = 10;");
            sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\tif (!IsPostBack)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tPageLoad();");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 리스트");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("private void PageLoad()");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tint page = Check.IsNone(Request[\"page\"], 1);");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tint total = 0;");
            sb.AppendLine("\t\t\tList&lt;Model." + _className + "&gt; list = biz.List(page, _size);");
            sb.AppendLine("\t\t\tif (list != null)");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tthis.rptList.DataSource = list;");
            sb.AppendLine("\t\t\t\tthis.rptList.DataBind();");
            sb.AppendLine("\t\t\t\tthis.noData.Visible = false;");
            sb.AppendLine("\t\t\t\ttotal = list[0].Total;");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("");
            sb.AppendLine("\t\t\tPaging paging = new Paging(\"./\", page, _size, _block, total);");
            sb.AppendLine("\t\t\tpaging.AddParams(\"sdate\", sdate);");
            sb.AppendLine("\t\t\t_paging = paging.ToString();");

            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 리스트 번호");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("protected string ListNumber(object obj, int index)");
            sb.AppendLine("{");
            sb.AppendLine("\tint page = Check.IsNone(Request[\"page\"], 1);");
            sb.AppendLine("\tint number = (Convert.ToInt32(obj) - _size * (page - 1) - index);");
            sb.AppendLine("\treturn number.ToString();");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            #endregion

            #region [ 조회 ]
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();
                string prefix = string.Empty;

                if (Check.IsIn(column, "_"))
                {
                    string[] array = column.Split('_');
                    for (int g = 0; g < array.Length; g++)
                        prefix += g == 0 ? array[g].ToLower() : Pascal(array[g]);
                }
                else
                    prefix = column.ToLower();

                sb.AppendLine("protected "+ Type(type) + " _" + prefix + " = string.Empty;");
            }
            sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\tif (!IsPostBack)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tPageLoad();");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 조회");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("private void PageLoad()");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tstring id = Check.IsNone(Request[\"id\"], true);");
            sb.AppendLine("\t\tModel." + _className + " " + _instance + " = null;");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t" + _instance + " = biz.Detail(id);");
            sb.AppendLine("\t\t\tif (" + _instance + " != null)");
            sb.AppendLine("\t\t\t{");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string prefix = string.Empty;
                if (Check.IsIn(column, "_"))
                {
                    string[] array = column.Split('_');
                    for (int g = 0; g < array.Length; g++)
                        prefix += g == 0 ? array[g].ToLower() : Pascal(array[g]);
                }
                else
                    prefix = column.ToLower();

                string type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();
                sb.AppendLine("\t\t\t\t _" + prefix + " = " + _instance + "." + Pascal(column) + ";");
            }
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\telse");
            sb.AppendLine("\t\t\t\tJS.Back(\"잘못된 접근입니다.\");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            #endregion

            #region [ 등록 ]
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 등록");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("protected void btnRegist_Click(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tModel." + _className + " " + _instance + " = new Model." + _className + "();");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string key = _ds.Tables[0].Rows[i]["KEY"].ToString();
                if (!key.Equals("IDENTITY"))
                {
                    if(key.Equals("PK"))
                        sb.AppendLine(string.Format("\t\t{0}.{1} = Tool.UniqueNewGuid;", _instance, Pascal(column)));
                    else
                        sb.AppendLine(string.Format("\t\t{0}.{1} = Element.Get(this.{2}{3});", _instance, Pascal(column), column.Substring(0, 1).ToLower(), Pascal(column).Substring(1, Pascal(column).Length - 1)));
                }
            }
            sb.AppendLine("");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tbool result = biz.Regist(" + _instance + ");");
            sb.AppendLine("\t\t\tif (result)");
            sb.AppendLine("\t\t\t\tTool.RR(\"./\");");
            sb.AppendLine("\t\t\telse");
            sb.AppendLine("\t\t\t\tJS.Back(\"처리중 에러가 발생했습니다.\");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            #endregion

            #region [ 수정 ]
            sb.AppendLine("protected void Page_Load(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\tif (!IsPostBack)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tPageLoad();");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 조회");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("private void PageLoad()");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tstring id = Check.IsNone(Request[\"id\"], true);");
            sb.AppendLine("\t\tModel." + _className + " " + _instance + " = null;");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t" + _instance + " = biz.Detail(gid);");
            sb.AppendLine("\t\t\tif (" + _instance + " != null)");
            sb.AppendLine("\t\t\t{");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string prefix = string.Empty;
                if (Check.IsIn(column, "_"))
                {
                    string[] array = column.Split('_');
                    for (int g = 0; g < array.Length; g++)
                        prefix += g == 0 ? array[g].ToLower() : Pascal(array[g]);
                }
                else
                    prefix = column.ToLower();

                string type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();
                //sb.AppendLine("\t\t\t\t _" + prefix + " = " + _instance + "." + Pascal(column) + ";");
                sb.AppendLine("\t\t\t\tElement.Set(this."+ prefix + ", " + _instance + "." + Pascal(column) + ");");
            }
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\telse");
            sb.AppendLine("\t\t\t\tJS.Back(\"잘못된 접근입니다.\");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 수정");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("protected void btnModify_Click(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tstring id = Check.IsNone(Request[\"id\"], true);");
            sb.AppendLine("\t\tModel." + _className + " " + _instance + " = new Model." + _className + "();");
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string key = _ds.Tables[0].Rows[i]["KEY"].ToString();
                if (!key.Equals("IDENTITY"))
                {
                    if (key.Equals("PK"))
                        sb.AppendLine(string.Format("\t\t{0}.{1} = id;", _instance, Pascal(column)));
                    else
                        sb.AppendLine(string.Format("\t\t{0}.{1} = Element.Get(this.{2}{3});", _instance, Pascal(column), column.Substring(0, 1).ToLower(), Pascal(column).Substring(1, Pascal(column).Length - 1)));
                }
            }
            sb.AppendLine("");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tbool result = biz.Modify(" + _instance + ");");
            sb.AppendLine("\t\t\tif (result)");
            sb.AppendLine("\t\t\t\tTool.RR(\"detail.aspx?id=\"+ id);");
            sb.AppendLine("\t\t\telse");
            sb.AppendLine("\t\t\t\tJS.Back(\"처리중 에러가 발생했습니다.\");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            #endregion

            #region [ 삭제 ]
            sb.AppendLine("/// &lt;summary&gt;");
            sb.AppendLine("/// " + _description + " 삭제");
            sb.AppendLine("/// &lt;/summary&gt;");
            sb.AppendLine("protected void btnDelete_Click(object sender, EventArgs e)");
            sb.AppendLine("{");
            sb.AppendLine("\ttry");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tstring id = Check.IsNone(Request[\"id\"], true);");
            sb.AppendLine("\t\tusing (Business." + _className + " biz = new Business." + _className + "(AppSetting.Connection))");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tbool result = biz.Delete(id);");
            sb.AppendLine("\t\t\tif (result)");
            sb.AppendLine("\t\t\t\tTool.RR(\"./\");");
            sb.AppendLine("\t\t\telse");
            sb.AppendLine("\t\t\t\tJS.Back(\"처리중 에러가 발생했습니다.\");");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("\tcatch (Exception ex)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tMLib.Util.Error.WebHandler(AppSetting.PathLog, AppSetting.ErrorSender, AppSetting.ErrorReceiver, ex);");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            #endregion

            #region [ 컨트롤 ]
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                string column = _ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                string key = _ds.Tables[0].Rows[i]["KEY"].ToString();
                string type = _ds.Tables[0].Rows[i]["DATA_TYPE"].ToString().ToUpper();
                int length = Convert.ToInt32(_ds.Tables[0].Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString());
                string name = column.Substring(0, 1).ToLower() + Pascal(column).Substring(1, Pascal(column).Length - 1);
                if (!key.Equals("IDENTITY"))
                {
                    if(type.Equals("UNIQUEIDENTIFIER"))
                        sb.AppendLine(string.Format("&lt;asp:TextBox ID=\"{0}\" runat=\"server\" MaxLength=\"36\"&gt;&lt;/asp:TextBox&gt;", name));
                    else if (type.Equals("VARCHAR"))
                        sb.AppendLine(string.Format("&lt;asp:TextBox ID=\"{0}\" runat=\"server\" MaxLength=\"{1}\"&gt;&lt;/asp:TextBox&gt;", name, length / 2));
                    else if (type.Equals("DATETIME"))
                        sb.AppendLine(string.Format("&lt;asp:TextBox ID=\"{0}\" runat=\"server\" MaxLength=\"10\"&gt;&lt;/asp:TextBox&gt;", name));
                    else
                        sb.AppendLine(string.Format("&lt;asp:TextBox ID=\"{0}\" runat=\"server\" MaxLength=\"{1}\"&gt;&lt;/asp:TextBox&gt;", name, length));
                }
            }
            #endregion

            Tool.Print(sb.ToString().Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
        }
        #endregion

        #region [ DataSet 만들기 ]
        private DataSet TableInfo()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();
                sb.AppendLine("SELECT A.TABLE_SCHEMA, A.TABLE_NAME, A.COLUMN_NAME, B.ColumnDescription AS [COLUMN_DESCRIPTION], A.ORDINAL_POSITION, A.DATA_TYPE,");
                sb.AppendLine("    CASE WHEN A.CHARACTER_MAXIMUM_LENGTH IS NULL");
                sb.AppendLine("        THEN ISNULL(A.NUMERIC_PRECISION, '')");
                sb.AppendLine("        ELSE A.CHARACTER_MAXIMUM_LENGTH");
                sb.AppendLine("    END AS CHARACTER_MAXIMUM_LENGTH,");
                sb.AppendLine("    A.IS_NULLABLE, ISNULL(A.COLUMN_DEFAULT, '') AS [DEFAULT_VALUE],");
                sb.AppendLine("    CASE");
                sb.AppendLine("        WHEN C.CONSTRAINT_NAME IS NULL THEN");
                sb.AppendLine("            CASE WHEN D.NAME IS NOT NULL THEN 'IDENTITY' END");
                sb.AppendLine("        ELSE LEFT(C.CONSTRAINT_NAME, 2)");
                sb.AppendLine("    END AS [KEY]");
                sb.AppendLine("FROM INFORMATION_SCHEMA.COLUMNS AS A WITH (NOLOCK)");
                sb.AppendLine("    INNER JOIN (");
                sb.AppendLine("        SELECT A.NAME AS TABLENAME , B.NAME AS COLUMNNAME ,C.VALUE AS COLUMNDESCRIPTION");
                sb.AppendLine("        FROM SYS.TABLES AS A WITH (NOLOCK)");
                sb.AppendLine("            INNER JOIN SYS.COLUMNS AS B WITH (NOLOCK) ON A.OBJECT_ID = B.OBJECT_ID");
                sb.AppendLine("            LEFT JOIN SYS.EXTENDED_PROPERTIES AS C WITH (NOLOCK) ON A.OBJECT_ID = C.MAJOR_ID AND B.COLUMN_ID = C.MINOR_ID");
                sb.AppendLine("    ) AS B ON A.TABLE_NAME = B.TABLENAME AND A.COLUMN_NAME = B.COLUMNNAME");
                sb.AppendLine("    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS C ON A.COLUMN_NAME = C.COLUMN_NAME AND C.TABLE_NAME = A.TABLE_NAME");
                sb.AppendLine("    LEFT OUTER JOIN SYSCOLUMNS D ON D.ID = OBJECT_ID(A.TABLE_NAME) AND A.COLUMN_NAME = D.NAME AND D.COLSTAT & 1 = 1");
                sb.AppendLine("WHERE A.TABLE_NAME = '"+ _table +"'");
                sb.AppendLine("ORDER BY ORDINAL_POSITION ASC");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn))
                {
                    cmd.Parameters.AddWithValue("@TABLENAME", _table);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                    }
                }
            }

            return ds;
        }
        #endregion

        #region [ 함수 ]
        /// <summary>
        /// 프로시져 매개변수 셋팅
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private string Parameter(DataRowCollection rows)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows.Count; i++)
            {
                string column = rows[i]["COLUMN_NAME"].ToString();
                string type = rows[i]["DATA_TYPE"].ToString().ToUpper();
                string length = rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString();
                string nullable = rows[i]["IS_NULLABLE"].ToString();
                string key = rows[i]["KEY"].ToString();
                string syntax = string.Empty;
                if (!key.Equals("IDENTITY"))
                {
                    if (nullable.Equals("YES"))
                        syntax = " = NULL";

                    string isnull = (length.Equals("0")) ? "" : "(" + length + ")";
                    if (type.Equals("INT") || type.Equals("TINYINT") || type.Equals("TEXT"))
                        isnull = "";

                    if (type.Equals("VARBINARY"))
                        sb.Append("\t@" + column + "\tVARCHAR(50)" + syntax);
                    else if (type.Equals("UNIQUEIDENTIFIER"))
                        sb.Append("\t@" + column + "\tVARCHAR(50)" + syntax);
                    else
                        sb.Append("\t@" + column + "\t" + type + isnull + syntax);

                    if (!i.Equals(rows.Count - 1))
                        sb.AppendLine(",");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// DB type => C# 타입으로 변환
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string Type(string type)
        {
            string rtn = string.Empty;
            type = type.ToUpper();
            if (type.Equals("INT") || type.Equals("TINYINT"))
            {
                rtn = "int";
            }
            else
            {
                rtn = "string";
            }

            return rtn;
        }

        /// <summary>
        /// underbar의 변수를 Pascal형태로 변환
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string Pascal(string column)
        {
            string[] array;
            string rtn = string.Empty;
            TextInfo text = new CultureInfo("en-US", false).TextInfo;

            if (Check.IsIn(column, "_"))
            {
                array = column.Split('_');
                foreach (string item in array)
                    rtn += text.ToTitleCase(item.ToLower());
            }
            else
                rtn = text.ToTitleCase(column.ToLower());

            return rtn;
        }

        private string Binding(string column, string type)
        {
            type = type.ToUpper();
            if (type.Equals("INT") || type.Equals("TINYINT"))
                return Pascal(column) + " = Convert.ToInt32(dr[\"" + column + "\"].ToString())";
            else if (type.Equals("UNIQUEIDENTIFIER"))
                return Pascal(column) + " = dr[\"" + column + "\"].ToString().ToUpper()";
            else
                return Pascal(column) + " = dr[\"" + column + "\"].ToString()";
        }
        #endregion
    }
}
