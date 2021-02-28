using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public Queen(Team team) : base(team) { }
    public Queen(ChessBoard board, int x, int y, Team team) : base(board, x, y, team) { }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whiteQueen"];
        else
            return GameManager.SpriteManager["blackQueen"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;
        patterns += MovePattern.MoveFullUp;
        patterns += MovePattern.MoveFullDown;
        patterns += MovePattern.MoveFullRight;
        patterns += MovePattern.MoveFullLeft;
        patterns += MovePattern.MoveFullNorthEast;
        patterns += MovePattern.MoveFullNorthWest;
        patterns += MovePattern.MoveFullSouthEast;
        patterns += MovePattern.MoveFullSouthWest;
    }
}
