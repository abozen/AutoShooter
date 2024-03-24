using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public int value;
    public float rotateSpeed;
    public abstract void Collect();


    private void Update()
    {
        RotateAround();
    }

    private void RotateAround()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Collect();
            gameObject.SetActive(false);
        }
    }
}
