using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessFactory
{
    public abstract T CreatePiece<T>() where T : ChessPiece;
    public abstract T CreatePiece<T>(ChessBoard board, int x, int y) where T : ChessPiece;
}

public class WhitePieceFactory : ChessFactory
{
    public override T CreatePiece<T>()
    {
        T p = System.Activator.CreateInstance(typeof(T), Team.White) as T;
        return p;
    }
    public override T CreatePiece<T>(ChessBoard board, int x, int y)
    {
        T p = System.Activator.CreateInstance(typeof(T), board, x, y, Team.White) as T;
        board.GetBoardArray()[x, y].Piece = p;
        return p;
    }
}

public class BlackPieceFactory : ChessFactory
{
    public override T CreatePiece<T>()
    {
        T p = System.Activator.CreateInstance(typeof(T), Team.Black) as T;
        return p;
    }
    public override T CreatePiece<T>(ChessBoard board, int x, int y)
    {
        T p = System.Activator.CreateInstance(typeof(T), board, x, y, Team.Black) as T;
        board.GetBoardArray()[x, y].Piece = p;
        return p;
    }
}
