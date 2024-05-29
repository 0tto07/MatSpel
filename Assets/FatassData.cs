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
    //increases money gain
    public int fameModifier;
    //how long you can go on an offensive without resting
    public float energy = 1;
    //how quickly you can move
    public float speed = 1;
    //decides your size and how difficult it is to be pushed
    public float weight = 1;
    //how well and quickly you can push enemies
    public float strength = 1;

    //Basic save stuff
    public string CurrentLevel;
    public int SFXvolume;
    public int Musicvolume;

    void Start()
    {
        
    }

    void Update()
    {
        fameModifier = (1 + (followers / 1000));
    }
}
