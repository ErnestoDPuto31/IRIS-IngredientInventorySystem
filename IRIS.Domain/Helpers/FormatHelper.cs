using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IRIS.Domain.Helpers
{
    public static class FormatHelper {

        /// <summary>
        /// Converts PascalCase enum strings into readable UI strings (e.g., "DairyAndOils" -> "Dairy & Oils")
        /// </summary>
        public static string FormatCategoryName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName)) return "Unknown";

            // This splits PascalCase words. "DairyAndOils" -> "Dairy And Oils"
            string spaced = Regex.Replace(rawName, "([a-z])([A-Z])", "$1 $2");

            // Swap " And " for " & " to match your dashboard style
            return spaced.Replace(" And ", " & ");
        }
    }
}
