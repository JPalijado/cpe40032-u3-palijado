using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgound : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position;

        // Gets the middle part of the background sprite
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // Repeats the background
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
