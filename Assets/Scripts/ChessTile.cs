using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Move, Eat, Default
}

public enum TileColor
{
    Green, Red, White, Gray
}

public class ChessTile : GameObjectCreator
{
    private int x, y; // coordinates of the tile
    private ChessPiece piece; // piece info standing in the tile
    private Sprite sprite; // tile's sprite
    private ChessBoard board; // the board this tile belongs to

    public bool moveable = false; // the boolean that controls whether the tile is clickable or not

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public ChessPiece Piece { get; set; } // property to set the piece

    public ChessBoard GetBoard()
    {
        return board;
    }

    public void SetMoveable(MoveType t)
    {
        if (t == MoveType.Move)
        {
            SetColor(TileColor.Green);
            moveable = true;
        }
        else if (t == MoveType.Eat)
        {
            SetColor(TileColor.Red);
            moveable = true;
        }
        else if (t == MoveType.Default)
        {
            SetColor(TileColor.White);
            moveable = false;
        }
    }

    public void SetColor(TileColor tc, bool force = false)
    {
        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();

        if (tc == TileColor.Gray)
        {
            s.color = new Color32((byte)145, (byte)145, (byte)145, (byte)255);
        }
        else if (tc == TileColor.White)
        {
            s.color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
        }
        else if (board.Options["highlight"] == true || force == true)
        {
            switch (tc)
            {
                case TileColor.Green:
                    s.color = new Color32((byte)188, (byte)255, (byte)154, (byte)255);
                    break;
                case TileColor.Red:
                    s.color = new Color32((byte)255, (byte)80, (byte)80, (byte)255);
                    break;
                default:
                    s.color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                    break;
            }
        }
    }

    public ChessTile(ChessBoard board, int x, int y, Sprite sprite)
    {
        this.board = board;
        this.x = x;
        this.y = y;
        this.sprite = sprite;

        _Create(board, x, y, sprite);
    }

    private void _Create(ChessBoard board, int x, int y, Sprite sprite)
    {
        _Instantiate((System.Convert.ToChar(x+65)).ToString() + (y+1).ToString());
        gameObject.transform.position = new Vector2(x * sprite.rect.width/sprite.pixelsPerUnit, y * sprite.rect.height/sprite.pixelsPerUnit);
        gameObject.transform.SetParent(board.GetGameObject().transform);
        SpriteRenderer s = gameObject.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        s.sortingLayerName = "Board";
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(3, 3);
        gameObject.AddComponent<TileManager>().SetTile(this);
    }
}
