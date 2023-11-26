using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{
    public float speed = 300f;
    public float Jumppower = 4.0f;
    int jumpcount=0;
    private float jumptimer = 0;
    private float ghosttimer = 0;
    private float magictimer = 0;
    private BoxCollider2D box = null;
    public float stepOnRate;
    public AudioClip StepOnSound;
    public AudioClip JumpSound;
    public AudioClip ChangeSound;
    public AudioClip ChangeEndSound;
    public AudioClip ItemGetSound;
    public GameObject myfireball;
    GameObject myfireballClone;
    int childCount;
    Transform childTransform;
    GameObject childObject;
    GameObject[] enemys;
    //GameObject ghost2;
    //GameObject ghost;
    //private float jumpPos = 0.0f;
    //private float otherJumpHeight = 0.0f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        ghosttimer = -3.0f;
        jumptimer = -3.0f;
        magictimer = -3.0f;

        childCount = this.transform.childCount;
        //ghost2 = GameObject.Find("ghost2");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * Time.deltaTime * 2, 0,0);
        jumptimer -= Time.deltaTime;
        ghosttimer -= Time.deltaTime;
        magictimer -= Time.deltaTime;

        if(magictimer >= 0)
        {
            if (Input.GetKeyDown("up") == true)
            {
                myfireballClone = Instantiate(myfireball, new Vector3(this.transform.position.x + 1.2f, this.transform.position.y + 1.5f, this.transform.position.z), Quaternion.Euler(0,180,0));
                Destroy(myfireballClone, 3.0f);
            }
        }
        



        if (Input.GetKeyDown("space") == true && jumpcount < 2)
        {
            this.GetComponent<AudioSource>().clip = JumpSound;
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,Jumppower*4.5f);
            jumpcount += 1;
            animator.SetBool("Jump", true);
        }

        if (jumptimer <= 0 && jumptimer >=-1.0f)
        {
            Jumppower = 4.0f;
            jumptimer = -3.0f;
        }

        if(ghosttimer <= 0 && ghosttimer >= -1.0f)
        {
            this.GetComponent<AudioSource>().clip = ChangeEndSound;
            this.GetComponent<AudioSource>().Play();
            ghosttimer = -3.0f;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            foreach (GameObject enemy in enemys)
            {
                enemy.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            for (int i = 0; i < childCount - 2; i++)
            {
                childTransform = this.transform.GetChild(i);
                childObject = childTransform.gameObject;
                childObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            float stepOnHeight = box.size.y * (stepOnRate / 100f);
            float judgePos = this.transform.position.y - (box.size.y / 2) + stepOnHeight;

            foreach (ContactPoint2D p in col.contacts)
            {
                if (p.point.y < judgePos)
                {
                    this.GetComponent<AudioSource>().clip = StepOnSound;
                    this.GetComponent<AudioSource>().Play();
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Jumppower * 2.0f);
                }

                else
                {
                    
                    SceneManager.LoadScene("Gameover");
                }
            }
        }

        if (col.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Gameclear");
        }

        if (col.gameObject.tag == "Ghost")
        {
            ghosttimer = 10.0f;
            this.gameObject.layer = LayerMask.NameToLayer("Player");
            Debug.Log(this.gameObject.layer);
            //ghost2.gameObject.layer = LayerMask.NameToLayer("Ghost");
            //Debug.Log(ghost2.gameObject.layer);
            foreach (GameObject enemy in enemys)
            {
                enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
            float stepOnHeight = box.size.y * (stepOnRate / 100f);
            float judgePos = this.transform.position.y - (box.size.y / 2) + stepOnHeight;

            foreach (ContactPoint2D p in col.contacts)
            {
                if (p.point.y < judgePos)
                {
                    this.GetComponent<AudioSource>().clip = ChangeSound;
                    this.GetComponent<AudioSource>().Play();
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Jumppower * 2.0f);
                    for (int i = 0; i < childCount - 2; i++)
                    {
                        childTransform = this.transform.GetChild(i);
                        childObject = childTransform.gameObject;
                        childObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
                    }
                }

                else
                {
                    
                    SceneManager.LoadScene("Gameover");
                }

            }



        }


        if (col.gameObject.tag == "Ground")
        {
            jumpcount = 0;

            animator.SetBool("Jump", false);
        }

        if (col.gameObject.tag == "Jumpitem")
        {
            this.GetComponent<AudioSource>().clip = ItemGetSound;
            this.GetComponent<AudioSource>().Play();
            Jumppower += 1.5f;
            Destroy(col.gameObject);
            jumptimer = 10.0f;
        }

        if(col.gameObject.tag == "Enemy"||col.gameObject.tag == "Ghost"||col.gameObject.tag == "Magician")
        {
            col.gameObject.SetActive(false);
        }

        if(col.gameObject.tag == "Fireball")
        {
            SceneManager.LoadScene("Gameover");
        }

        if (col.gameObject.tag == "Magician")
        {
            magictimer = 10.0f;
            float stepOnHeight = box.size.y * (stepOnRate / 100f);
            float judgePos = this.transform.position.y - (box.size.y / 2) + stepOnHeight;

            foreach (ContactPoint2D p in col.contacts)
            {
                if (p.point.y < judgePos)
                {
                    this.GetComponent<AudioSource>().clip = StepOnSound;
                    this.GetComponent<AudioSource>().Play();
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Jumppower * 2.0f);
                }

                else
                {

                    SceneManager.LoadScene("Gameover");
                }
            }
        }

    }

 

    
    
}
