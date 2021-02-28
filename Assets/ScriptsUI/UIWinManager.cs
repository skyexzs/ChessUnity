using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public void Show(ChessBoard board, Team team)
    {
        board.SetPiecesInteraction(false);
        this.transform.Find("Win_Panel").Find("winner_text").GetComponent<TMPro.TextMeshProUGUI>().text = (team == Team.White) ? "BLACK" : "WHITE";
        this.transform.Find("Win_Panel").Find("win_text").GetComponent<TMPro.TextMeshProUGUI>().text = "- WINS -";
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void Show(ChessBoard board)
    {
        board.SetPiecesInteraction(false);
        this.transform.Find("Win_Panel").Find("winner_text").GetComponent<TMPro.TextMeshProUGUI>().text = "DRAW";
        this.transform.Find("Win_Panel").Find("win_text").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }
}
