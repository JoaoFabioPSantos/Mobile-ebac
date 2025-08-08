using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public float speed = 3f;

    [Header("Tags")]
    public string tagToCheckEnemy = "Enemy";
    public string tagEndLine = "EndLine";

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Screens")]
    public GameObject endScreen;
    public GameObject winScreen;

    [Header("Power Ups config")]
    public TextMeshPro uiTextPowerUp;
    public bool invencible = false;
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;


    private bool _canRun;
    private Vector3 _position;
    private bool _isWin;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 5f;

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;

        _position = target.position;
        _position.y = transform.position.y;
        _position.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _position, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            if (!invencible)
            {
                MoveBack(transform);
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagEndLine)
        {
            _isWin = true;
            if (!invencible)EndGame(_isWin);
        }
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(-2f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
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
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed/_baseSpeedToAnimation);
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    //quando igualamos um valor assim, é um valor default
    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }
    
    public void  ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
