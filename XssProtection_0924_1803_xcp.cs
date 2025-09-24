// 代码生成时间: 2025-09-24 18:03:28
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorXssProtection
{
    /// <summary>
    /// Provides utility methods to protect against XSS attacks.
    /// </summary>
    public class XssProtectionService
    {
        /// <summary>
        /// Sanitizes input to prevent XSS attacks.
        /// </summary>
        /// <param name="input">The input to sanitize.</param>
        /// <returns>A sanitized string.</returns>
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                // Return empty string for null or whitespace input
                return string.Empty;
            }

            // Use a regular expression to remove any HTML tags
            // This is a very basic example, real-world applications should
            // use a more comprehensive approach or a library to handle XSS
            string sanitizedInput = Regex.Replace(input, "<[^>]*>.*?|<[^>]*>|<.*?/>"