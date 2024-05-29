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
        price = Random.Range(minPrice, maxPrice);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = price.ToString();
    }



    public void ButtonPressed()
    {
        if (data.money >= price)
        {
            data.money -= price;
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
        else
        {
             return;
        }
    }


}
