using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TBD
// - en passant
// - promote to become other piece

public enum PawnDirection
{
    UP = 1, DOWN = -1
}

// interface for pieces that have different eating behaviour
public interface IDiffEatBehaviour
{
    List<Coords> GetEatPattern();
}

public class Pawn : ChessPiece, IDiffEatBehaviour
{
    public int hasMoved { get; private set; } // to allow the pawn to move 2 steps forward for its first move
    public int lastMoveTurn { get; private set; } // the turn number this pawn last moved
    public ChessPiece promoted { get; private set; } // check whether this pawn has promotion or not
    private PawnDirection direction; // the direction of the pawn's movement, dependent on the team it's in

    // Since pawns haave different move and eat behaviours, they need to have their own pattern delegates
    // It will still use the "patterns" delegate field but that delegate is for move only
    private Patterns eatPatterns;

    public Pawn(Team team) : base(team) { }
    public Pawn(ChessBoard board, int x, int y, Team team) : base(board, x, y, team)
    {
        hasMoved = 0;
        promoted = null;

        if (team == Team.White) direction = PawnDirection.UP;
        else direction = PawnDirection.DOWN;

        SetPattern(out patterns);
    }

    public override Sprite SetSprite()
    {
        if (GetTeam() == Team.White)
            return GameManager.SpriteManager["whitePawn"];
        else
            return GameManager.SpriteManager["blackPawn"];
    }

    public override void SetPattern(out Patterns patterns)
    {
        patterns = null;

        if (direction == PawnDirection.UP)
        {
            patterns += MovePattern.MoveOneUp;
            eatPatterns += MovePattern.MoveOneNorthEast;
            eatPatterns += MovePattern.MoveOneNorthWest;
            eatPatterns += MovePattern.EnPassantNorthEast;
            eatPatterns += MovePattern.EnPassantNorthWest;
        }

        else if (direction == PawnDirection.DOWN)
        {
            patterns += MovePattern.MoveOneDown;
            eatPatterns += MovePattern.MoveOneSouthEast;
            eatPatterns += MovePattern.MoveOneSouthWest;
            eatPatterns += MovePattern.EnPassantSouthEast;
            eatPatterns += MovePattern.EnPassantSouthWest;
        }
    }

    public override void Move(int x, int y)
    {
        base.Move(x, y);
        hasMoved += 1;
        lastMoveTurn = GetBoard().turnNumber;
    }

    public override List<Coords> MovementCheck()
    {
        List<Coords> coords = new List<Coords>();

        if (promoted == null) // if the pawn has not been promoted (still act as normal)
        {
            // Pawns have different movement and eating behaviour so it is divided into two.
            // Movement Behaviour
            if (hasMoved > 0)
            {
                patterns(coords, this, move: true, eat: false);
            }
            else
            {
                if (direction == PawnDirection.UP)
                    patterns += MovePattern.MoveTwoUp;
                if (direction == PawnDirection.DOWN)
                    patterns += MovePattern.MoveTwoDown;

                patterns(coords, this, move: true, eat: false);

                patterns -= MovePattern.MoveTwoUp;
                patterns -= MovePattern.MoveTwoDown;
            }

            eatPatterns?.Invoke(coords, this, move: false, eat: true);
        }
        else // if promoted, move and eat
        {
            patterns(coords, this);
        }

        return coords;
    }

    public List<Coords> GetEatPattern()
    {
        List<Coords> coords = new List<Coords>();
        eatPatterns?.Invoke(coords, this, move: false, eat: true);
        return coords;
    }

    public void Promote<T>() where T : ChessPiece
    {
        ChessFactory factory;

        if (GetTeam() == Team.White) factory = new WhitePieceFactory();
        else factory = new BlackPieceFactory();

        promoted = factory.CreatePiece<T>();
        promoted.SetPattern(out patterns);
        this.GetGameObject().GetComponent<SpriteRenderer>().sprite = promoted.SetSprite();
    }
}
