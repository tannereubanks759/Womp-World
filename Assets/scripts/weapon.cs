using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject player;
    public GameObject itemsPos;
    public float fireRate;
    private float nextFire;

    private bool lookingLeft;
    private float moveHorizontal;

    public AudioClip weaponClip;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = itemsPos.transform.position;
        lookingLeft = false;
        nextFire = Time.time;
        fireRate = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if(moveHorizontal > .1f)
        {
            lookingLeft = false;
        }
        if(moveHorizontal < -.1f)
        {
            lookingLeft = true;
        }

        if (Input.GetButtonDown("Jump") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            source.clip = weaponClip;
            source.Play();
            StartCoroutine(duration());
        }
        
    }
    private IEnumerator duration()
    {
        if (lookingLeft == false)
        {
            this.transform.position = player.transform.position + new Vector3(1, 0, 0);
            yield return new WaitForSeconds(.07f);
            this.transform.position = itemsPos.transform.position;
            
        }
        if(lookingLeft == true)
        {
            this.transform.position = player.transform.position + new Vector3(-1, 0, 0);
            yield return new WaitForSeconds(.07f);
            this.transform.position = itemsPos.transform.position;
            
        }

    }
}
