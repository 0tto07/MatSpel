using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarketScript : MonoBehaviour
{
    public int whatItem;
    public FatassData data;
    public TMP_Text text;
    int minPrice;
    int maxPrice;
    public int price;
    float inStat;
    float deStat;
    // Start is called before the first frame update
    void Start()
    {
        //Check what item is being purchased and their price range
        switch (whatItem)
        {
            //sushi
            case 0:
                {
                    minPrice = 100;
                    maxPrice = 250;
                    break;
                }
            //ramen
            case 1:
                {
                    minPrice = 170;
                    maxPrice = 400;
                    break;
                }
            //chankonade
            case 2:
                {
                    minPrice = 225;
                    maxPrice = 500;
                    break;
                }
            //turkey
            case 3:
                {
                    minPrice = 135;
                    maxPrice = 350;
                    break;
                }
            //eggs
            case 4:
                {
                    minPrice = 70;
                    maxPrice = 175;
                    break;
                }
            //mount eggverest
            case 5:
                {
                    minPrice = 750;
                    maxPrice = 2500;
                    break;
                }
        }
        //randomly generate a price within the price range
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
                //sushi
                case 0:
                    {
                        data.speed += 8;
                        data.energy -= 2;
                        break;
                    }
                //ramen
                case 1:
                    {
                        data.energy += 13;
                        data.strength -= 2;
                        break;
                    }
                //chankonade
                case 2:
                    {
                        data.weight += 16;
                        data.speed -= 3;
                        break;
                    }
                //turkey
                case 3:
                    {
                        data.strength += 11;
                        data.speed -= 3;
                        break;
                    }
                //eggs
                case 4:
                    {
                        data.strength += 6;
                        data.energy -= 2;
                        break;
                    }
                //mount eggverest
                case 5:
                    {
                        data.strength += 25;
                        data.weight += 7;
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
