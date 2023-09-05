using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    GameManager GM;
    public Transform point1, point2, point3;
    GameObject parent;
    public GameObject[] objectRoom;
    public GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        int randomRoom = Random.Range(0, objectRoom.Length);
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        parent = transform.parent.gameObject;
        if (parent.name == "FirstRoom")
        {

        }
        else if(parent.name == "BossRoom")
        {
            Instantiate(Boss, transform.position, Quaternion.identity);
        }
        else
        {
            GM.MonsterSpawn(point1, point2, point3);
            GameObject obj = Instantiate(objectRoom[randomRoom],transform.position,Quaternion.identity);
            if (randomRoom == 0)
            {
                obj.name = "object1";
            }
            else if (randomRoom == 1) 
            {
                obj.name = "object2";
            }
            else if (randomRoom == 2)
            {
                obj.name = "object3";
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
