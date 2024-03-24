using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneBattery : MonoBehaviour
{
    public float batteryCapacity;
    [SerializeField] private float battery;
    [SerializeField] private float chargeSpeed;

    [SerializeField] private Follower followerScript;
    [SerializeField] private Slider slider;
    [SerializeField] private WeaponShoot weaponShoot;
    [SerializeField] private Animator animator;


    private bool isPlayerSide = true;
    private bool isAwake = true;

    void Start()
    {
        battery = batteryCapacity;
    }


    void Update()
    {
        if (battery > 0 && isAwake)
        {
            battery -= chargeSpeed * Time.deltaTime;
            if (battery <= 0)
                GoSleep();
        }
        slider.value = battery / batteryCapacity;
    }
    void GoSleep()
    {
        isAwake = false;
        followerScript.enabled = false;
        weaponShoot.enabled = false;
        animator.SetTrigger("sleep");
    }

    void GetUp()
    {
        isAwake = true;
        animator.SetTrigger("getUp");
        followerScript.enabled = true;
        weaponShoot.enabled = true;
    }

    public void AddBattery(float value)
    {
        battery = Mathf.Min(batteryCapacity, battery + value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerSide = true;
            if (!isAwake && battery > 0)
            {
                GetUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerSide = false;
        }
    }
}
