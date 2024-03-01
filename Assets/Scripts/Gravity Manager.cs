using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public ArrayList Objects = new ArrayList();

    public void FixedUpdate(){
        foreach(GameObject obj in Objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            float dist = Vector3.Distance(Vector3.zero, obj.transform.position);
            rb.AddForce((-obj.transform.position).normalized * 750 / ((dist * dist)/100), ForceMode2D.Force);
        }
    }
}
