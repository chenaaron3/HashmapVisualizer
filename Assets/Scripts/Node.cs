using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : Operator
{
    public Rigidbody2D rb;
    public GameObject hashtag;
    public Text valueText;
    public Text hashText;
    string value;
    int hash;

    private void Start()
    {
        SetDirection(transform.right);
    }

    public override void OnTick()
    {
        // clip to the nearest position
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        if(node != null)
        {
            // if encounter same node while chaining
            if(value.Equals(node.GetValue()) && rb.velocity.magnitude > 0)
            {
                // remove node
                BuildManager.instance.numNodes--;
                Destroy(gameObject);
                // complete process
                Generator.instance.processing = false;
            }
        }
    }

    // sets the direction 
    public void SetDirection(Vector2 direction)
    {
        rb.velocity = TickManager.tickSpeed * direction;
    }

    // sets the value
    public void SetValue(string val)
    {
        value = val;
        valueText.text = val + "";
    }
    
    // gets the value
    public string GetValue()
    {
        return value;
    }

    // set the hash value
    public void SetHash(int val)
    {
        hashtag.SetActive(true);
        hash = val;
        hashText.text = val + "";
    }

    // get the hash value
    public int GetHash()
    {
        return hash;
    }

    // disables the hashtag
    public void HideHashtag()
    {
        hashtag.SetActive(false);
    }
}
