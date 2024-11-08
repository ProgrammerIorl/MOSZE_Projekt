using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour



{

    public float scrollSpeed = 0.5f;
    private float offset;
    private Material Mat;


    // Start is called before the first frame update
    void Start()
    {
        Mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        Mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
