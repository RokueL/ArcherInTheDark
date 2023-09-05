using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Animator anim;
    CharacterController CC;
    AudioSource audioSource;
    SceneChange sceneChange;

    public GameObject DestroyPlayer1, DestroyPlayer2;
    public AudioClip walkSound;
    public AudioClip CoinPick;
    public Transform ShootPosition;
    public GameObject Arrow;
    public Transform point;
    GameObject arrow;
    public GameObject hitEffect;

    public float damageAdd;
    public int Score = 0;
    public int hp = 20;
    int maxHP = 20;
    public Slider hpSlider;
    float addPower = 5f;
    public float moveSpeed = 1.5f;
    float gravity = -20f;
    float yVelocity = 0;

    public Vector3 lastPosition;

    void walk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        CC.Move(dir * moveSpeed * Time.deltaTime);

        WalkSound();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
    }

    void WalkSound()
    {
        float walkDistance = 1f;
        if(Vector3.Distance(transform.position, lastPosition) > walkDistance)
        {
            audioSource.PlayOneShot(walkSound);
            lastPosition = transform.position;
        }
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
        if(hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }
    IEnumerator PlayHitEffect() 
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        hitEffect.SetActive(false);
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            damageAdd = 0;
        }
        if(Input.GetMouseButton(0))
        {
            damageAdd += 1f * Time.deltaTime;
            addPower += 10f * Time.deltaTime;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            
            arrow = Instantiate(Arrow, ShootPosition.position, ShootPosition.rotation);
            Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();
            arrowRigid.velocity = transform.forward * addPower;
            addPower = 5f;
        }
    }

    void Die()
    {
        if(hp <= 0)
        {
            Destroy(DestroyPlayer1);
            Destroy(DestroyPlayer2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        sceneChange = GameObject.Find("GameManager").GetComponent<SceneChange>();
        anim.SetBool("isAiming", false);
        hp = maxHP;
        lastPosition = transform.position;
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();

            if(Physics.Raycast(ray,out hitInfo, 2f, LayerMask.GetMask("Coin")) == true)
            {
                if(hitInfo.transform.gameObject.name == "GoldenCoin")
                {
                    Score += 10;
                }
                else if ( hitInfo.transform.gameObject.name == "SilverCoin")
                {
                    Score += 5;
                }
                audioSource.PlayOneShot(CoinPick);
                Destroy(hitInfo.transform.gameObject);
            }
            else if(Physics.Raycast(ray,out hitInfo,2f, LayerMask.GetMask("Portal")) == true)
            {
                sceneChange.EndSceneMove();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Score", Score);
        hpSlider.value = (float)hp / (float)maxHP;
        walk();
        Shoot();
        Die();
        Interaction();
    }
}
