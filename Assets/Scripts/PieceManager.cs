using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coords
{
    public int x { get; set; }
    public int y { get; set; }
}

public class PieceManager : MonoBehaviour
{
    private ChessPiece piece;
    
    public void SetPiece(ChessPiece piece)
    {
        this.piece = piece;
    }

    private void OnMouseDown()
    {
        if (piece.Enabled == true)
        {
            ChessBoard board = piece.GetBoard();

            if (board.moveTurn.Equals(piece.GetTeam())) // define move event
            {
                if (board.pieceInfo == null || !board.pieceInfo.Equals(piece))
                {
                    board.ResetMoveable();
                    board.pieceInfo = piece;
                    List<Coords> coords = piece.MovementCheck();
                    List<Coords> invalid_coords = new List<Coords>(); // store invalid coordinates from TestCheckKing() and remove it from coords

                    foreach (Coords c in coords) // check all possible movements if it causes a check or not
                    {
                        if (board.TestCheckKing(piece, c.x, c.y) == true)
                        {
                            invalid_coords.Add(c);
                        }
                    }

                    coords.RemoveAll(c => invalid_coords.Contains(c)); // remove the invalid coordinates from coords

                    ChessTile[,] tiles = board.GetBoardArray();
                    tiles[piece.X, piece.Y].SetColor(TileColor.Gray);
                    foreach (Coords c in coords)
                    {
                        if (tiles[c.x, c.y].Piece == null)
                            tiles[c.x, c.y].SetMoveable(MoveType.Move);
                        else
                            tiles[c.x, c.y].SetMoveable(MoveType.Eat);
                    }
                }
                else if (board.pieceInfo.Equals(piece))
                {
                    board.ResetMoveable();
                }
            }
            else // define eat event
            {
                if (board.pieceInfo != null)
                {
                    if (board.pieceInfo.GetTeam() != piece.GetTeam())
                    {
                        ChessTile[,] tiles = board.GetBoardArray();

                        if (tiles[piece.X, piece.Y].moveable == true)
                        {
                            board.RemoveFromBoard(piece); // remove the piece from board
                            piece.Enabled = false; // disable the piece interaction

                            // simple lambda function to allow code reuse in the statements below
                            System.Action<ChessPiece, ChessTile> movePiece = (p, t) =>
                            {
                                board.GetBoardArray()[p.X, p.Y].Piece = null; // set the previous tile's piece information to null
                                t.Piece = p; // store the piece into the tile
                                p.Move(t.X, t.Y); // move the piece's game object into the tile's position
                            };

                            // promotion + eat event
                            if (board.pieceInfo is Pawn && (tiles[piece.X, piece.Y].Y == 0 || tiles[piece.X, piece.Y].Y == board.GetBoardArray().GetLength(1) - 1) && ((Pawn)board.pieceInfo).promoted == null)
                            {
                                movePiece(board.pieceInfo, tiles[piece.X, piece.Y]);

                                Pawn p = board.pieceInfo as Pawn;
                                board.uiPromotion.Show(board, p);
                            }

                            else
                            {
                                movePiece(board.pieceInfo, tiles[piece.X, piece.Y]);
                            }

                            board.onMoveListener();
                        }
                    }
                }
            }
        }
    }
}
