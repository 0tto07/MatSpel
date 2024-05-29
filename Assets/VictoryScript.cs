using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public FatassData data;
    int minMoneyGain = 100;
    int maxMoneyGain = 500;
    public int moneyGain;
    int minFollowerGain = 10;
    int maxFollowerGain = 100;
    public int followerGain;

    // Start is called before the first frame update
    void Start()
    {
        //Decide how much money is gotten upon win
        moneyGain = Random.Range(minMoneyGain, maxMoneyGain) * data.fameModifier;
        //Decide how many followers are obtained upon win
        followerGain = Random.Range(minFollowerGain, maxFollowerGain) * data.fameModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //increase money and followers when you win
    public void OnWin()
    {
        data.money += moneyGain;
        data.followers += followerGain;
    }
}