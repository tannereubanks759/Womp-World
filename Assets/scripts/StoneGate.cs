using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGate : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 111f)
        {
            anim.SetBool("isShut", true);
        }
    }
}
