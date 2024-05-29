using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarketScript : MonoBehaviour
{
    public int whatItem;
    public FatassData data;
    public TMP_Text text;
    int minPrice = 100;
    int maxPrice = 300;
    public int price;
    float inStat;
    float deStat;
    // Start is called before the first frame update
    void Start()
    {
        //randomly generate a price between min price and max price
        price = Random.Range(minPrice, maxPrice);
    }

    // Update is called once per frame
    void Update()
    {
        //display the price
        text.text = price.ToString();
    }



    public void ButtonPressed()
    {
        //check if the player has enough money to purchase item
        if (data.money >= price)
        {
            data.money -= price;
            //check what item is being purchased and what that item buffs/debuffs
            switch (whatItem)
            {
                case 0:
                    {
                        data.speed += 10;
                        data.energy -= 3;
                        break;
                    }
                case 1:
                    {
                        data.energy += 10;
                        data.strength -= 3;
                        break;
                    }
                case 2:
                    {
                        data.weight += 10;
                        data.speed -= 3;
                        break;
                    }
                case 3:
                    {
                        data.strength += 10;
                        data.speed -= 3;
                        break;
                    }
            }
        }
        //if the player doesn't have enough money return
        else
        {
             return;
        }
    }


}
