using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private ChessTile tile;

    public void SetTile(ChessTile tile)
    {
        this.tile = tile;
    }

    private void OnMouseDown()
    {
        if (tile.moveable == true)
        {
            ChessBoard board = tile.GetBoard();

            // simple lambda function to allow code reuse in the statements below
            System.Action<ChessPiece, ChessTile> movePiece = (p, t) =>
            {
                board.GetBoardArray()[p.X, p.Y].Piece = null; // set the previous tile's piece information to null
                t.Piece = p; // store the piece into the tile
                p.Move(t.X, t.Y); // move the piece's game object into the tile's position
            };

            //hardcoded castling algorithm
            if (board.pieceInfo is King && Mathf.Abs(tile.X - board.pieceInfo.X) > 1)
            {
                ChessTile t = (tile.X - board.pieceInfo.X > 0) ? board.GetBoardArray()[tile.X - 1, tile.Y] : board.GetBoardArray()[tile.X + 1, tile.Y];
                Rook r = (tile.X - board.pieceInfo.X > 0) ? board.GetBoardArray()[tile.X + 1, tile.Y].Piece as Rook : board.GetBoardArray()[tile.X - 2, tile.Y].Piece as Rook;
                movePiece(r, t);

                movePiece(board.pieceInfo, tile); // move king
            }

            //hardcoded en passant algorithm
            else if (board.pieceInfo is Pawn && tile.Piece == null && tile.X != board.pieceInfo.X && ((Pawn)board.pieceInfo).promoted == null)
            {
                ChessTile t = (board.pieceInfo.GetTeam() == Team.White) ? board.GetBoardArray()[tile.X, tile.Y - 1] : board.GetBoardArray()[tile.X, tile.Y + 1];

                board.RemoveFromBoard(t.Piece); // remove the piece from board
                t.Piece.Enabled = false; // disable the piece interaction
                t.Piece = null; // set the previous tile's piece information to null

                movePiece(board.pieceInfo, tile);
            }

            //hardcoded promotion algorithm
            else if (board.pieceInfo is Pawn && (tile.Y == 0 || tile.Y == board.GetBoardArray().GetLength(1) - 1) && ((Pawn)board.pieceInfo).promoted == null)
            {
                movePiece(board.pieceInfo, tile);

                Pawn p = board.pieceInfo as Pawn;
                board.uiPromotion.Show(board, p);
            }

            // normal move
            else if (board.pieceInfo != null)
            {
                movePiece(board.pieceInfo, tile); // move the piece
            }

            board.onMoveListener();
        }
    }
}
