using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EveryDayNick
{
    internal class Note
    {
        public string name;
        public string description;
        public DateTime? date;

        public Note(string Name, string Description, DateTime? Date)
        {
            name = Name;
            description = Description;
            date = Date;
        }

        public static void Serialize<T>(List<T> notes)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notesInfo.json", $"\r\n{json}");
        }


        public static List<T> Deserialization<T>()
        {
            string name= File.ReadAllText("name");
            List<T> notes = JsonConvert.DeserializeObject<List<T>>(name);
            return notes;
        }


    }
}