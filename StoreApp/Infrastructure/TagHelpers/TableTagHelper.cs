using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("table")]
    public class TableTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // sayfa içerisinde tanımlanan tabloları gördüğü anda otomatik olarak bootstrapin table-hover özelliğini entegre edecek
            output.Attributes.SetAttribute("class","table table-hover"); 
        }
    }
}