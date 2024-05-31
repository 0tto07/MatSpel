using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    public int whatItem;
    public FatassData data;
    public TMP_Text text;
    int minPrice;
    int maxPrice;
    public int price;
    // Start is called before the first frame update
    void Start()
    {
        //Check what drink is being purchased and their price range
        switch (whatItem)
        {
            //energy drink
            case 0:
                {
                    minPrice = 300;
                    maxPrice = 1000;
                    break;
                }
            //soda
            case 1:
                {
                    minPrice = 300;
                    maxPrice = 1000;
                    break;
                }
            //protein
            case 2:
                {
                    minPrice = 300;
                    maxPrice = 1000;
                    break;
                }
            //sake
            case 3:
                {
                    minPrice = 300;
                    maxPrice = 1000;
                    break;
                }
            //disel
            case 4:
                {
                    minPrice = 300;
                    maxPrice = 1000;
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
        //check if the player has enough money to purchase drink
        if (data.money >= price)
        {
            data.money -= price;
            //check what drink is being purchased and what that drink buffs
            switch (whatItem)
            {
                //energy drink
                case 0:
                    {
                        data.speed *= 8;
                        data.energydrink = true;
                        break;
                    }
                //soda
                case 1:
                    {
                        data.energy *= 13;
                        data.soda = true;
                        break;
                    }
                //protein drink
                case 2:
                    {
                        data.weight *= 50;
                        data.proteinshake = true;
                        break;
                    }
                //sake
                case 3:
                    {
                        data.strength *= 11;
                        data.sake = true;
                        break;
                    }
                //disel
                case 4:
                    {
                        data.weight *= 50;
                        data.disel = true;
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
