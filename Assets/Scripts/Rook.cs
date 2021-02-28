using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public bool hasMoved; // to allow castling (rook swap with king)

    public Rook(Team team) : base(team) { }
    public Rook(ChessBoard board, int x, int y, Team team) : base(board, x, y, team) { }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whiteRook"];
        else
            return GameManager.SpriteManager["blackRook"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;
        patterns += MovePattern.MoveFullUp;
        patterns += MovePattern.MoveFullDown;
        patterns += MovePattern.MoveFullLeft;
        patterns += MovePattern.MoveFullRight;
    }

    public override void Move(int x, int y)
    {
        base.Move(x, y);
        hasMoved = true;
    }
}
