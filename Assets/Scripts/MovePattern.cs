using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Patterns(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true);

public class MovePattern
{
    public static void MoveFullUp(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();

        for (int y = piece.Y + 1; y < tilesInfo.GetLength(1); y++) // vertical up movement
        {
            if (!_CheckCoords(coords, piece, piece.X, y, move, eat)) break;
        }
    }

    public static void MoveFullDown(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        for (int y = piece.Y - 1; y >= 0; y--) // vertical down movement
        {
            if (!_CheckCoords(coords, piece, piece.X, y, move, eat)) break;
        }
    }

    public static void MoveFullLeft(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        for (int x = piece.X - 1; x >= 0; x--) // horizontal left movement
        {
            if (!_CheckCoords(coords, piece, x, piece.Y, move, eat)) break;
        }
    }

    public static void MoveFullRight(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();

        for (int x = piece.X + 1; x < tilesInfo.GetLength(0); x++) // horizontal right movement
        {
            if (!_CheckCoords(coords, piece, x, piece.Y, move, eat)) break;
        }
    }

    public static void MoveOneLeft(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 1, piece.Y, move, eat);
    }

    public static void MoveOneRight(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 1, piece.Y, move, eat);
    }

    public static void MoveOneUp(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X, piece.Y + 1, move, eat);
    }

    public static void MoveOneDown(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X, piece.Y - 1, move, eat);
    }

    public static void MoveOneNorthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 1, piece.Y + 1, move, eat);
    }

    public static void MoveOneNorthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 1, piece.Y + 1, move, eat);
    }

    public static void MoveOneSouthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 1, piece.Y - 1, move, eat);
    }

    public static void MoveOneSouthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 1, piece.Y - 1, move, eat);
    }

    public static void MoveTwoUp(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        for (int i = 1; i <= 2; i++)
        {
            if (!_CheckCoords(coords, piece, piece.X, piece.Y + i, move, eat)) break;
        }
    }

    public static void MoveTwoDown(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        for (int i = 1; i <= 2; i++)
        {
            if (!_CheckCoords(coords, piece, piece.X, piece.Y - i, move, eat)) break;
        }
    }

    public static void MoveFullNorthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
        int rightDist = tilesInfo.GetLength(0) - piece.X;
        int upDist = tilesInfo.GetLength(1) - piece.Y;

        for (int i = 1; i <= Mathf.Max(rightDist, upDist); i++) // north-east movement
        {
            if (!_CheckCoords(coords, piece, piece.X + i, piece.Y + i, move, eat)) break;
        }
    }

    public static void MoveFullNorthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
        int leftDist = piece.X - 0;
        int upDist = tilesInfo.GetLength(1) - piece.Y;

        for (int i = 1; i <= Mathf.Max(leftDist, upDist); i++) // north-west movement
        {
            if (!_CheckCoords(coords, piece, piece.X - i, piece.Y + i, move, eat)) break;
        }
    }

    public static void MoveFullSouthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
        int leftDist = piece.X - 0;
        int downDist = piece.Y - 0;

        for (int i = 1; i <= Mathf.Max(leftDist, downDist); i++) // south-west movement
        {
            if (!_CheckCoords(coords, piece, piece.X - i, piece.Y - i, move, eat)) break;
        }
    }

    public static void MoveFullSouthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
        int rightDist = tilesInfo.GetLength(0) - piece.X;
        int downDist = piece.Y - 0;

        for (int i = 1; i <= Mathf.Max(rightDist, downDist); i++) // south-east movement
        {
            if (!_CheckCoords(coords, piece, piece.X + i, piece.Y - i, move, eat)) break;
        }
    }

    public static void MoveKnightUpLeft(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 1, piece.Y + 2, move, eat); // the "2 up 1 left" movement
    }

    public static void MoveKnightUpRight(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 1, piece.Y + 2, move, eat); // the "2 up 1 right" movement
    }

    public static void MoveKnightDownLeft(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 1, piece.Y - 2, move, eat); // the "2 down 1 left" movement
    }

    public static void MoveKnightDownRight(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 1, piece.Y - 2, move, eat); // the "2 down 1 right" movement
    }

    public static void MoveKnightLeftUp(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 2, piece.Y + 1, move, eat); // the "2 left 1 up" movement
    }

    public static void MoveKnightLeftDown(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X - 2, piece.Y - 1, move, eat); // the "2 left 1 down" movement
    }

    public static void MoveKnightRightUp(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 2, piece.Y + 1, move, eat); // the "2 right 1 up" movement
    }

    public static void MoveKnightRightDown(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        _CheckCoords(coords, piece, piece.X + 2, piece.Y - 1, move, eat); // the "2 right 1 down" movement
    }

    // Castling uses different logic so it doesn't utilise _CheckCoords
    public static void CastlingLong(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        try
        {
            // Only permitted if the piece is a King piece
            if (piece is King && piece.GetBoard().kingInDanger == null)
            {
                ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
                int x = piece.X;
                int y = piece.Y;

                ChessPiece p = tilesInfo[x - 4, y].Piece;

                if (p is Rook)
                {
                    Rook rook = p as Rook;

                    if (rook.hasMoved == true) return;
                }

                if (tilesInfo[x - 1, y].Piece == null)
                {
                    if (tilesInfo[x - 2, y].Piece == null)
                    {
                        if (tilesInfo[x - 3, y].Piece == null)
                        {
                            coords.Add(new Coords { x = x - 2, y = y });
                        }
                    }
                }
            }
        }
        catch (System.IndexOutOfRangeException) { return; }
    }

    public static void CastlingShort(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        try
        {
            // Only permitted if the piece is a King piece and no king is in check
            if (piece is King && piece.GetBoard().kingInDanger == null)
            {
                ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();
                int x = piece.X;
                int y = piece.Y;

                ChessPiece p = tilesInfo[x + 3, y].Piece;

                if (p is Rook)
                {
                    Rook rook = p as Rook;

                    if (rook.hasMoved == true) return;
                }

                if (tilesInfo[x + 1, y].Piece == null)
                {
                    if (tilesInfo[x + 2, y].Piece == null)
                    {
                        coords.Add(new Coords { x = x + 2, y = y });
                    }
                }
            }
        }
        catch (System.IndexOutOfRangeException) { return; }
    }

    // pawn's different move pattern (to include en passant)
    public static void EnPassantNorthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        if (_CheckEnPassant(piece, piece.X + 1, piece.Y))
            coords.Add(new Coords { x = piece.X + 1, y = piece.Y + 1 });
    }

    public static void EnPassantNorthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        if (_CheckEnPassant(piece, piece.X - 1, piece.Y))
            coords.Add(new Coords { x = piece.X - 1, y = piece.Y + 1 });
    }

    public static void EnPassantSouthEast(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        if (_CheckEnPassant(piece, piece.X + 1, piece.Y))
            coords.Add(new Coords { x = piece.X + 1, y = piece.Y - 1 });
    }

    public static void EnPassantSouthWest(List<Coords> coords, ChessPiece piece, bool move = true, bool eat = true)
    {
        if (_CheckEnPassant(piece, piece.X - 1, piece.Y))
            coords.Add(new Coords { x = piece.X - 1, y = piece.Y - 1 });
    }

    private static bool _CheckCoords(List<Coords> coords, ChessPiece piece, int x, int y, bool move = true, bool eat = true)
    {
        ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();

        try
        {
            if (tilesInfo[x, y].Piece == null && move == true)
            {
                coords.Add(new Coords { x = x, y = y });
                return true;
            }
            else if (tilesInfo[x, y].Piece != null && tilesInfo[x, y].Piece.GetTeam() != piece.GetTeam() && eat == true)
            {
                coords.Add(new Coords { x = x, y = y });
                return false;
            }
        }
        catch (System.IndexOutOfRangeException) { return false; } // ignore errors

        return false;
    }

    private static bool _CheckEnPassant(ChessPiece piece, int x, int y)
    {
        try
        {
            // Only permitted if piece is a pawn
            if (piece is Pawn)
            {
                ChessTile[,] tilesInfo = piece.GetBoard().GetBoardArray();

                if (tilesInfo[x, y].Piece is Pawn)
                {
                    Pawn p = tilesInfo[x, y].Piece as Pawn;

                    if (p.hasMoved == 1 && piece.GetBoard().turnNumber - p.lastMoveTurn < 2 && (y == 3 || y == 4)) return true;
                }
            }
        }
        catch (System.IndexOutOfRangeException) { return false; }

        return false;
    }
}
