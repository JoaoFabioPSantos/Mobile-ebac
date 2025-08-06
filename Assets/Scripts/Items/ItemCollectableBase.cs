using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("Collected");
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {

    }
}
