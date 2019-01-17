using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingballscript : MonoBehaviour {

    private System.Random rand = new System.Random();
    public float speed;
    private Vector2 dir;
	// Use this for initialization
	void Start () {
        dir = new Vector2(rand.Next(-1, 1), rand.Next(-1, 1));
        while(dir == new Vector2(0,0))
        {
            dir = new Vector2(rand.Next(-1, 1), rand.Next(-1, 1));
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((dir * speed) * Time.deltaTime, Space.World);
	}
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "p1")
        {
            if (collision.collider == collision.gameObject.GetComponents<Collider>()[0])
                dir = new Vector2(1, 1);
            if (collision.collider == collision.gameObject.GetComponents<Collider>()[1])
                dir = new Vector2(1, -1);
            if (collision.collider == collision.gameObject.GetComponents<Collider>()[2])
                dir = new Vector2(1, 0);
        }
       if(collision.gameObject.tag == "p2")
        {
            if(collision.collider == collision.gameObject.GetComponents<Collider>()[0])
                dir = new Vector2(-1, 1);
            if (collision.collider == collision.gameObject.GetComponents<Collider>()[1])
                dir = new Vector2(-1, -1);
            if (collision.collider == collision.gameObject.GetComponents<Collider>()[2])
                dir = new Vector2(-1, 0);
        }
    }
}
