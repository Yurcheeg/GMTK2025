using DG.Tweening;
using System;
using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public static System.Action GoalReached;
    public static System.Action Dropped;
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private TextMeshPro _scoreText;
    [SerializeField] private int _currentLevel;

    private int _loopCount;
    private int _loopTotal;
    private Coroutine _coroutine;

    private IEnumerator ShowTextTemporarily()
    {
        _scoreText.DOKill();
        _scoreText.DOFade(1f, 0f);
        _scoreText.text = $"{_loopCount} / {_loopTotal}";
        _scoreText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        _scoreText.DOFade(0f, 0.25f).OnComplete(() => _scoreText.gameObject.SetActive(false));
    }

    private void Awake()
    {
        //hackxd
        _loopTotal = FindObjectsByType<Loop>(FindObjectsSortMode.None).Length;
        _scoreText.gameObject.SetActive(false);

        if (_currentLevel == 0)
            throw new ArgumentOutOfRangeException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Loop loop) == false)
            return;

        Dropped?.Invoke();

        if (loop.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            rb.bodyType = RigidbodyType2D.Static;

        _loopCount++;
        ShowText();

        if (_loopCount == _loopTotal)
        {
            if (PlayerPrefs.GetInt("levels_unlocked", 1) <= _currentLevel)
                PlayerPrefs.SetInt("levels_unlocked", _currentLevel);
            GoalReached?.Invoke();
        }
    }

    private void ShowText()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShowTextTemporarily());

    }
    private void OnDestroy()
    {
        _scoreText.DOKill();
    }
}
