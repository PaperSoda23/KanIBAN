using System;
using System.Text;

namespace KanIBAN.API.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder RemoveLastNewLine(this StringBuilder sb, int from, int count)
        {
            return sb.Replace(Environment.NewLine, string.Empty, from, count);
        }
    }
}