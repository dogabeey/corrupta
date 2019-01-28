using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Xml.Serialization;

// Her yeni efekt için bunları eklemek gerekiyor yoksa xmle dönüştürülemiyor
public abstract class Effect
{
    public Effect()
    {
    }
    public virtual void Execute()
    {

    }
}
