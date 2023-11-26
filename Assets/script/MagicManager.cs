using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public GameObject fireball;
    GameObject fireballClone;
    float Insttimer=0;
    private SpriteRenderer sr = null;
    private bool vision = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Insttimer += Time.deltaTime;

        if (sr.isVisible)
        {
            vision = true;
        }


        if (Insttimer >= 2.0f && vision == true)
        {
            fireballClone = Instantiate(fireball, new Vector3(this.transform.position.x - 2.0f, this.transform.position.y - 0.5f, this.transform.position.z), this.transform.rotation);
            Insttimer = 0;
            Destroy(fireballClone, 3.0f);
        }
    }
}
