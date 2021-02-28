using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGame
{
    private static SingletonGame _instance; // a static object (can only host one reference)

    private SingletonGame() { } // private constructor

    private static List<ChessBoard> boards; // stores the boards created during runtime
    public static ChessBoard currentBoard;
    
    public static SingletonGame Instance() // get the instance (or create one if none exist)
    {
        if (_instance == null)
        {
            _instance = new SingletonGame();
            boards = new List<ChessBoard>();
        }

        return _instance;
    }
    
    public void CreateBoard(int x, int y, Mode mode, Dictionary<string, bool> options) // create a new chessboard
    {
        ChessBoard cb = new ChessBoard(x, y, mode, options);
        boards.Add(cb);
        currentBoard = cb; 
    }
}
