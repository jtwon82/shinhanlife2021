using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace MLib.Config
{
    public class Binding
    {
        #region [Web.config 셋팅 정보 컨트롤 바인딩]
        /// <summary>
        /// 코드 DropDownList에 바인딩
        /// </summary>
        /// <param name="group">코드그룹</param>
        /// <param name="control">바인딩할 DropDownList</param>
        /// <param name="value">선택될 값</param>
        public static void Set(string group, DropDownList control, string value)
        {
            SortedList sort = Section.List(group);
            control.DataSource = sort;
            control.DataValueField = "Key";
            control.DataTextField = "Value";
            control.DataBind();
            control.SelectedValue = value;
        }

        /// <summary>
        /// 코드 RadioButtonList에 바인딩
        /// </summary>
        /// <param name="group">코드그룹</param>
        /// <param name="control">바인딩할 RadioButtonList</param>
        /// <param name="value">선택될 값</param>
        public static void Set(string group, RadioButtonList control, string value)
        {
            SortedList sort = Section.List(group);
            control.DataSource = sort;
            control.DataValueField = "Key";
            control.DataTextField = "Value";
            control.DataBind();
            control.SelectedValue = value;
        }

        /// <summary>
        /// 코드 CheckBoxList에 바인딩
        /// </summary>
        /// <param name="group">코드그룹</param>
        /// <param name="control">바인딩할 CheckBoxList</param>
        /// <param name="value">선택될 값</param>
        public static void Set(string group, CheckBoxList control, string value)
        {
            SortedList sort = Section.List(group);
            control.DataSource = sort;
            control.DataValueField = "Key";
            control.DataTextField = "Value";
            control.DataBind();
            control.SelectedValue = value;
        }

        /// <summary>
        /// 코드 CheckBoxList에 바인딩
        /// </summary>
        /// <param name="group">코드그룹</param>
        /// <param name="control">바인딩할 CheckBoxList</param>
        /// <param name="value">선택될 값</param>
        public static void Set(string group, CheckBoxList control, string[] value)
        {
            SortedList list = Section.List(group);
            control.DataSource = list;
            control.DataValueField = "Key";
            control.DataTextField = "Value";
            control.DataBind();

            foreach (ListItem item in control.Items)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (item.Value == value[i].ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
