using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManeger : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPosition = Player.transform.position;

        this.transform.position = new Vector3(PlayerPosition.x+5, PlayerPosition.y + 4.0f,-10);
    }
}
