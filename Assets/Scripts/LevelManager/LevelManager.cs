using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Sections")]
    public float timeBetweenPieces = .3f;
    public List<LevelPieceBasedSetup> levelPieceBasedSetup;
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currentSetup;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .02f;
    public Ease ease = Ease.OutBack;

    [SerializeField]
    private int _index = 0;
    private GameObject _currentLevel;

    private void Awake()
    {
        //basta chamar esta função para o próximo nível: SpawnNextLevel();
        CreateLevel();
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            //reset list
            if (_index >= levels.Count) ResetLevelIndex();
        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region SECTIONS
    public void CreateLevel()
    {
        CleanSpawnedPieces();

        if (_currentSetup != null)
        {
            _index++;
            if (_index >= levelPieceBasedSetup.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentSetup = levelPieceBasedSetup[_index];

        for (int i = 0; i < _currentSetup.pieceStartNumber; i++)
        {
            CreateLevelSection(_currentSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currentSetup.pieceNumber; i++)
        {
            CreateLevelSection(_currentSetup.levelPieces);
        }

        for (int i = 0; i < _currentSetup.pieceEndNumber; i++)
        {
            CreateLevelSection(_currentSetup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(_currentSetup.artType);
        StartCoroutine(ScalePiecesByTime());
        //PARA VISUALIZAR: StartCoroutine(CreateLevelPiecesCoroutine());
    }

    private void CreateLevelSection(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.position = Vector3.zero;
        }

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currentSetup.artType).artGameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach(var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }
        yield return null;

        for(int i = 0; i<_spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
        //CoinsAnimationManager.Instance.StartAnimations();
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count-1; i>=0 ; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();
        for (int i = 0; i < _currentSetup.pieceNumber; i++)
        {
            CreateLevelSection(_currentSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CreateLevel();
        }
    }
}
