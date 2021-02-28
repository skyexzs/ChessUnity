using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : GameObjectCreator
{
    private ChessTile[,] chessTiles; // store the board's individual tiles' information
    private List<ChessPiece> blackPieces; // store the remaining black pieces in the board
    private List<ChessPiece> whitePieces; // store the remaining white pieces in the board

    private List<ChessPiece> eatenByBlack; // store the pieces that have been eaten by black
    private List<ChessPiece> eatenByWhite; // store the pieces that have been eaten by white

    private ChessPiece whiteKing; // store the reference of White's king
    private ChessPiece blackKing; // store the reference of Black's king
    private Coords inDanger = null; // if a king is in danger, store the coordinates

    public Coords kingInDanger { get { return inDanger; } } // get the coordinates of a king in danger

    public Team moveTurn = Team.White; // the turn of which team can move (White starts first)
    public int turnNumber = 1;
    public ChessPiece pieceInfo = null; // the piece that is being chosen (null = no pieces are currently chosen)

    public UIPromotionManager uiPromotion; // UI manager for promotion
    public UIWinManager uiWin; // UI manager for winning

    public Dictionary<string, bool> Options; // dictionary that contains the options for playing

    private Mode gameMode; // the game mode of the board (what and where pieces should be in the board)

    public ChessBoard(int x, int y, Mode mode, Dictionary<string, bool> options)
    {
        chessTiles = new ChessTile[x, y];
        blackPieces = new List<ChessPiece>();
        whitePieces = new List<ChessPiece>();

        eatenByBlack = new List<ChessPiece>();
        eatenByWhite = new List<ChessPiece>();

        if (options.Count == 0)
        {
            _SetDefaultOptions();
        }
        else Options = options;

        this.uiPromotion = GameObject.Find("Promotion_Canvas").GetComponent<UIPromotionManager>();
        this.uiWin = GameObject.Find("Win_Canvas").GetComponent<UIWinManager>();

        gameMode = mode;

        _Create(0, 0); // Create the board's unity game object

        _Generate(); // Generate the tiles and pieces

        Sprite blackTile = GameManager.SpriteManager["blackTile"];
        float x_pixels = blackTile.rect.width / blackTile.pixelsPerUnit;
        float y_pixels = blackTile.rect.height / blackTile.pixelsPerUnit;

        Camera.main.transform.position = new Vector3(((chessTiles.GetLength(0) / 2) - 1) * x_pixels + (x_pixels / 2), ((chessTiles.GetLength(1) / 2) - 1) * y_pixels + (y_pixels / 2), -10);
        Camera.main.orthographicSize = 13; // +3 = +2 tiles in Y-axis (above and below) //Ray: make it 13 from 12
    }

    private void _SetDefaultOptions()
    {
        Options.Add("highlight", true);
        Options.Add("rotate", true);
    }

    public ChessTile[,] GetBoardArray()
    {
        return chessTiles;
    }

    public void ResetMoveable()
    {
        pieceInfo = null;

        foreach (ChessTile tile in chessTiles)
        {
            tile.SetMoveable(MoveType.Default);
        }

        if (inDanger != null)
            chessTiles[inDanger.x, inDanger.y].SetColor(TileColor.Red, true);
    }

    public void RemoveFromBoard(ChessPiece piece)
    {
        if (piece.GetTeam() == Team.Black)
        {
            eatenByWhite.Add(piece);
            blackPieces.Remove(piece);
        }
        else if (piece.GetTeam() == Team.White)
        {
            eatenByBlack.Add(piece);
            whitePieces.Remove(piece);
        }
        GameObject.Destroy(piece.GetGameObject()); // destroy the piece's gameobject
    }

    public void RotateBoard()
    {
        foreach (ChessPiece piece in whitePieces)
        {
            float z = piece.GetGameObject().transform.localEulerAngles.z;
            piece.GetGameObject().transform.localEulerAngles = new Vector3(0, 0, (z + 180) % 360);
        }
        foreach (ChessPiece piece in blackPieces)
        {
            float z = piece.GetGameObject().transform.localEulerAngles.z;
            piece.GetGameObject().transform.localEulerAngles = new Vector3(0, 0, (z + 180) % 360);
        }
        float cam_z = Camera.main.transform.localEulerAngles.z;
        Camera.main.transform.localEulerAngles = new Vector3(0, 0, (cam_z + 180f) % 360f);
    }

    public void SetPiecesInteraction(bool b)
    {
        foreach (ChessPiece piece in whitePieces)
        {
            piece.Enabled = b;
        }
        foreach (ChessPiece piece in blackPieces)
        {
            piece.Enabled = b;
        }
    }

    public void onMoveListener(bool swapTurn = true)
    {
        // Swap the moveTurn of the game
        if (swapTurn) moveTurn = (moveTurn == Team.White ? Team.Black : Team.White);

        // If it's White's turn now (meaning black just moved), check whether White's King is in check and vice versa
        ChessPiece king = (moveTurn == Team.White ? whiteKing : blackKing);
        if (LoopAllPieces(moveTurn)) inDanger = new Coords { x = king.X, y = king.Y };
        else inDanger = null;

        ResetMoveable(); // reset the board's state

        if (CheckGameOver() == true)
        {
            return;
        }

        if (swapTurn)
        {
            if (Options["rotate"])
                RotateBoard(); // rotate the camera and pieces
            turnNumber++; // add the turn number
        }
    }

    private bool CheckGameOver()
    {
        bool gameOver = true; // set gameOver initially true

        foreach (ChessPiece piece in (moveTurn == Team.White ? whitePieces : blackPieces))
        {
            List<Coords> coords = piece.MovementCheck();
            List<Coords> invalid_coords = new List<Coords>(); // store invalid coordinates from TestCheckKing() and remove it from coords

            foreach (Coords c in coords) // check all possible movements if it causes a check or not
            {
                if (TestCheckKing(piece, c.x, c.y) == true)
                {
                    invalid_coords.Add(c);
                }
            }

            coords.RemoveAll(c => invalid_coords.Contains(c)); // remove the invalid coordinates from coords

            if (coords.Count > 0) // if there is at least one piece that can still move, set gameOver to false
            {
                gameOver = false;
                break;
            }
        }

        if (gameOver)
        {
            // If a king is in check and no pieces can move, initiate GameOver.
            if (inDanger != null)
            {
                GameOver();
            }
            // If not, initiate Stalemate.
            else if (inDanger == null)
            {
                Stalemate();
            }

            return true;
        }

        return false;
    }

    private void GameOver()
    {
        uiWin.Show(this, chessTiles[inDanger.x, inDanger.y].Piece.GetTeam());
    }

    private void Stalemate()
    {
        uiWin.Show(this);
    }

    public bool TestCheckKing(ChessPiece pieceToMove, int x, int y)
    {
        // We want to virtually move (without showing it in the GUI) the piece, test if there's a check, then return the state of the board.
        Coords stored_coord = pieceToMove.VirtualMove(x, y);
        ChessPiece stored_piece_prev = chessTiles[stored_coord.x, stored_coord.y].Piece;
        ChessPiece stored_piece_next = chessTiles[x, y].Piece;
        chessTiles[stored_coord.x, stored_coord.y].Piece = null;
        chessTiles[x, y].Piece = pieceToMove;

        // loop all pieces' movements and test the virtual state if there's a check
        bool check = LoopAllPieces(pieceToMove.GetTeam());

        // Now we return the board state before the piece was virtually moved.
        pieceToMove.VirtualMove(stored_coord.x, stored_coord.y);
        chessTiles[stored_coord.x, stored_coord.y].Piece = stored_piece_prev;
        chessTiles[x, y].Piece = stored_piece_next;

        return check;
    }

    // loop all pieces of a team and check if any of their possible movements triggers a check
    private bool LoopAllPieces(Team team)
    {
        bool check = false;

        ChessPiece king = (team == Team.White ? whiteKing : blackKing);

        foreach (ChessPiece piece in (team == Team.White ? blackPieces : whitePieces))
        {
            if (chessTiles[piece.X, piece.Y].Piece == piece) // Checks whether the piece is in the same location as in the virtual board
            {
                if (piece is IDiffEatBehaviour) // if the piece has different eating behaviour
                {
                    if (piece is Pawn && ((Pawn)piece).promoted != null) // if the piece is a pawn that has already been promoted
                    {
                        if (piece.MovementCheck().Exists(c => c.x == king.X && c.y == king.Y))
                        {
                            check = true;
                            break;
                        }
                    }
                    else // check the eat behaviour
                    {
                        IDiffEatBehaviour p = piece as IDiffEatBehaviour;
                        if (p.GetEatPattern().Exists(c => c.x == king.X && c.y == king.Y))
                        {
                            check = true;
                            break;
                        }
                    }
                }
                else if (piece.MovementCheck().Exists(c => c.x == king.X && c.y == king.Y)) // check the movement+eat behaviour
                {
                    check = true;
                    break;
                }
            }
        }

        return check;
    }

    private void _Generate()
    {
        bool alternate = true;
        for (int x = 0; x < chessTiles.GetLength(0); x++)
        {
            alternate = !alternate;
            for (int y = 0; y < chessTiles.GetLength(1); y++)
            {
                if (alternate)
                    chessTiles[x, y] = new ChessTile(this, x, y, GameManager.SpriteManager["whiteTile"]); 
                else
                    chessTiles[x, y] = new ChessTile(this, x, y, GameManager.SpriteManager["blackTile"]);
                alternate = !alternate;
            }
        }

        gameMode(this, whitePieces, blackPieces, out whiteKing, out blackKing);
    }

    private void _Create(int x, int y)
    {
        _Instantiate("ChessBoard");
        gameObject.transform.position = new Vector2(x, y);
    }
}
