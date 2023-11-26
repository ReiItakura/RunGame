using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    private SpriteRenderer sr = null;
    public float nowPosi;
    //private bool vision = false;
    //bool vision = false;
    //public float ghostspeed;
    // public float time = 1 + Time.deltaTime;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        nowPosi = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.isVisible)
        {
            this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime, nowPosi + Mathf.PingPong(Time.time, 3.0f), this.transform.position.z);
        }
        
    }
}
