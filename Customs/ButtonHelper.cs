using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmaPosAdminProject.Customs
{
    public static class ButtonHelpers
    {
        //this ile HtmlHelper sınıfına ait olduğunu işaretliyoruz. Button Text i parametre olarak alıyoruz.
        public static MvcHtmlString Submit(this HtmlHelper helper, string text)
        {
            //Aynı kodların tekrarını önlemek amacı ile burda Diğer metodumuzu kullandık.
            return Submit(helper, text, null);
        }

        public static MvcHtmlString Submit(this HtmlHelper helper, string text, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("button")
            {
                InnerHtml = text
            };
            tagBuilder.MergeAttribute("type", "submit");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}