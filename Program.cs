using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KaiJsonToXml
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<String, String> translation = new Dictionary<string, string>();
			JsonSerializer serializer = new JsonSerializer();
			List<List<String>> fuck = JsonConvert.DeserializeObject<List<List<String>>>(File.ReadAllText(args[1], Encoding.UTF8));
			List<String> en = fuck[1];
			List<String> jp = fuck[2];
			for (int i = 0; i < en.Count; i++)
			{
				translation.Add(jp[i], en[i]);
			}
			StreamWriter log = new StreamWriter(File.Create("log.log"));
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			XmlDocument toTrans = new XmlDocument();
			toTrans.Load(new StreamReader(args[0], Encoding.UTF8, true));
			foreach (XmlElement element in toTrans.GetElementsByTagName("Name"))
			{
				try
				{
					Console.Write(element.InnerText + " -> ");
					Console.WriteLine(translation[element.InnerText]);
					log.WriteLine(element.InnerText + " -> ");
					log.Write(translation[element.InnerText]);
					if (translation[element.InnerText].Length > 0)
						element.InnerText = translation[element.InnerText];
				}
				catch (KeyNotFoundException e)
				{

					Console.WriteLine("Translation not found!");
				}
			}
			toTrans.Save(File.Create("testing.xml"));
			Console.ReadKey();
			log.Close();
		}
	}
}
