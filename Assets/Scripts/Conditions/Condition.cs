using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public Condition()
    {
    }
    public virtual bool Execute()
    {
        return true;
    }
}