using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Operator
{
    public static Generator instance;

    Animator anim;
    public GameObject nodePrefab;
    // line for dispensing nodes
    List<Node> queue;
    // if a node is currently traversing through
    public bool processing;
    // if the line is moving
    bool consuming;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        queue = new List<Node>();
        anim = GetComponent<Animator>();
        anim.speed = TickManager.tickSpeed;
        processing = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
            int charAmount = Random.Range(1, 5);
            string myString = "";
            for (int i = 0; i < charAmount; i++)
            {
                myString += glyphs[Random.Range(0, glyphs.Length)];
            }
            GenerateNode(myString);
        }
    }

    public override void OnTick()
    {
        // spins wheel
        anim.SetTrigger("generate");

        // if requested and currently not processing and not rehashing
        if (queue.Count > 0 && !processing && !Rehash.instance.rehashing)
        {
            BuildManager.instance.numNodes++;
            // if should continue
            if (BuildManager.instance.CheckLoadFactor())
            {
                processing = true;
                // creates the node
                Node node = Consume();
                // pushes the node
                node.SetDirection(transform.right);
            }
            else
            {
                BuildManager.instance.numNodes--;
            }
        }
    }

    // assume that there is something to consume
    Node Consume()
    {
        consuming = true;
        Invoke("EndConsume", 1 / TickManager.tickSpeed);
        foreach(Node n in queue)
        {
            n.MoveUp();
        }
        Node node = queue[0];
        queue.RemoveAt(0);
        return node;
    }

    void EndConsume()
    {
        consuming = false;
    }

    // generates a node
    public void GenerateNode(string val)
    {
        if(!consuming)
        {
            // gets the position to spawn the node
            Vector2 pos = (Vector2)(queue.Count == 0 ? transform.position : queue[queue.Count - 1].transform.position) + Vector2.down;
            Node n = Instantiate(nodePrefab, pos, Quaternion.identity).GetComponent<Node>();
            n.SetValue(val);
            queue.Add(n);
        }
    }
}
