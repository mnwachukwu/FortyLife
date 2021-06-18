﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FortyLife.Core
{
    /// <summary>
    /// Even though the official Markdown specification doesn't include ~~text~~ for strikethrough, I strongly feel it should be included.
    /// This helper will parse ~~text~~ to <del>text</del>. It is a deviation from the spec.
    /// </summary>
    public class MarkdownHelper
    {
        private const string TildePattern = "(~~)(.*)(~~)";
        private const string TildeReplacePattern = "<del>$2</del>";
        private const string ScriptPattern = "(<script>)(.*)(</script>)";
        private const string StylePattern = "(<style>)(.*)(</style>)";

        public string Transform(string rawText)
        {
            return !string.IsNullOrEmpty(rawText) ? Regex.Replace(rawText, TildePattern, TildeReplacePattern, RegexOptions.Multiline) : string.Empty;
        }

        public string RemoveProhibitedTags(string rawText)
        {
            var newText = Regex.Replace(rawText, ScriptPattern, string.Empty, RegexOptions.Multiline);
            newText = Regex.Replace(newText, StylePattern, string.Empty, RegexOptions.Multiline);
            newText = newText.Replace("<style>", string.Empty).Replace("<style", string.Empty).Replace("</style>", string.Empty);
            newText = newText.Replace("<script>", string.Empty).Replace("</script>", string.Empty);

            return newText;
        }
    }
}
