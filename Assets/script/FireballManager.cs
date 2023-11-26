using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public float FireSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(FireSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground"|| col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ghost" || col.gameObject.tag == "Magician")
        {
            this.gameObject.SetActive(false);
        }
    }
}
