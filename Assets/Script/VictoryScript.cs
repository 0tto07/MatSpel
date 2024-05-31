using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public Sceneloader Sceneloader;
    public FatassData data;
    public int fameModifier;
    int minMoneyGain = 100;
    int maxMoneyGain = 500;
    public int moneyGain;
    int minFollowerGain = 10;
    int maxFollowerGain = 100;
    public int followerGain;

    // Start is called before the first frame update
    void Start()
    {
        fameModifier = 1 + (data.followers / 1000);
        //Decide how much money is gotten upon win
        moneyGain = Random.Range(minMoneyGain, maxMoneyGain) * fameModifier;
        //Decide how many followers are obtained upon win
        followerGain = Random.Range(minFollowerGain, maxFollowerGain) * fameModifier;
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
        data.CurrentLevel += 1;
        data.health = false;
        data.energydrink = false;
        data.proteinshake = false;
        data.sake = false;
        data.disel = false;
        data.milk = false;
        Sceneloader.LoadScene("Market");
    }
}
