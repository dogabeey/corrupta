using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PersonConditions
{
    public static bool HasIntrigue(Person person,int x)
    {
        return person.Intrigue >= x;
    }
}
