using System;
using System.Text;
using System.Web.Mvc;
using ESN3.WebUI.Models;

namespace ESN3.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");



            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {

                TagBuilder li = new TagBuilder("li");

                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    li.AddCssClass("active");
                    
                }

                li.InnerHtml += tag.ToString();
                ul.InnerHtml += li.ToString();
            }
            
            result.Append(ul.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}



    //public static class PagingHelpers
    //{
    //    public static MvcHtmlString PageLinks(this HtmlHelper html,
    //                                          PagingInfo pagingInfo,
    //                                          Func<int, string> pageUrl)
    //    {
    //        StringBuilder result = new StringBuilder();
    //        for (int i = 1; i <= pagingInfo.TotalPages; i++)
    //        {
    //            TagBuilder tag = new TagBuilder("a");
    //            tag.MergeAttribute("href", pageUrl(i));
    //            tag.InnerHtml = i.ToString();
    //            if (i == pagingInfo.CurrentPage)
    //            {
    //                tag.AddCssClass("selected");
    //                tag.AddCssClass("btn-primary");
    //            }
    //            tag.AddCssClass("btn btn-default");
    //            result.Append(tag.ToString());
    //        }
    //        return MvcHtmlString.Create(result.ToString());
    //    }
    //}
