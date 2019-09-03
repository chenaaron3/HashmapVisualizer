using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : Operator
{
    public LineRenderer lr;
    public Rigidbody2D rb;
    public GameObject hashtag;
    public Text valueText;
    public Text hashText;
    string value;
    int hash;
    Animator anim;

    public bool fromGenerator;
    public bool deleter;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = TickManager.tickSpeed;
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
                // send delete request
                if(deleter)
                {
                    GetComponentInParent<Bucket>().RemoveNode(node.gameObject);
                }
                // remove node
                Destroy(gameObject);
                // complete process
                Generator.instance.processing = false;
                ViewManager.instance.ResetCamera();
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
        anim.SetTrigger("tagin");
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
        anim.ResetTrigger("tagin");
        anim.SetTrigger("tagout");
    }

    // move to destination within a tick
    public void Teleport(GameObject destination)
    {
        float time = 1 / TickManager.tickSpeed * .9f;
        // stop movement
        SetDirection(Vector2.zero);
        // lerps to destination
        lr.gameObject.SetActive(true);
        StartCoroutine(MoveToPosition(destination.transform.position, time));
        // sets parent to destination
        transform.parent = destination.transform;
    }

    IEnumerator MoveToPosition(Vector3 target, float timeToMove)
    {
        // sets up trail
        lr.SetPosition(1, target);
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            lr.SetPosition(0, transform.position);
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, target, t);
            yield return null;
        }
        transform.position = target;
        lr.gameObject.SetActive(false);
    }

    public void MoveDirection(Vector3 direction)
    {
        StartCoroutine(MoveToPosition(transform.position + direction, 1 / TickManager.tickSpeed));
    }

    // update numNodes
    private void OnDestroy()
    {
        BuildManager.instance.numNodes--;
    }

    public void TransformDeleter()
    {
        deleter = true;
        transform.Find("Graphics").GetComponent<SpriteRenderer>().color = Color.red;
    }
}
