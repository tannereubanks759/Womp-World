using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public ParticleSystem endParticle;
    public GameObject player;
    public Vector3 playerPos;
    public Animator anim;
    public AudioClip concrete;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (playerPos.x >= 113.7 && endParticle.isPlaying == false)
        {
            StartCoroutine(EndGame());
        }

        if (player.transform.position.x > 111f)
        {
            anim.SetBool("isShut", true);
            source.clip = concrete;
            source.PlayOneShot(concrete, .04f);
        }
    }
    IEnumerator EndGame()
    {
        endParticle.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("End");
    }
    
}
