using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Operator
{
    public GameObject nodesBucket;

    public virtual void RehashNodes()
    {
    }

    public virtual bool CheckOverflow()
    {
        return false;
    }
}
