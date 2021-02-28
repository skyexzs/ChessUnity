using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    List<Coords> MovementCheck();
}

public enum Team
{
    White, Black
}

public abstract class ChessPiece : GameObjectCreator, IMoveable
{
    private int x, y;
    private Team team;
    private Sprite sprite;
    private ChessBoard board;
    private bool enabled;

    protected Patterns patterns; // default move pattern (can move and eat)

    public bool Enabled { get { return enabled; } set { this.enabled = value; } }

    public Team GetTeam()
    {
        return team;
    }

    public ChessBoard GetBoard()
    {
        return board;
    }

    public int X { get { return x; } protected set { y = value; } }
    public int Y { get { return y; } protected set { y = value; } }

    public ChessPiece(Team team) // empty constructor to create placeholder piece
    {
        this.team = team;
    }

    public ChessPiece(ChessBoard board, int x, int y, Team team)
    {
        this.board = board;
        this.x = x;
        this.y = y;
        this.team = team;
        this.sprite = SetSprite();
        this.enabled = true;
        this.SetPattern(out patterns);

        _Create(x, y, sprite);
    }

    public abstract Sprite SetSprite();

    private void _Create(int x, int y, Sprite sprite)
    {
        _Instantiate(sprite.name);
        _MoveObject();
        gameObject.transform.SetParent(board.GetGameObject().transform);
        SpriteRenderer s = gameObject.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        s.sortingLayerName = "Piece";
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(3, 3);
        gameObject.AddComponent<PieceManager>().SetPiece(this);
    }

    public virtual void Move(int x, int y)
    {
        this.x = x;
        this.y = y;

        _MoveObject();
    }

    public virtual Coords VirtualMove(int x, int y)
    {
        Coords c = new Coords { x = this.x, y = this.y };
        this.x = x;
        this.y = y;
        return c;
    }

    private void _MoveObject()
    {
        gameObject.transform.position = new Vector3(x * sprite.rect.width / sprite.pixelsPerUnit, y * sprite.rect.height / sprite.pixelsPerUnit, -1);
    }

    public abstract void SetPattern(out Patterns patterns);

    public virtual List<Coords> MovementCheck()
    {
        List<Coords> coords = new List<Coords>();
        patterns(coords, this);
        return coords;
    }
}
