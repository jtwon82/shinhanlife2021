using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrangeSummer.Common.Master
{
    public class Paging
    {
        private string _target;
        private int _page;
        private int _size;
        private int _count;
        private int _block;
        private int _total;
        private int _start;
        private int _end;
        private string _render = string.Empty;
        private Dictionary<string, string> _dic = new Dictionary<string, string>();

        /// <summary>
        /// 페이징 함수
        /// </summary>
        /// <param name="target">페이지 URL</param>
        /// <param name="page">현재 페이지 번호</param>
        /// <param name="size">리스트에서 보여줄 ROW COUNT</param>
        /// <param name="block">페이징에서 보여줄 페이지 블럭 카운트</param>
        /// <param name="count">총 ROW COUNT</param>
        public Paging(string target, int page, int size, int block, int count)
        {
            _target = target;
            _page = page;
            _size = size;
            _block = block;
            _count = count;

            Init();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void Init()
        {
            _total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_count) / Convert.ToDouble(_size)));
            if (_page <= 0)
                _page = 1;

            if ((_page % _block) == 0)
            {
                _start = ((_page / _block) * _block + 1) - _block;
                _end = _start + (_block - 1);
            }
            else
            {
                _start = (_page / _block) * _block + 1;
                _end = _start + (_block - 1);
            }

            if (_end >= _total)
                _end = _total;
        }

        /// <summary>
        /// 파라메터 추가
        /// </summary>
        public void AddParams(string key, string value)
        {
            _dic.Add(key, value);
        }

        /// <summary>
        /// 페이징
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string param = Parameters();
            StringBuilder sb = new StringBuilder();
            if (_total > 0)
            {
                sb.AppendFormat("<nav>");
                sb.AppendFormat("  <ul class=\"pagination justify-content-center\">");
                sb.AppendFormat("    <li class=\"page-item{0}\">", _page == 1 ? " disabled" : "");
                sb.AppendFormat("      <a class=\"page-link\" href=\"{0}?page=1{1}\" tabindex=\"-1\">&lt;&lt;</a>", _target, param);
                sb.AppendFormat("    </li>");
                if (_start > _block)
                {
                    int pre = _start - _block;
                    sb.AppendFormat("    <li class=\"page-item\"><a class=\"page-link\" href=\"{0}?page={1}{2}\">&lt;</a></li>", _target, pre, param);
                }
                else
                {
                    sb.AppendFormat("    <li class=\"page-item disabled\"><a class=\"page-link\" href=\"javascript:;\">&lt;</a></li>");
                }

                for (int i = _start; i <= _end; i++)
                {
                    if (_page == i)
                        sb.AppendFormat("    <li class=\"page-item active\"><a class=\"page-link\" href=\"javascript:;\">{0}</a></li>", i);
                    else
                        sb.AppendFormat("    <li class=\"page-item\"><a class=\"page-link\" href=\"{0}?page={1}{2}\">{1}</a></li>", _target, i, param);
                }

                if (_total > _end)
                {
                    int next = _start + _block;
                    sb.AppendFormat("    <li class=\"page-item\"><a class=\"page-link\" href=\"{0}?page={1}{2}\">&gt;</a></li>", _target, next, param);
                }
                else
                {
                    sb.AppendFormat("    <li class=\"page-item disabled\"><a class=\"page-link\" href=\"javascript:;\">&gt;</a></li>");
                }

                if (_page == _total || _total == 0)
                    sb.AppendFormat("    <li class=\"page-item disabled\">");
                else
                    sb.AppendFormat("    <li class=\"page-item\">");
                sb.AppendFormat("      <a class=\"page-link\" href=\"{0}?page={1}{2}\">&gt;&gt;</a>", _target, _total, param);
                sb.AppendFormat("    </li>");
                sb.AppendFormat("  </ul>");
                sb.AppendFormat("</nav>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 파라메터 조합
        /// </summary>
        /// <returns></returns>
        private string Parameters()
        {
            string param = string.Empty;
            foreach (KeyValuePair<string, string> item in _dic)
            {
                param += "&" + item.Key + "=" + HttpUtility.UrlEncode(item.Value);
            }

            return param;
        }
    }
}
