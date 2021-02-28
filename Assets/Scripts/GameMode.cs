using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Mode (ChessBoard board, List<ChessPiece> whitePieces, List<ChessPiece> blackPieces, out ChessPiece whiteKing, out ChessPiece blackKing);

public class GameMode
{
    public static void Default(ChessBoard board, List<ChessPiece> whitePieces, List<ChessPiece> blackPieces, out ChessPiece whiteKing, out ChessPiece blackKing)
    {
        ChessFactory whiteFactory = new WhitePieceFactory();
        ChessFactory blackFactory = new BlackPieceFactory();

        for (int i = 0; i < 8; i++)
        {
            whitePieces.Add(whiteFactory.CreatePiece<Pawn>(board, i, 1));
        }
        whitePieces.Add(whiteFactory.CreatePiece<Rook>(board, 0, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Rook>(board, 7, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Knight>(board, 1, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Knight>(board, 6, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Bishop>(board, 2, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Bishop>(board, 5, 0));
        whitePieces.Add(whiteFactory.CreatePiece<Queen>(board, 3, 0));
        whiteKing = whiteFactory.CreatePiece<King>(board, 4, 0);
        whitePieces.Add(whiteKing);

        for (int i = 0; i < 8; i++)
        {
            blackPieces.Add(blackFactory.CreatePiece<Pawn>(board, i, 6));
        }
        blackPieces.Add(blackFactory.CreatePiece<Rook>(board, 0, 7));
        blackPieces.Add(blackFactory.CreatePiece<Rook>(board, 7, 7));
        blackPieces.Add(blackFactory.CreatePiece<Knight>(board, 1, 7));
        blackPieces.Add(blackFactory.CreatePiece<Knight>(board, 6, 7));
        blackPieces.Add(blackFactory.CreatePiece<Bishop>(board, 2, 7));
        blackPieces.Add(blackFactory.CreatePiece<Bishop>(board, 5, 7));
        blackPieces.Add(blackFactory.CreatePiece<Queen>(board, 3, 7));
        blackKing = blackFactory.CreatePiece<King>(board, 4, 7);
        blackPieces.Add(blackKing);

        board.uiPromotion.SetPromotionOptions<Queen>(1, new SpriteHolder { whiteSprite = GameManager.SpriteManager["whiteQueen"], blackSprite = GameManager.SpriteManager["blackQueen"], name = "Queen" });
        board.uiPromotion.SetPromotionOptions<Knight>(2, new SpriteHolder { whiteSprite = GameManager.SpriteManager["whiteKnight"], blackSprite = GameManager.SpriteManager["blackKnight"], name = "Knight" });
        board.uiPromotion.SetPromotionOptions<Rook>(3, new SpriteHolder { whiteSprite = GameManager.SpriteManager["whiteRook"], blackSprite = GameManager.SpriteManager["blackRook"], name = "Rook" });
        board.uiPromotion.SetPromotionOptions<Bishop>(4, new SpriteHolder { whiteSprite = GameManager.SpriteManager["whiteBishop"], blackSprite = GameManager.SpriteManager["blackBishop"], name = "Bishop" });
    }
}
