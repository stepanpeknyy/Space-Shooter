using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 0.05f;
    Material spaceMaterial;
    Vector2 offSet;
    // Start is called before the first frame update
    void Start()
    {
        spaceMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        spaceMaterial.mainTextureOffset += offSet*Time.deltaTime ; 
    }
}
