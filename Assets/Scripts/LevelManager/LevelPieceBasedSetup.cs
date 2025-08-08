using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBasedSetup : ScriptableObject
{
    [Header("Arts")]
    public ArtManager.ArtType artType;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPiecesEnd;

    [Header("Numbers")]
    public int pieceNumber = 5;
    public int pieceStartNumber = 2;
    public int pieceEndNumber = 1;
}
