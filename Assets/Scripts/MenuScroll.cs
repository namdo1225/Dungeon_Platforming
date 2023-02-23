using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    private RawImage texture;
    [SerializeField]
    private float xScroll;
    [SerializeField]
    private float yScroll;

    private float x = 0;
    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        texture = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        x += xScroll;
        y += yScroll;
        texture.uvRect = new Rect(x, y, 6, 6);

        if (x > 2 || x < -2)
            x = 0;
        if (y > 2 || y < -2)
            y = 0;
    }
}
