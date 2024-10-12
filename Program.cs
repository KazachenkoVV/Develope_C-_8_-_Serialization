// Напишите приложение, конвертирующее произвольный JSON в XML. Используйте JsonDocument.
using System.Text.RegularExpressions;

namespace JsonToXml
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filejson = "{\"Current\":{\"Time\":\"2023-06-18T20:35:06.722127+04:00\",\"Temperature\":29,\"Weathercode\":1,\"Windspeed\":2.1,\"Winddirection\":1}," +
                    "\"History\":[{\"Time\":\"2023-06-17T20:35:06.77707+04:00\",\"Temperature\":30,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}," +
                    "{\"Time\":\"2023-06-16T20:35:06.777081+04:00\",\"Temperature\":22,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}," +
                    "{\"Time\":\"2023-06-15T20:35:06.777082+04:00\",\"Temperature\":21,\"Weathercode\":4,\"Windspeed\":2.2,\"Winddirection\":1}]}";

            var filejson1 = "{\"Array\":{\"MyArray\":[\"text\",123,true]}," +
                    "\"Current\":{\"Time\":\"2023-06-18T20:35:06.722127+04:00\",\"Temperature\":29,\"Weathercode\":1,\"Windspeed\":2.1,\"Winddirection\":1}," +
                    "\"History\":[{\"Time\":\"2023-06-17T20:35:06.77707+04:00\",\"Temperature\":30,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}," +
                    "{\"Time\":\"2023-06-16T20:35:06.777081+04:00\",\"Temperature\":22,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}," +
                    "{\"Time\":\"2023-06-15T20:35:06.777082+04:00\",\"Temperature\":21,\"Weathercode\":4,\"Windspeed\":2.2,\"Winddirection\":1}]}";
            
            var filejson2 = "{\"person\":{\"name\":\"Alice\",\"age\":25,\"isEmployed\":true,\"address\":{\"city\":\"New York\",\"zipcode\":10001},\"phones\":[\"123-456-7890\",\"987-654-3210\"]}}";


            Console.WriteLine();

            /*
            Console.WriteLine("Тест печати XML\n");
            string xml = "<?xmlversion=\"1.0\"encoding=\"utf-8\"?><people><personname=\"Tom\"><company>Microsoft</company><age>37</age></person><personname=\"Bob\"><company>Google</company><age>41</age></person></people>";
            printXml(xml);
            */

            Console.WriteLine("Файл из домошнего задания:\n");
            string fileXml = ConvetrerJsonToXml.ConvertJsonToXml(filejson);
            XmlService.printXml(fileXml);
            
            Console.WriteLine("\n\nФайл из домошнего задания с добавлением массива с данными разных форматов:\n");
            XmlService.printXml(ConvetrerJsonToXml.ConvertJsonToXml(filejson1));

            Console.WriteLine("\n\nПроизвольлный файл из интернета:\n");
            fileXml = ConvetrerJsonToXml.ConvertJsonToXml(filejson2);
            XmlService.printXml(fileXml);
        }
    }
}
