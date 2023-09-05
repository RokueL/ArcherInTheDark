using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    PlayerControl pc;
    public int weaponPower = 5;
    MonsterControl mc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("character").GetComponent<PlayerControl>();
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13 || other.gameObject.layer == 14) 
        {
            mc = other.transform.GetComponent<MonsterControl>();
            mc.HitEnemy(weaponPower + (int)pc.damageAdd);
            Destroy(this.gameObject);
        }
    }
}
