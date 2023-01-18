namespace Skolplattformen.ElevApp.SkolplattformenApi.Utils
{
    internal static class HtmlCleaner
    {

        // Simple HTML cleaner
       public static string CleanHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }
            html = html.Replace("\r\n", string.Empty);
            html = html.Replace("</p>", Environment.NewLine);
            html = html.Replace("<br>", Environment.NewLine);

            // Remove html tags
            html = System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);

           // Decode html entities
            html = System.Net.WebUtility.HtmlDecode(html);

            return html.Trim();
        }
    }
}
