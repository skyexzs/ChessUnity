using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public Bishop(Team team) : base(team) { }
    public Bishop(ChessBoard board, int x, int y, Team team) : base(board, x, y, team) { }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whiteBishop"];
        else
            return GameManager.SpriteManager["blackBishop"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;
        patterns += MovePattern.MoveFullNorthEast;
        patterns += MovePattern.MoveFullNorthWest;
        patterns += MovePattern.MoveFullSouthEast;
        patterns += MovePattern.MoveFullSouthWest;
    }
}
