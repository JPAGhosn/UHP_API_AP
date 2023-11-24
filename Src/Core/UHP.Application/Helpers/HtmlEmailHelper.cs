using System;
using System.Text;

namespace UHP.Application.Helpers
{
    public static class HtmlEmailHelper
    {
        public static string HtmlPrescriptionEmail(string dr, string patient)
        {
            var html = new StringBuilder();
            html.Append(@"<!DOCTYPE html>" +
                        "<html>" +
                        "<head>" +
                        "<meta charset='utf-8'/>" +
                        "<title>Prescription</title>" +
                        "<style>" +
                        "</style>" +
                        "</head>" +
                        "<body>");

            html.Append(
                "<div style=\"background-color: white; border: 1px solid #e2e8f0; border-radius: 6px;\">");
            html.Append($"<img src=\"cid:qrcodeImage\" width=\"220\" height=\"220\">");
            html.Append("<div style =\"flex: 1 1 auto; padding: 1rem 1rem;\">");
            html.Append($"<h5>{DateTime.Now:d}</h5>");
            html.Append("<div>");
            html.Append($"<h4> Patient: {patient} </h4>");
            html.Append($"<h4> Doctor: {dr}</h4 >");
            html.Append("</div>");
            html.Append("</div>");
            html.Append("</div>");
            html.Append("</body>");
            html.Append("</html>");

            return html.ToString();
        }
    }
}