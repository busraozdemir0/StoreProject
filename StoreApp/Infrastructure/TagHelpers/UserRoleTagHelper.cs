using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("td",Attributes ="user-role")]
    public class UserRoleTagHelper:TagHelper
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [HtmlAttributeName("user-name")] // sayfa içeriisnde td alanına user-name şeklinde kullandığımız için burada da belirtiyoruz
        public String? UserName{get;set;}
        public UserRoleTagHelper(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user=await _userManager.FindByNameAsync(UserName); // kullanıcıyı bul
            TagBuilder ul=new TagBuilder("ul");

            var roles=_roleManager.Roles.ToList().Select(r=>r.Name); // rollerin adı
            foreach(var role in roles)
            {
                TagBuilder li=new TagBuilder("li");
                li.InnerHtml.Append($"{role}:{await _userManager.IsInRoleAsync(user,role)}"); // rol adıyla birlikte bu rolün o kullanıcıya ait olup olmadığı bilgisi (aitse True değilse False)
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }

    }
}