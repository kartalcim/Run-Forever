using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraManager : MonoBehaviour
{
    private Transform hedef;
    private Vector3 mesafe;
    void Start()
    {
        hedef = GameObject.FindGameObjectWithTag("Player").transform;
        mesafe = transform.position - hedef.position;
    }
    void LateUpdate()
    {
        Vector3 pozisyon = new Vector3(transform.position.x,transform.position.y,mesafe.z+hedef.position.z);
        transform.position = Vector3.Lerp(transform.position, pozisyon,0.6f);
    }
}
