using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonToXml
{
    internal static class XmlService
    {
        enum tegs
        {
            first,
            open,
            close,
            full
        }
        public static void printXml(string xml)
        {
            int margin = 0;
            int delta = 2;
            tegs previous = tegs.first;
            tegs current;
            string line = "";
            for (int i = 0; i < xml.Length; i++)
            {
                line += xml[i];
                if (i + 1 < xml.Length && xml[i] == '>' && xml[i + 1] == '<' || i == xml.Length - 1)
                {
                    current = ClassificatorTegs(line);
                    margin += CalculateMargin(previous, current, delta);
                    Console.WriteLine(new string(' ', margin) + line);
                    previous = current;
                    line = "";
                }
            }
        }

        private static int CalculateMargin(tegs previous, tegs current, int delta)
        {
            if (current == tegs.first ||
                previous == tegs.full && current == tegs.full ||
                previous == tegs.full && current == tegs.open ||
                previous == tegs.first && current == tegs.open ||
                previous == tegs.close && current == tegs.open)
                return 0;
            else if (previous == tegs.open)
                return delta;
            else if (current == tegs.close)
                return -delta;
            throw new ArgumentException("Не валидный текст xml");
        }

        static tegs ClassificatorTegs(string line)
        {
            if (line.Contains("<?"))
                return tegs.first;
            else if (line.Contains("<") && !line.Contains("/")) // Открывающий тег
                return tegs.open;
            else if (Regex.IsMatch(line, @"<[^/].*<\/")) // Открывающий + закрывающий теги
                return tegs.full;
            else if (Regex.IsMatch(line, @"^([^<]*<\/[^<]*)$")) // Закрывающий тег
                return tegs.close;
            else
                throw new ArgumentException("Не валидный текст xml");
        }

    }
}
