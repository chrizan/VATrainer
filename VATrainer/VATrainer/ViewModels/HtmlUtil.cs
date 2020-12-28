using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public static class HtmlUtil
    {
        public static string BuildWebpage(string style, string body)
        {
            return @"<!DOCTYPE html>
                    <html>
	                    <head>"
                            + style +
                      @"</head>
                        <body>"
                            + body +
                        @"</body>
                    </html>";
        }

        public static string BuildStyle()
        {
            return @"<style>

                        /* The Modal (background) */
			            .modal
			            {
				            display: none; /* Hidden by default */
				            position: fixed; /* Stay in place */
				            z-index: 1; /* Sit on top */
				            padding-top: 100px; /* Location of the box */
				            left: 0;
				            top: 0;
				            width: 100%; /* Full width */
				            height: 100%; /* Full height */
				            overflow: auto; /* Enable scroll if needed */
				            background-color: rgb(0, 0, 0); /* Fallback color */
				            background-color: rgba(0, 0, 0, 0.4); /* Black w/ opacity */
			            }

			            /* Modal Content */
			            .modal-content
			            {
				            background-color: #fefefe;
				            margin: auto;
				            padding: 20px;
				            border: 1px solid #888;
				            width: 80%;
			            }

			            /* The Close Button (x)*/
			            .close
			            {
				            color: #000;
				            float: right;
				            font-size: 28px;
				            font-weight: bold; 
			            }

			            /* The Link */
			            .link
			            {
				            color: blue;
				            font-weight: bold;
			            }

                        /* The Textmarker */
                        .textmarker
                        {
                            background-color: yellow;
                        }

                        html {
                            background-color: #F0FFFF
                        }

                        p, li {
                            font-family: Calibri;
                            font-size: medium;
                            color: #191970;
                        }

                        ul {
                            padding-left: 20px;
                        }

                        table {
                            border-collapse: collapse;
                        }

                        table, td, th {
                            border: 1px solid #104E8B;
                        }

                    </style>";
        }

        public static string BuildBody(string htmlText, string javaScript)
        {
            return htmlText +
                    @"
                    <div id=""Modal"" class=""modal"">
                        <div class=""modal-content"">
				            <span class=""close"">&times;</span>
				            <p id=""txt""></p>
			            </div>
		            </div>
		            <script>"
                        + javaScript +
                    @"</script>";
        }

        public static string BuildJavaScript(string articles)
        {
            return articles + 
                @"
                function showModal(clicked_id)
                {
                    modal.style.display = ""block"";
                    document.getElementById(""txt"").innerHTML = getArticle(clicked_id);
                }

                // Get the modal
                var modal = document.getElementById(""Modal"")

                // Get the <span> element that closes the modal
                var span = document.getElementsByClassName(""close"")[0];

                // When the user clicks on <span> (x), close the modal
                span.onclick = function()
		        {
                    modal.style.display = ""none""
                }

                // When the user clicks anywhere outside of the modal, close it
                window.onclick = function(event)
                {
                    if (event.target == modal)
			        {
                        modal.style.display = ""none"";
                    }
                }";
        }

        public static string BuildJavaScriptArticles(ICollection<Article> articles)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function getArticle(id){");
            foreach (Article article in articles)
            {
                string s = "if (id == " + article.Id.ToString() + ") return " + "\"" + RemoveLineBreaks(article.Text) + "\"";
                sb.AppendLine(s);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string RemoveLineBreaks(string s)
        {
            string s1 = Regex.Replace(s, @"\t|\n|\r", "");
            return Regex.Replace(s1, "\"", "\\\"");
        }
    }
}
