using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatApplier : MonoBehaviour
{
    public FatassData data;
    public Rigidbody2D myRB = null;
    public float strengthMod;
    public float weightMod;
    public float energyMod;
    public float speedMod;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        strengthMod = data.strength / 10;
        weightMod = data.weight/100;
        energyMod = data.energy/100;
        speedMod = data.speed/100;
    }
}
