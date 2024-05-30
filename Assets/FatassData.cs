using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SavedData", menuName = "OUR STUFF")]

public class FatassData : ScriptableObject
{
    //CURRENCY
    //used to buy food
    public int money = 0;
    //used to decide fameModifier
    public int followers = 0;
    //STATS
    //how long you can go on an offensive without resting
    public float energy;
    //how quickly you can move
    public float speed;
    //decides your size and how difficult it is to be pushed
    public float weight;
    //how well and quickly you can push enemies
    public float strength;

    //Basic save stuff
    public float CurrentLevel;
    public int SFXvolume;
    public int Musicvolume;

    void Start()
    {
        if (strength < 1)
        {
            strength = 1;
        }
        if (speed < 1)
        {
            speed = 1;
        }
        if (weight < 1)
        {
            weight = 1;
        }
        if (energy < 1)
        {
            energy = 1;
        }
    }

    void Update()
    {

    }
}
