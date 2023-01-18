using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public ParticleSystem endParticle;
    public GameObject player;
    public Vector3 playerPos;
    public Animator anim;
    public AudioClip concrete;
    public AudioSource source;
    public AudioSource musicSource;
    public AudioClip musicClip;
    public Slider musicVolume;
    private GameObject[] objects;
    public static float volume;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "music" && GameObject.FindGameObjectsWithTag("music").Length < 2)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
            DontDestroyOnLoad(this.gameObject);
            volume = musicVolume.value;
            musicSource.volume = volume;
        }
        else
        {
            volume = 1f;
        }
        if (this.gameObject.tag == "post")
        {
            DontDestroyOnLoad(this.gameObject);
        }
        objects = GameObject.FindGameObjectsWithTag("music");
    }
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag != "music" && this.gameObject.tag != "post")
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
        if(this.gameObject.tag == "music")
        {
            if(musicVolume.value != volume)
            {
                musicSource.volume = volume;
            }
        }
    }
    IEnumerator EndGame()
    {
        endParticle.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("End");
    }
}
