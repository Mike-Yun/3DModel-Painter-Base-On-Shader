using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float outSpeed = 0.5f;
    public Vector3 rotateSpeed = new Vector3(0, 0, 0);
    private Vector3 oriPosition;
    public float distance = 0;
    public Vector3 rotation = new Vector3(0, 0, 0);
    
    void Start()
    {
        oriPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * outSpeed;
        rotation += Time.deltaTime * rotateSpeed;
        Quaternion quat = Quaternion.Euler(rotation);
        Vector3 forward = quat * Vector3.left;
        transform.localPosition = oriPosition + forward * distance;
    }
}
