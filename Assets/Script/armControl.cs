using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armControl : MonoBehaviour
{
    Animator anim;
    AudioSource audiosSource;
    public AudioClip AimSound;
    public AudioClip ShootSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        audiosSource= this.GetComponent<AudioSource>();
    }


    void walk()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

    }

    void Aiming()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAiming", true);
            audiosSource.PlayOneShot(AimSound);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAiming", false);
            audiosSource.PlayOneShot(ShootSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();

    }
}
