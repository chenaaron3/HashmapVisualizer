using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator instance;

    public GameObject nodePrefab;
    bool generate;
    string value;
    public bool processing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        processing = false;
    }

    private void OnEnable()
    {
        TickManager.OnTick += OnTick;
    }

    private void OnDisable()
    {
        TickManager.OnTick -= OnTick;
    }

    void OnTick()
    {
        // if requested and currently not processing and not rehashing
        if (generate && !processing && !Rehash.instance.rehashing)
        {
            BuildManager.instance.numNodes++;
            // if should continue
            if (BuildManager.instance.CheckLoadFactor())
            {
                processing = true;
                generate = false;
                // creates the node
                Node node = Instantiate(nodePrefab).GetComponent<Node>();
                // sets the position, velocity, and value of node
                node.transform.position = transform.position;
                node.SetDirection(transform.right);
                node.SetValue(value);
            }
            else
            {
                BuildManager.instance.numNodes--;
            }
        }
    }

    // generates a node at the next tick
    public void GenerateNode(string val)
    {
        generate = true;
        value = val;
    }
}
