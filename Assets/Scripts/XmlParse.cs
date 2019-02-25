using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class XmlParse : MonoBehaviour
{

    static XmlDocument xml = new XmlDocument();

    public static void ParseIdeology(string fileName)
    {
        xml.Load("Assets\\XML\\" + fileName);

        XmlNodeList ideologies = xml.SelectNodes("/ideologies/ideology");
        foreach (XmlNode ideology in ideologies)
        {
            string idName = ideology["name"].InnerText;
            string idDesc = ideology["description"].InnerText;

            new Ideology(idName, idDesc);



        }
        foreach (XmlNode ideology in ideologies)
        {
            string idName = ideology["name"].InnerText;
            XmlNodeList idOpinions = ideology["opinions"].ChildNodes;
            for (int j = 0; j < idOpinions.Count; j++)
            {
                string tarName = idOpinions[j].Attributes["name"].Value;
                string tarOp = idOpinions[j].InnerText;

                Ideology inv = Ideology.ideologyList.Find(i => i.ideologyName.ToString() == idName);
                Ideology tar = Ideology.ideologyList.Find(t => t.ideologyName.ToString() == tarName);
                /* Problem aranan tarName, yani ideolojinin kod çalıştırma esnasında henüz yaratılmamş olmasından
                    * kaynaklanıyor. start() içinde başka bir foreach açıp bu for u oraya eklemek gerek. */
                new Opinion<Ideology>(inv, tar, (float)Convert.ToDouble(tarOp));
            }
        }

        /*foreach (Opinion<Ideology> i in Opinion<Ideology>.opinions)
        {
            Debug.Log("As the followers of " + i.invidual.ideologyName + ", we think around " + i.opinionValue + " against " + i.target.ideologyName);
        }*/
    }
    //public static void ParseCity(string fileName)
    //{
    //    xml.Load("Assets\\XML\\" + fileName);

    //    XmlNodeList cities = xml.SelectNodes("/cities/city");
    //    foreach (XmlNode city in cities)
    //    {
    //        string idName = city["name"].InnerText;
    //        string idDesc = city["description"].InnerText;

    //        new City(idName, idDesc);
    //    }
    //    foreach (XmlNode city in cities)
    //    {
    //        string idName = city["name"].InnerText;
    //        XmlNodeList idOpinions = city["demographys"].ChildNodes;
    //        //Debug.Log(idOpinions.Count);
    //        for (int j = 0; j < idOpinions.Count; j++)
    //        {
    //            string tarName = idOpinions[j].Attributes["name"].Value;
    //            string tarOp = idOpinions[j].InnerText;

    //            City inv = City.cityList.Find(i => i.cityName.ToString() == idName);
    //            Ideology tar = Ideology.ideologyList.Find(t => t.ideologyName.ToString() == tarName);

    //            inv.ideologyRates[j] = Eppy.Tuple.Create<Ideology, float>(tar, (float)Convert.ToDouble(tarOp));
    //        }

    //    }
    //    /*
    //    foreach (City c in City.cityList)
    //    {

    //        Debug.Log(c.cityName + ": " + c.description + ". ");
    //        Debug.Log(c.ideologyRates[0].Item2.ToString() + " percent of this city is " + c.ideologyRates[0].Item1.ideologyName + ".");
    //    }
    //    */
    //}

    public static string Serialize<T>(T dataToSerialize)
    {
        try
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, dataToSerialize);
            return stringwriter.ToString();
        }
        catch
        {
            throw;
        }
    }

    public static T Deserialize<T>(string xmlText)
    {
        try
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
        catch
        {
            throw;
        }
    }

    public static List<T> ImportAll<T>()
    {
        List<T> retVal = new List<T>();
        string[] directories = Directory.GetDirectories("Assets\\XML", typeof(T).ToString());
        if (directories.Length > 0)
        {
            string directory = directories[0];
            string[] files = Directory.GetFiles(directory, "*.xml");
            foreach (string file in files)
            {
                string fileContent = File.ReadAllText(file);
                T content = Deserialize<T>(fileContent);
                retVal.Add(content);
            }
            return retVal;
        }
        else return new List<T>();
    }

    public static void ExportAll<T>(List<T> list)
    {
        foreach (T item in list)
        {
            if (Directory.GetDirectories("Assets/XML", typeof(T).ToString()).Length == 0)
            {
                Directory.CreateDirectory("Assets\\XML\\" + typeof(T).ToString());
            }
            string directory = Directory.GetDirectories("Assets/XML", typeof(T).ToString())[0];
            string xmlText = Serialize(item);
            File.Create(directory + "/" + typeof(T).ToString() + "_" + list.FindIndex(t => t.GetHashCode() == item.GetHashCode()) + ".xml").Dispose();
            File.WriteAllText(directory + "/" + typeof(T).ToString() + "_" + list.FindIndex(t => t.GetHashCode() == item.GetHashCode()) + ".xml", xmlText);
        }
    }
}