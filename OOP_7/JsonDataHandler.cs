using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OOP_7
{
    public class JsonDataHandler
    {
        private const string JsonFilePath = "data.json"; // Путь к файлу JSON

        public List<DataRow> LoadData()
        {
            if (File.Exists(JsonFilePath))
            {
                var jsonData = File.ReadAllText(JsonFilePath);
                return JsonConvert.DeserializeObject<List<DataRow>>(jsonData);
            }
            return new List<DataRow>(); // Возвращаем пустой список, если файл не существует
        }

        public void SaveData(List<DataRow> rowsData)
        {
            var jsonData = JsonConvert.SerializeObject(rowsData, Formatting.Indented);
            File.WriteAllText(JsonFilePath, jsonData);
        }
    }

    public class DataRow
    {
        public int Id { get; set; }
        public string TextField1 { get; set; }
        public string TextField2 { get; set; }
        public string TextField3 { get; set; }
        public string DayName { get; set; }
        public string SpecName { get; set; }
        public DateTime DateValue { get; set; }
    }
}