#pragma checksum "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "350458840698f956a62ad66534c1443893e496eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\_ViewImports.cshtml"
using ISOCS;

#line default
#line hidden
#line 2 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\_ViewImports.cshtml"
using ISOCS.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"350458840698f956a62ad66534c1443893e496eb", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0504d55931cf7868a8b16951e93c98a19f067e8d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoginModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
  
    Layout = "";

#line default
#line hidden
            BeginContext(44, 37, true);
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            EndContext();
            BeginContext(81, 126, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "84bedaaa077543b6ab9fe4a10707ad4e", async() => {
                BeginContext(87, 70, true);
                WriteLiteral("\r\n    <title>ISOCS</title>\r\n    <link rel=\"stylesheet\" type=\"text/css\"");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 157, "\"", 194, 1);
#line 10 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
WriteAttributeValue("", 164, Url.Content("~/css/site.css"), 164, 30, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(195, 5, true);
                WriteLiteral(" />\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(207, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(209, 961, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0c53eaa264ee425e93aab322411e1c16", async() => {
                BeginContext(215, 39, true);
                WriteLiteral("\r\n    <div id=\"MainLogo\">\r\n        <h3>");
                EndContext();
                BeginContext(255, 17, false);
#line 14 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
       Write(ViewData["Titel"]);

#line default
#line hidden
                EndContext();
                BeginContext(272, 21, true);
                WriteLiteral("</h3>\r\n    </div>\r\n\r\n");
                EndContext();
#line 17 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
     using (Html.BeginForm())
    {

#line default
#line hidden
                BeginContext(331, 50, true);
                WriteLiteral("        <div>\r\n            <div>\r\n                ");
                EndContext();
                BeginContext(382, 27, false);
#line 21 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.LabelFor(x => x.email));

#line default
#line hidden
                EndContext();
                BeginContext(409, 18, true);
                WriteLiteral("\r\n                ");
                EndContext();
                BeginContext(428, 29, false);
#line 22 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.TextBoxFor(x => x.email));

#line default
#line hidden
                EndContext();
                BeginContext(457, 18, true);
                WriteLiteral("\r\n                ");
                EndContext();
                BeginContext(476, 75, false);
#line 23 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.ValidationMessageFor(x => x.email, "", new { @style = "color: red;" }));

#line default
#line hidden
                EndContext();
                BeginContext(551, 57, true);
                WriteLiteral("\r\n            </div>\r\n            <div>\r\n                ");
                EndContext();
                BeginContext(609, 30, false);
#line 26 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.LabelFor(x => x.password));

#line default
#line hidden
                EndContext();
                BeginContext(639, 18, true);
                WriteLiteral("\r\n                ");
                EndContext();
                BeginContext(658, 33, false);
#line 27 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.PasswordFor(x => x.password));

#line default
#line hidden
                EndContext();
                BeginContext(691, 18, true);
                WriteLiteral("\r\n                ");
                EndContext();
                BeginContext(710, 42, false);
#line 28 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
           Write(Html.ValidationMessageFor(x => x.password));

#line default
#line hidden
                EndContext();
                BeginContext(752, 132, true);
                WriteLiteral("\r\n            </div>\r\n            <div>\r\n                <input type=\"submit\" value=\"Login\" />\r\n            </div>\r\n        </div>\r\n");
                EndContext();
#line 34 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
    }

#line default
#line hidden
                BeginContext(891, 19, true);
                WriteLiteral("    <div>\r\n        ");
                EndContext();
                BeginContext(910, 102, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b35f0bc74d534c72924a8071ddd4db5b", async() => {
                    BeginContext(969, 39, true);
                    WriteLiteral("Klik hier als je nog geen account hebt!");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1012, 14, true);
                WriteLiteral("\r\n    </div>\r\n");
                EndContext();
#line 38 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
     if (ViewData["Message"] != null)
    {

#line default
#line hidden
                BeginContext(1072, 39, true);
                WriteLiteral("        <div>\r\n            <p>Bericht: ");
                EndContext();
                BeginContext(1112, 19, false);
#line 41 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
                   Write(ViewData["Message"]);

#line default
#line hidden
                EndContext();
                BeginContext(1131, 23, true);
                WriteLiteral(" </p>\r\n        </div>\r\n");
                EndContext();
#line 43 "D:\GitKraken\Repos\ISOCS\ISOCS\Views\Home\Index.cshtml"
    }

#line default
#line hidden
                BeginContext(1161, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1170, 9, true);
            WriteLiteral("\r\n</html>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoginModel> Html { get; private set; }
    }
}
#pragma warning restore 1591