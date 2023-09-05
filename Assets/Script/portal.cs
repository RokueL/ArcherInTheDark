using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public GameObject portall;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(portalSpawn(this.gameObject.transform));
    }

    IEnumerator portalSpawn(Transform dietrans)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(portall, dietrans.transform.position, this.gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
