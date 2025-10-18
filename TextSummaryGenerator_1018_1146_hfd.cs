// 代码生成时间: 2025-10-18 11:46:14
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace BlazorApp
{
    // Component to generate text summary
    public partial class TextSummaryGenerator
    {
        [Parameter]
        public string InputText { get; set; }

        [Parameter]
        public string Summary { get; set; } = "";

        [Parameter]
        public int SummaryLength { get; set; } = 200;

        // Method to generate summary
        private void GenerateSummary()
        {
            if (string.IsNullOrWhiteSpace(InputText))
            {
                Summary = "Please enter text to generate summary.";
                return;
            }

            if (SummaryLength < 50)
            {
                Summary = "Summary length should be at least 50 characters.";
                return;
            }

            string processedText = PreprocessText(InputText);
            string summary = ExtractSummary(processedText, SummaryLength);
            Summary = summary;
        }

        // Preprocess text by removing HTML tags, special characters, and extra spaces
        private string PreprocessText(string text)
        {
            // Remove HTML tags
            text = Regex.Replace(text, "<[^>]+>|
", string.Empty);

            // Remove special characters and extra spaces
            text = Regex.Replace(text, @"[^a-zA-Z0-9\s]", string.Empty);
            text = Regex.Replace(text, @"(\s)+", @"$1");
            text = text.Trim();

            return text;
        }

        // Extract summary by selecting the most relevant sentences
        private string ExtractSummary(string text, int length)
        {
            string[] sentences = text.Split('.');
            List<string> summarySentences = new List<string>();
            StringBuilder summary = new StringBuilder();

            foreach (string sentence in sentences)
            {
                sentence =(sentence.Trim());
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    summary.Append(sentence + ". ");

                    if (summary.Length >= length)
                    {
                        // Trim the last period and space before returning
                        return summary.ToString().Substring(0, summary.Length - 2);
                    }
                }
            }

            return summary.ToString();
        }
    }
}