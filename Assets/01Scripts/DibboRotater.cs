using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DibboRotater : MonoBehaviour
{
    public float speed = 135f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
