using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawn : MonoBehaviour
{
    public GameObject[] Coin;
    public Transform[] obj1, obj2, obj3;
    int randomCoinAmount;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name == "object1")
        {
            randomCoinAmount = Random.Range(0, 8);   
            for(int i = 0; i<randomCoinAmount; i++)
            {
                int ranCoin = Random.Range(0, 2);
                GameObject coin = Instantiate(Coin[ranCoin], obj1[i].position, Quaternion.identity);
                if(ranCoin == 0)
                {
                    coin.name = "GoldenCoin";
                }
                else
                {
                    coin.name = "SilverCoin";
                }
            }
        }
        if (this.gameObject.name == "object2")
        {
            randomCoinAmount = Random.Range(0, 5);
            for (int i = 0; i < randomCoinAmount; i++)
            {
                int ranCoin = Random.Range(0, 2);
                GameObject coin = Instantiate(Coin[ranCoin], obj2[i].position, Quaternion.identity);
                if (ranCoin == 0)
                {
                    coin.name = "GoldenCoin";
                }
                else
                {
                    coin.name = "SilverCoin";
                }
            }
        }
        if (this.gameObject.name == "object3")
        {
            randomCoinAmount = Random.Range(0, 7);
            for (int i = 0; i < randomCoinAmount; i++)
            {
                int ranCoin = Random.Range(0, 2);
                GameObject coin = Instantiate(Coin[ranCoin], obj3[i].position, Quaternion.identity);
                if (ranCoin == 0)
                {
                    coin.name = "GoldenCoin";
                }
                else
                {
                    coin.name = "SilverCoin";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
