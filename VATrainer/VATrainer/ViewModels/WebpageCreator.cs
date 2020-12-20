using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class WebpageCreator : IWebpageCreator
    {
        public string CreateWebpageForQuestion(Question question)
        {
            List<Article> articles = question.ArticleQuestions.Select(articleQuestion => articleQuestion.Article).ToList();
            return CreateWebpage(question.Text, articles, GetQuestionSpecificStyle());
        }

        public string CreateWebpageForAnswer(Answer answer)
        {
            List<Article> articles = answer.ArticleAnswers.Select(articleAnswer => articleAnswer.Article).ToList();
            return CreateWebpage(answer.Text, articles, GetAnswerSpecificStyle());
        }

        private string RemoveLineBreaks(string s)
        {
            string s1 = Regex.Replace(s, @"\t|\n|\r", "");
            return Regex.Replace(s1, "\"", "\\\"");
        }

        private string CreateWebpage(string text, ICollection<Article> articles, string specificStyle)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
            <!DOCTYPE html>
            <html>
	            <head>
		            <style>
			
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
                        }"
                    );

            sb.AppendLine(specificStyle);
            
            sb.AppendLine(@"
		            </style>
	            </head>
                <body>");
            
            sb.AppendLine(text);
            
            sb.AppendLine(@"
		            <div id=""myModal"" class=""modal"">
                        <div class=""modal-content"">
				            <span class=""close"">&times;</span>
				            <p id =""txt"">... Some text in the Modal ...</p>
			            </div>
		            </div>
		            <script>
		                function getArticle(id)
                        {");
            AddArticles(sb, articles);
            sb.AppendLine(@"
                        }

                        function showModal(clicked_id)
                        {
                            modal.style.display = ""block"";
                            document.getElementById(""txt"").innerHTML = getArticle(clicked_id);
                        }

                        // Get the modal
                        var modal = document.getElementById(""myModal"")

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
                        }
		            </script>
	            </body>
            </html>");
            return sb.ToString();
        }

        private void AddArticles(StringBuilder sb, ICollection<Article> articles)
        {
            foreach (Article article in articles)
            {
                string s = "if (id == " + article.Id.ToString() + ") return " + "\"" + RemoveLineBreaks(article.Text) + "\"";
                sb.AppendLine(s);
            }
        }

        private string GetQuestionSpecificStyle()
        {
            return @"

                html {
                    background-color: #F0FFFF
                }

                p {
                    padding: 20px;
                    font-family: Calibri;
                    font-size: large;
                    color: #191970;
                }

                ";
        }

        private string GetAnswerSpecificStyle()
        {
            return @"

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

                ";
        }
    }
}
