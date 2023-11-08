using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers;

public class EmailTagHelper : TagHelper
{
    public string? Address { get; set; }
    public string? Content { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //base.Process(context, output);

        output.TagName = "a";
        output.Attributes.SetAttribute("href", "mailto:" + Address);
        output.Content.SetContent(Content);
    }
}
