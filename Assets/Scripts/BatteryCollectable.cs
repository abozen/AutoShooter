using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryCollectable : Collectable
{
    [SerializeField] DroneBattery droneBattery;

    void Start()
    {
        this.value = 5;
    }


    public override void Collect()
    {
        droneBattery.AddBattery(value);
    }

}
