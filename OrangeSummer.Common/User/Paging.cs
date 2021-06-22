using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrangeSummer.Common.User
{
    public class Paging
    {
        private string _target;
        private int _page;
        private string _addparam;
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
        public Paging(string target, int page, int size, int block, int count, string addparam)
        {
            _target = target;
            _page = page;
            _size = size;
            _block = block;
            _count = count;
            _addparam = addparam;

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
            string param = Parameters() + _addparam;
            StringBuilder sb = new StringBuilder();
            if (_total > 0)
            {
                sb.Append("<ul class=\"numbering\">");
                if (_start > _block)
                {
                    int pre = _start - _block;
                    sb.Append($"    <li><a href=\"{_target}?page={pre}{param}\" ><span class=\"hidden\">&lt</span></a><li>");
                }
                else
                    sb.Append($"    <li><a href=\"javascript:;\" ><span class=\"hidden\">&lt</span></a><li>");

                for (int i = _start; i <= _end; i++)
                {
                    if (_page == i)
                        sb.Append($"    <li><a href=\"javascript:;\" class=\"current\">{i.ToString()}</a></li>");
                    else
                        sb.Append($"    <li><a href=\"{_target}?page={i.ToString()}{param}\">{i.ToString()}</a><li>");
                }

                if (_total > _end)
                {
                    int next = _start + _block;
                    sb.Append($"    <li><a href=\"{_target}?page={next}{param}\" ><span class=\"hidden\">&gt;</span></a></li>");
                }
                else
                    sb.Append($"    <li><a href=\"javascript:;\" ><span class=\"hidden\">&gt;</span></a></li>");
                sb.Append("</ul>");


                
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
