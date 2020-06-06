using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Projekt_programowanie
{
    class XmlScoreOperator
    {
        public void saveScore(Score score)
        {
            //jeśli plik nie istnieje -> stwórz go i zapisz score
            if (!File.Exists("score.xml"))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create("score.xml", xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Root");

                    xmlWriter.WriteStartElement("Score");
                    xmlWriter.WriteElementString("value", score.value.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            //jeśli plik istnieje
            {
                //załaduj dokument
                XDocument xDocument = XDocument.Load("score.xml");
                XElement root = xDocument.Element("Root");
                //iteracja w roocie po Score
                IEnumerable<XElement> rows = root.Descendants("Score");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("Score",
                   new XElement("value", score.value.ToString())));
                xDocument.Save("score.xml");
            }
        }
        public String readScore()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Root";
            xRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Score>), xRoot);
            List<Score> scores = new List<Score>();
            using (FileStream fileStream = File.OpenRead("score.xml"))
            {
                scores = (List<Score>)serializer.Deserialize(fileStream);
            }

            string text = null;
            foreach (Score score in scores)
            {
                text += score.value.ToString() + "\n";

            }
            return text;
        }
    }
}
