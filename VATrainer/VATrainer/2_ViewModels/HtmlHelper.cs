﻿using System.Collections.Generic;
using System.Text;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class HtmlHelper : IHtmlHelper
    {
        private const string PrimaryColorDark = "#320b86";

        private const string SecondaryColorDark = "#008ba3";
        private const string SecondaryColorLight = "#62efff";
        
        private const string BackgroundColorLight = "#f5f5f5";
        
        private const string PaddingText = "10px";
        private const string PaddingBottomAnswer = "5px";

        public string BuildWebpage(string style, string body)
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

        public string BuildStyle(ISettings settings)
        {
            return @"<style>

                        /* The Modal */
			            .modal-background
			            {
						    display: none;
						    position: fixed;
						    top: 0;
						    left: 0;
						    height: 100%;
						    width: 100%;
			            }
						
						.modal-vertical-center {
						    display: table-cell;
						    vertical-align: middle;
						}
						
						.modal-content {
						    margin-left: auto;
						    margin-right: auto;
						    width: 80%;
                            max-height: 80%;
							padding: 20px;
						    background-color: " + BackgroundColorLight + @";
						    border: 1px solid " + PrimaryColorDark + @";
                            overflow: scroll;
						}

			            /* The Close Button (x)*/
			            .close
			            {
				            color: " + PrimaryColorDark + @";
				            float: right;
				            font-size: 28px;
				            font-weight: bold; 
			            }

			            /* The Link */
			            .link
			            {
				            color: " + SecondaryColorDark + @";
				            font-weight: bold;
			            }

                        /* The Textmarker */
                        .textmarker
                        {
                            background-color: " + SecondaryColorLight + @";
                        }

                        html {
                            background-color: " + BackgroundColorLight + @";
                            padding-left: " + PaddingText + @";
                            padding-right: " + PaddingText + @";
                        }

                        p, li {
                            font-family: Calibri;
                            font-size: " + settings.FontSize + @";
                            color: " + PrimaryColorDark + @";
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

        public string BuildBody(string htmlText, List<Article> articles)
        {
            return htmlText +
                    @"
                    <div id=""modal"" class=""modal-background"">
                        <div id=""verticalCenter"" class=""modal-vertical-center"">                    
                            <div class=""modal-content"">
				                <span class=""close"">&times;</span>
				                <p id=""txt""></p>
			                </div>
                        </div>
                    </div>
		            <script>"
                        + BuildJavaScript(articles) +
                    @"</script>";
        }

        public string FormatQuestionForContentView(string question)
        {
            return @"<div style=""font-weight:bold;"">" + question + "</div>";
        }

        public string FormatAnswerForContentView(string answer)
        {
            return @"<div style=""padding-bottom: " + PaddingBottomAnswer + @""";>" + answer + "</div>";
        }

        private static string BuildJavaScript(List<Article> articles)
        {
            return BuildArticleFunction(articles) +
                @"
                function showModal(clicked_id)
                {
                    modal.style.display = ""table"";
                    document.getElementById(""txt"").innerHTML = getArticle(clicked_id);
                }

                // Get the modal
                var modal = document.getElementById(""modal"")

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
                    if (event.target == verticalCenter)
			        {
                        modal.style.display = ""none"";
                    }
                }";
        }

        private static string BuildArticleFunction(IList<Article> articles)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function getArticle(id)");
            sb.AppendLine("{");
            foreach (Article article in articles)
            {
                sb.AppendLine($"if (id == {article.Id}) return \"{article.Text}\"");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
