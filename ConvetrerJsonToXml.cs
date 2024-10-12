using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonToXml
{
    internal static class ConvetrerJsonToXml
    {
        static string XmlString { get; set; }
        static int Index = 0;
        // В произвольном json могут встречаться форматирующие символы.
        // По хорошему их нужно обрабатывать и пропускать
        // но я не успел.. :(
        //const string FormatSymbols = " \t\r\n\f";
        static Stack<string> EndXmlStringStack { get; set; } = new Stack<string>();
        internal static string ConvertJsonToXml(string jsonString)
        {
            Index = 0;
            if (jsonString.Substring(0, 2) == "{\"")
            {
                XmlString = "<?xmlversion=\"1.0\"encoding=\"utf-8\"?>";
                XmlString += "<root>";
                EndXmlStringStack.Push("</root>");
                Index++;
                XmlString += GetObject(jsonString, ref Index);
                XmlString += EndXmlStringStack.Pop();
            }
            else
                throw new ArgumentException("Oшибка в формате документа json!\nДокумент должен начинаться с \"{{{\"");
            return XmlString;
        }
        private static string GetObject(string jsonString, ref int Index) // Вызывается после "{"
        {
            string line = "";
            do
            {
                string key = GetKey(jsonString, ref Index); // Получаем ключ
                line += "<" + key + ">";
                EndXmlStringStack.Push("</" + key + ">");
                if (jsonString[Index++] == ':')         // Получаем значение
                {
                    line += GetValue(jsonString, ref Index);
                    line += EndXmlStringStack.Pop();
                }
                else
                    throw new ArgumentException("Oшибка в формате документа json!\nПосле ключа ожидалось  \":\"");
            } while (jsonString[Index++] == ',');
            return line;
        }
        private static string GetKey(string jsonString, ref int Index)
        {
            return GetString(jsonString, ref Index);
        }
        private static string GetValue(string jsonString, ref int Index)
        {
            string line = "";
            if (jsonString[Index] == '"') // текствовое значение
                line = GetString(jsonString, ref Index);
            else if (jsonString[Index] == '{') // Объект
            {
                Index++;
                line = GetObject(jsonString, ref Index);
            }
            else if (jsonString[Index] == '[') // массив
                line = GetArray(jsonString, ref Index);
            else // НЕ текстовое значение
                line = GetNotString(jsonString, ref Index);
            return line;
        }
        private static string GetArray(string jsonString, ref int index)
        {
            Index++;
            string line = "";
            if (jsonString[Index] == '{')  // Массив объектов
                do
                {
                    Index++;
                    line += "<record>";
                    EndXmlStringStack.Push("</record>");
                    line += GetObject(jsonString, ref Index);
                    line += EndXmlStringStack.Pop();
                } while (jsonString[Index++] == ',');
            else // Массив значений
                do
                {
                    line += "<record>";
                    EndXmlStringStack.Push("</record>");
                    line += GetValue(jsonString, ref Index);
                    line += EndXmlStringStack.Pop();
                } while (jsonString[Index++] == ',');
            return line;
        }

        private static string GetNotString(string jsonString, ref int index)
        {
            string line = "";
            while (jsonString[Index] != ',' && jsonString[Index] != '}' && jsonString[Index] != ']')
            {
                line += jsonString[Index];
                if (Index++ >= jsonString.Length)
                    throw new ArgumentException("Oшибка в формате документа json!\nПри формировании строки не удалось найти закрыающей кавычки!");
            }
            return line;
        }
        private static string GetString(string jsonString, ref int Index)
        {
            string line = "";
            Index++;
            while (jsonString[Index] != '"')
            {
                line += jsonString[Index];
                if (Index++ >= jsonString.Length)
                    throw new ArgumentException("Oшибка в формате документа json!\nПри формировании строки не удалось найти закрыающей кавычки!");
            }
            Index++;
            return line;
        }
    }
}
