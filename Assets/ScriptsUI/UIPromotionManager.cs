using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteHolder
{
    public Sprite whiteSprite { get; set; }
    public Sprite blackSprite { get; set; }
    public string name { get; set; }
}

public class UIPromotionManager : MonoBehaviour
{
    private ChessBoard board;
    private Pawn pawn;

    public List<Transform> grids;
    public Dictionary<string, SpriteHolder> options;
    private CanvasGroup canvasGroup;

    public void Show(ChessBoard board, Pawn pawn)
    {
        this.board = board;
        this.pawn = pawn;
        board.SetPiecesInteraction(false);

        SetSprite(pawn.GetTeam());
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
        grids = new List<Transform>();
        options = new Dictionary<string, SpriteHolder>();
        canvasGroup = this.GetComponent<CanvasGroup>();

        Transform grid_panel = this.transform.Find("promotion_panel").Find("grid_panel");
        for (int i = 1; i <= 4; i++)
        {
            grids.Add(grid_panel.Find("grid" + i));
            options.Add("grid" + i, null);
        }
    }

    public void SetPromotionOptions<T>(int index, SpriteHolder spt) where T : ChessPiece
    {
        options["grid" + index] = spt;
        Button btn = grids[index-1].GetComponent<Button>();
        btn.onClick.AddListener(delegate { Promote<T>(); });
    }

    public void SetSprite(Team team)
    {
        List<Transform> grid_images = new List<Transform>();
        List<Transform> grid_texts = new List<Transform>();

        for (int i = 1; i <= grids.Count; i++)
        {
            grid_images.Add(grids[i-1].Find("image"));
            grid_texts.Add(grids[i - 1].Find("name_text"));

            if (team == Team.White)
                grid_images[i - 1].GetComponent<Image>().sprite = options["grid" + i].whiteSprite;
            else
                grid_images[i - 1].GetComponent<Image>().sprite = options["grid" + i].blackSprite;
            grid_texts[i - 1].GetComponent<TMPro.TextMeshProUGUI>().text = options["grid" + i].name;
        }
    }

    public void Promote<T>() where T : ChessPiece
    {
        pawn.Promote<T>();
        board.SetPiecesInteraction(true);
        board.onMoveListener(false);
        board = null;
        pawn = null;

        Hide();
    }
}
