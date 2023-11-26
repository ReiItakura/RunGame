using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManeger1 : MonoBehaviour
{
    int spriteCount = 3;
    float rightOffset = 1.45f;
    float leftOffset = -0.4f;
    Transform bgTfm;
    SpriteRenderer mySpriteRndr;
    float width;
    // Start is called before the first frame update
    void Start()
    {
        bgTfm = transform;
        mySpriteRndr = GetComponent<SpriteRenderer>();
        width = mySpriteRndr.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myViewport = Camera.main.WorldToViewportPoint(bgTfm.position);

        if (myViewport.x < leftOffset)
        {
            bgTfm.position += Vector3.right * (width * spriteCount);
        }
        // 背景の回り込み(カメラがX軸マイナス方向に移動時)
        else if (myViewport.x > rightOffset)
        {
            bgTfm.position -= Vector3.right * (width * spriteCount);
        }
    }
}
