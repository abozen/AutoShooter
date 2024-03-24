using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private float valueX;
    private float valueZ;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform root;


    void Update()
    {
        valueX = joystick.xAxis.value;
        valueZ = joystick.yAxis.value;

        float animationSpeed = Mathf.Max(Mathf.Abs(valueX), Mathf.Abs(valueZ));
        animator.SetFloat("Speed", animationSpeed);

        if (valueX != 0 || valueZ != 0)
        {
            Vector3 direction = new Vector3(valueX, 0, valueZ).normalized; // Joystick'ten gelen yönü normalleştir

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction); // Yeni rotasyonu oluştur

                // Objeyi yavaşça yeni rotasyona döndür
                root.rotation = targetRotation;//Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        transform.Translate(valueX * speed * Time.deltaTime, 0, valueZ * speed * Time.deltaTime);
    }
}
