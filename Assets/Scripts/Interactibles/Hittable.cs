using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public AudioClip hitAudioClip;
    [Range(0,1)]
    public float hitClipVolume = 1f;

    [Range(1,5)]
    public int hitsToDestroy = 1;

    private GameManager _gameManager;
    private ParticleSystem _particleSystem;
    private Animator _animator;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _particleSystem = GetComponent<ParticleSystem>();
        _animator = GetComponent<Animator>();
    }

    public void Hit(int damage)
    {
        _gameManager.GetSFXSource().PlayOneShot(hitAudioClip,hitClipVolume);
        hitsToDestroy -= damage;
        if(hitsToDestroy <= 0){
            ObjectDestroyed();
        }
    }

    void ObjectDestroyed()
    {
        Debug.Log("Destroyed");
        if(_particleSystem != null && _animator != null){
            _animator.SetTrigger("Break");
            GetComponent<BoxCollider2D>().enabled = false;
            _particleSystem.Play();
        }else{
            DisableObject();
        }
    }

    public void DisableSpriteRenderer(float delay)
    {
        StartCoroutine(DisableSpriteRendererCoroutine(delay));
    }

    IEnumerator DisableSpriteRendererCoroutine(float delay){
        yield return new WaitForSeconds(delay);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void DisableObject(){
        gameObject.SetActive(false);
    }
    public void EnableObject(){
        gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnParticleSystemStopped(){
        DisableObject();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.DisableMovement();
            pc.PlayerFailed();
        }
    }
}
