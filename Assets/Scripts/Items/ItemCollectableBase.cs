using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3f;
    public GameObject graphicItem;
    public Collider collider;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void HideItems()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        if (collider != null) collider.enabled = false;

        Invoke("HideObject", timeToHide);
    }

    protected virtual void Collect()
    {
        HideItems();
        OnCollect();        
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
            particleSystem.Play();
        }
        if(audioSource != null) audioSource.Play();
    }
}
