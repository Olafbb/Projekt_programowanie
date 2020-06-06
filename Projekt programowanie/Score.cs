using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace Projekt_programowanie
{
    //klasa do serializowania/deserializowania wyników
    [Serializable()]
    public class Score : ISerializable
    {
        public int value { get; set; }
        public Score() { }

        public Score(int score)
        {
            this.value = score;
        }
        //serializacja mapa
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("value", value);
        }
        //deserializacja -> przypisywanie wartosci do zmiennych
        public Score(SerializationInfo info, StreamingContext context)
        {
            value = (int)info.GetValue("value", typeof(int));
        }
    }
}
