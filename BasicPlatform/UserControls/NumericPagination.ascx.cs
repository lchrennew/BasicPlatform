using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BasicPlatform.UserControls
{
    public partial class NumericPagination : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPages();
        }

        void BindPages()
        {
            List<PageItem> pages = new List<PageItem>();
            if (LinkPattern != null)
            {
                pages.Add(new PageItem
                {
                    Text = FirstText,
                    Link = string.Format(LinkPattern, 1),
                    Disabled = CurrentPage <= 1,
                    Tip = FirstTip
                });
                pages.Add(new PageItem
                {
                    Text = PrevText,
                    Link = string.Format(LinkPattern, RangeStart - 1),
                    Disabled = !HasPrevRange,
                    Tip = PrevTip
                });

                for (long i = RangeStart; i <= RangeEnd; i++)
                {
                    pages.Add(new PageItem { Active = i == CurrentPage, Link = string.Format(LinkPattern, i), Text = string.Format(textPattern, i) });
                }

                pages.Add(new PageItem
                {
                    Text = NextText,
                    Link = string.Format(LinkPattern, RangeEnd + 1),
                    Disabled = !HasNextRange,
                    Tip = NextTip
                });
                pages.Add(new PageItem
                {
                    Text = LastText,
                    Link = string.Format(LinkPattern, TotalPages),
                    Disabled = CurrentPage >= TotalPages,
                    Tip = LastTip
                });
                rpt.DataSource = pages;
                rpt.DataBind();
            }
        }

        string textPattern = "{0}";
        public string TextPattern { get { return textPattern ?? "{0}"; } set { textPattern = value; } }
        public string LinkPattern { get; set; }
        string prevText = "<";
        string nextText = ">";
        string firstText = "<<";
        string lastText = ">>";
        string prevTip = "向前5页";

        /// <summary>
        /// 向前5页的提示
        /// </summary>
        [Description("向前5页的提示")]
        public string PrevTip
        {
            get { return prevTip; }
            set { prevTip = value; }
        }
        string nextTip = "向后5页";

        /// <summary>
        /// 向后5页的提示
        /// </summary>
        [Description("向后5页的提示")]
        public string NextTip
        {
            get { return nextTip; }
            set { nextTip = value; }
        }
        string firstTip = "第一页";

        /// <summary>
        /// 第一页的提示
        /// </summary>
        [Description("第一页的提示")]
        public string FirstTip
        {
            get { return firstTip; }
            set { firstTip = value; }
        }
        string lastTip = "最后页";

        /// <summary>
        /// 最后一页的提示
        /// </summary>
        [Description("最后一页的提示")]
        public string LastTip
        {
            get { return lastTip; }
            set { lastTip = value; }
        }

        /// <summary>
        /// 第一页的文本
        /// </summary>
        [Description("第一页的文本")]
        public string FirstText
        {
            get { return firstText; }
            set { firstText = value; }
        }

        /// <summary>
        /// 最后一页的文本
        /// </summary>
        [Description("最后一页的文本")]
        public string LastText
        {
            get { return lastText; }
            set { lastText = value; }
        }

        /// <summary>
        /// 向后5页的文本
        /// </summary>
        [Description("向后5页的文本")]
        public string NextText
        {
            get { return nextText; }
            set { nextText = value; }
        }

        /// <summary>
        /// 向前5页的文本
        /// </summary>
        [Description("向前5页的文本")]
        public string PrevText
        {
            get { return prevText; }
            set { prevText = value; }
        }


        public override bool Visible
        {
            get
            {
                return base.Visible && TotalPages > 1;
            }
            set
            {
                base.Visible = value;
            }
        }

        /// <summary>
        /// 分页项
        /// </summary>
        class PageItem
        {
            public string Text { get; set; }
            public string Link { get; set; }
            public bool Active { get; set; }
            public bool Disabled { get; set; }
            public string Tip { get; set; }
        }

        const long maxDisplay = 5;

        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        [Description("每页显示条数")]
        public long PageSize { get; set; }

        long currentPage;
        /// <summary>
        /// 当前页页码
        /// </summary>
        [Description("当前页页码")]
        public long CurrentPage { get { return currentPage < 1 ? 1 : currentPage > TotalPages ? TotalPages : currentPage; } set { currentPage = value; } }

        /// <summary>
        /// 是否有前5页
        /// </summary>
        public bool HasPrevRange { get { return CurrentPage > 5; } }

        /// <summary>
        /// 是否有后5页
        /// </summary>
        public bool HasNextRange { get { return CurrentPage < TotalPages - 4; } }

        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get { return TotalRecords == 0 ? 0 : (TotalRecords - 1) / PageSize + 1; } }

        /// <summary>
        /// 范围开始
        /// </summary>
        protected long RangeStart
        {
            get
            {
                long m = CurrentPage % 5;
                if (m == 0) return CurrentPage - 4;
                else return CurrentPage - m + 1;
            }
        }

        /// <summary>
        /// 范围结束
        /// </summary>
        protected long RangeEnd
        {
            get
            {
                long m = CurrentPage % 5;
                if (m == 0) return CurrentPage;
                else
                {
                    m = CurrentPage - m + 5;
                    return m < TotalPages ? m : TotalPages;
                }
            }
        }
    }
}