#pragma checksum "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f2b2617bc174db89b576bf2393802210143ca1e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Member_Views_Member_Index), @"mvc.1.0.view", @"/Areas/Member/Views/Member/Index.cshtml")]
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
#line 1 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
using System.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f2b2617bc174db89b576bf2393802210143ca1e9", @"/Areas/Member/Views/Member/Index.cshtml")]
    public class Areas_Member_Views_Member_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PagedList<DataRow>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    // DataTable
    <div class=""layui-fluid"">
        <div class=""layui-row layui-col-space15"">
            <div class=""layui-col-md12"">
                <div class=""layui-card"">

                    <div class=""layui-card-body layui-table-body layui-table-main"">
                        <table class=""layui-table layui-form"">
                            <thead>
                                <tr>
                                    <th>
                                        <input type=""checkbox"" lay-filter=""checkall""");
            BeginWriteAttribute("name", " name=\"", 686, "\"", 693, 0);
            EndWriteAttribute();
            WriteLiteral(@" lay-skin=""primary""><div class=""layui-unselect layui-form-checkbox"" lay-skin=""primary""><i class=""layui-icon layui-icon-ok""></i></div>
                                    </th>
                                    <th>头像</th>
                                    <th>姓名</th>
                                    <th>性别</th>
                                    <th>电话</th>
                                    <th>邮箱</th>
                                    <th>积分</th>
                                    <th>注册时间</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 31 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                 foreach (DataRow item in Model)
                          {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <tr>
                                    <td>
                                        <input type=""checkbox"" name=""id"" value=""1"" lay-skin=""primary""><div class=""layui-unselect layui-form-checkbox"" lay-skin=""primary""><i class=""layui-icon layui-icon-ok""></i></div>
                                    </td>
                                    <td><img style=""width:2rem;""");
            BeginWriteAttribute("src", " src=\"", 1830, "\"", 1852, 1);
#nullable restore
#line 37 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
WriteAttributeValue("", 1836, item["HeadImg"], 1836, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></td>\r\n                                    <td>");
#nullable restore
#line 38 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["fullName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 39 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["Sex"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 40 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["Mobile"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 41 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["Email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 42 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["Integral"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 43 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                                   Write(item["RegTime"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#nullable restore
#line 45 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                    ");
#nullable restore
#line 50 "G:\工作文件\Project\Project.AdminWeb\Areas\Member\Views\Member\Index.cshtml"
               Write(Html.PagedListPager(Model, page => Url.Action("Index", new { page }),
                    PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(
                    new PagedListRenderOptions {
                    MaximumPageNumbersToDisplay = 10,
                    DisplayPageCountAndCurrentLocation = true,
                    LinkToFirstPageFormat = "首页",
                    LinkToLastPageFormat = "尾页",
                    LinkToNextPageFormat = "下一页",
                    PageCountAndCurrentLocationFormat = "{1}页{2}条记录",
                    LinkToPreviousPageFormat = "上一页",
                    UlElementClasses = new[] { "pagination col-md-8" },
                    ContainerDivClasses = new[] { "row" } },
                    new AjaxOptions()
                    {
                    DataForm = "searchForm",
                    HttpMethod = "GET",
                    UpdateTargetId = "table"
                    }
                    )));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PagedList<DataRow>> Html { get; private set; }
    }
}
#pragma warning restore 1591
