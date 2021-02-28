using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    private bool hasMoved; // to allow castling (rook swap with king)
    private Patterns castlingPatterns; // special patterns for castling

    public King(Team team) : base(team) { }
    public King(ChessBoard board, int x, int y, Team team) : base(board, x, y, team) { }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whiteKing"];
        else
            return GameManager.SpriteManager["blackKing"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;
        patterns += MovePattern.MoveOneUp;
        patterns += MovePattern.MoveOneDown;
        patterns += MovePattern.MoveOneLeft;
        patterns += MovePattern.MoveOneRight;
        patterns += MovePattern.MoveOneNorthEast;
        patterns += MovePattern.MoveOneNorthWest;
        patterns += MovePattern.MoveOneSouthEast;
        patterns += MovePattern.MoveOneSouthWest;
    }

    public override void Move(int x, int y)
    {
        base.Move(x, y);
        hasMoved = true;
    }

    public override List<Coords> MovementCheck()
    {
        List<Coords> coords = new List<Coords>();

        if (hasMoved == true)
        {
            patterns(coords, this);
        }
        else
        {
            patterns += MovePattern.CastlingLong;
            patterns += MovePattern.CastlingShort;

            patterns(coords, this);

            patterns -= MovePattern.CastlingLong;
            patterns -= MovePattern.CastlingShort;
        }

        return coords;
    }
}