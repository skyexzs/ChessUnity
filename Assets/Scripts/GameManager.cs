using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // sprites used for the tiles and pieces, assigned through drag&drop from asset
    [SerializeField] private Sprite blackTile;
    [SerializeField] private Sprite whiteTile;
    [SerializeField] private Sprite blackPawn;
    [SerializeField] private Sprite whitePawn;
    [SerializeField] private Sprite blackRook;
    [SerializeField] private Sprite whiteRook;
    [SerializeField] private Sprite blackKnight;
    [SerializeField] private Sprite whiteKnight;
    [SerializeField] private Sprite blackBishop;
    [SerializeField] private Sprite whiteBishop;
    [SerializeField] private Sprite blackQueen;
    [SerializeField] private Sprite whiteQueen;
    [SerializeField] private Sprite blackKing;
    [SerializeField] private Sprite whiteKing;

    // static dictionary that holds all the sprites
    public static Dictionary<string, Sprite> SpriteManager;

    // called only once when scene is entered
    private void Awake()
    {
        CreateSpriteDictionary(); // store all the sprites into a dictionary variable

        SingletonGame.Instance();
    }

    private void CreateSpriteDictionary()
    {
        SpriteManager = new Dictionary<string, Sprite>();
        SpriteManager.Add("blackTile", blackTile);
        SpriteManager.Add("whiteTile", whiteTile);
        SpriteManager.Add("blackPawn", blackPawn);
        SpriteManager.Add("whitePawn", whitePawn);
        SpriteManager.Add("blackRook", blackRook);
        SpriteManager.Add("whiteRook", whiteRook);
        SpriteManager.Add("blackKnight", blackKnight);
        SpriteManager.Add("whiteKnight", whiteKnight);
        SpriteManager.Add("blackBishop", blackBishop);
        SpriteManager.Add("whiteBishop", whiteBishop);
        SpriteManager.Add("blackQueen", blackQueen);
        SpriteManager.Add("whiteQueen", whiteQueen);
        SpriteManager.Add("blackKing", blackKing);
        SpriteManager.Add("whiteKing", whiteKing);
    }
}
