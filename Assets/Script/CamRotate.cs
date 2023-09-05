using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    PlayerControl pc;

    public float rotSpeed = 200f;

    float mx = 0;
    float my = 0;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("character").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);

        my = Mathf.Clamp(my, -90f, 90f);

        transform.eulerAngles = new Vector3(-my,mx,0);

        if(pc.hp <= 0)
        {
            this.gameObject.GetComponent<CamRotate>().enabled = false;
            this.gameObject.GetComponent<CamPosition>().enabled = false;
        }
    }
}
