#pragma checksum "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8a67fb725f0a42858b3eb70378328d5dfa47f963"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_List), @"mvc.1.0.view", @"/Views/User/List.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "G:\工作文件\Project\Project.AdminWeb\Views\_ViewImports.cshtml"
using Project.AdminWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\工作文件\Project\Project.AdminWeb\Views\_ViewImports.cshtml"
using Project.AdminWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a67fb725f0a42858b3eb70378328d5dfa47f963", @"/Views/User/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd8235f53a92ed82aae19d2a7afc52166d0e17b1", @"/Views/_ViewImports.cshtml")]
    public class Views_User_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml"
  
    ViewData["Title"] = "List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>用户列表</h1>\r\n<a href=\"/user/exportexcel\">导出报表</a>\r\n<table>\r\n    <tr>\r\n        <td>姓名</td>\r\n        <td>操作</td>\r\n    </tr>\r\n");
#nullable restore
#line 13 "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml"
     foreach (var item in ViewBag.List)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 16 "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml"
           Write(item.Item2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td><a");
            BeginWriteAttribute("href", " href=\"", 283, "\"", 316, 2);
            WriteAttributeValue("", 290, "/user/create/", 290, 13, true);
#nullable restore
#line 17 "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml"
WriteAttributeValue("", 303, item.Item1, 303, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">编辑</a></td>\r\n        </tr>\r\n");
#nullable restore
#line 19 "G:\工作文件\Project\Project.AdminWeb\Views\User\List.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
