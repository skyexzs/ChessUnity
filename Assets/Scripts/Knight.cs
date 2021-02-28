using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public Knight(Team team) : base(team) { }
    public Knight(ChessBoard board, int x, int y, Team team) : base(board, x, y, team) { }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whiteKnight"];
        else
            return GameManager.SpriteManager["blackKnight"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;
        patterns += MovePattern.MoveKnightUpLeft;
        patterns += MovePattern.MoveKnightUpRight;
        patterns += MovePattern.MoveKnightDownLeft;
        patterns += MovePattern.MoveKnightDownRight;
        patterns += MovePattern.MoveKnightLeftUp;
        patterns += MovePattern.MoveKnightLeftDown;
        patterns += MovePattern.MoveKnightRightUp;
        patterns += MovePattern.MoveKnightRightDown;
    }
}
