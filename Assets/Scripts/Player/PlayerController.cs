using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    public string tagToCheckEnemy = "Enemy";
    public string tagEndLine = "EndLine";

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Screens")]
    public GameObject endScreen;
    public GameObject winScreen;

    private bool _canRun;
    private Vector3 _position;
    private bool _isWin;


    void Update()
    {
        if (!_canRun) return;

        _position = target.position;
        _position.y = transform.position.y;
        _position.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _position, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagEndLine)
        {
            _isWin = true;
            EndGame(_isWin);
        }
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    private void EndGame(bool isWin)
    {
        if (isWin)
        {
            _canRun = false;
            winScreen.SetActive(true);
        }
    }

    public void StartToRun()
    {
        _canRun = true;
    }
}
