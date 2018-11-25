using System.Collections;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

    public class Ideology
    {
        public static List<Ideology> ideologyList = new List<Ideology>();

        public string ideologyName;
        public string description;
        public List<Opinion<Ideology>> opinionList = new List<Opinion<Ideology>>();

        public Ideology(string ideology, string desc)
        {
            ideologyName = ideology;
            description = desc;

            ideologyList.Add(this);
        }
    }
