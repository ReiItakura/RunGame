using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private SpriteRenderer sr = null;
    float speed = 3.0f;
    private bool vision = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //enemytimer = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //enemytimer += Time.deltaTime;

        if (sr.isVisible)
        {
            vision = true;
            // animator.SetBool("Move", true);
            //enemytimer = 0;
        }

        if (vision == true)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }

    }
}