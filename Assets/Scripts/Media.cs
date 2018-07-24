using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Media
    {
        static List<Media> mediaList;

        string name;
        Person founder;
        Person boss;
        Ideology ideology;
        float subjectivity;
        enum mediaType { newspaper, tvChannel, blog }

        public Media(string name, Person founder)
        {
            this.name = name;
            this.founder = founder;
            boss = founder;
            ideology = boss.ideology;
        }
    }

