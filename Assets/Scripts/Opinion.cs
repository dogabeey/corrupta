using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Opinion<T>
    {
        public static List<Opinion<T>> opinions = new List<Opinion<T>>();

        public T invidual;
        public T target;
        public float opinionValue;

        public Opinion(T i, T t, float val = 0)
        {
            invidual = i;
            target = t;
            opinionValue = val;

            // new Opinion<T>(t, i, val); // bir opinion oluşturulduğunda karşıdaki insanın da diğerine opinionu oluşuyor.
            opinions.Add(this);
        }

        static void SetOpinion(T i, T t, float val)
        {
            opinions.Find(o => o.invidual.ToString() == i.ToString() && o.target.ToString() == t.ToString()).opinionValue = val;
        }

        static Opinion<T> GetOpinion(T i, T t)
        {
            return opinions.Find(o => o.invidual.ToString() == i.ToString() && o.target.ToString() == t.ToString());
        }
    }