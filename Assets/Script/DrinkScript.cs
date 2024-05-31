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
                    minPrice = 2000;
                    maxPrice = 10000;
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
                        data.energy *= 1.5f;
                        data.energydrink = true;
                        break;
                    }
                //health
                case 1:
                    {
                        data.weight *= 1.5f;
                        data.health = true;
                        break;
                    }
                //protein drink
                case 2:
                    {
                        data.strength *= 1.5f;
                        data.proteinshake = true;
                        break;
                    }
                //sake
                case 3:
                    {
                        data.strength *= 1.75f;
                        data.speed *= 1.15f;
                        data.weight *= 1.25f;
                        data.energy *= 1.4f;
                        data.sake = true;
                        if (data.milk == false)
                        {
                            data.weight /= 1.3f;
                            data.strength /= 1.25f;
                            data.speed /= 1.15f;
                        }
                        break;
                    }
                //disel
                case 4:
                    {
                        data.weight *= 1.7f;
                        data.disel = true;
                        if (data.milk == false)
                        {
                            data.weight /= 1.7f;
                        }
                        break;
                    }
                //milk
                case 5:
                    {
                        data.weight *= 1.3f;
                        data.strength *= 1.25f;
                        data.milk = true;
                        if (data.milk == false)
                        {
                            data.weight /= 1.3f;
                            data.strength /= 1.25f;
                        }
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
