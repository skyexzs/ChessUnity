using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts {
    public enum Options
    {
        Highlight, Rotate
    }

    public class MainMenu : MonoBehaviour
    {
        public List<Sprite> chessPieces;
        public Image pieceImage;
        Sprite currentPiece;
        Animator animator;

        //Animator
        int booEnterGame;
        int triPlayGame;

        //Prop
        bool isBright = false;
        float brightnessMin = 0.5f;
        float brightnessMax = 0.85f;
        float brightnessSpeed = 0.0025f;

        bool highlight = false;
        bool rotate = false;

        // Start is called before the first frame update
        void Start()
        {
            //Assign components
            animator = GetComponent<Animator>();

            //Animator params
            booEnterGame = Animator.StringToHash("boo_EnterGame");
            triPlayGame = Animator.StringToHash("tri_Outro");

            currentPiece = chessPieces[Random.Range(0, chessPieces.Count)];
            pieceImage.sprite = currentPiece;
        }

        // Update is called once per frame
        void Update()
        {
            if (isBright)
            {
                if (pieceImage.color.a < brightnessMax) pieceImage.color = new Color(pieceImage.color.r, pieceImage.color.g, pieceImage.color.b, pieceImage.color.a + brightnessSpeed);
            }
            else
            {
                if (pieceImage.color.a > brightnessMin) pieceImage.color = new Color(pieceImage.color.r, pieceImage.color.g, pieceImage.color.b, pieceImage.color.a - brightnessSpeed);
            }
        }

        /// <summary>
        /// Called by Play button
        /// </summary>
        public void SetAnimatorEnterGame(bool _isEnter)
        {
            animator.SetBool(booEnterGame, _isEnter);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void ToggleHighlight()
        {
            highlight = !highlight;

            Transform button = this.transform.Find("cornerPanel").Find("slide_panel").Find("disPosMove_but");
            button.GetComponent<Image>().color = (highlight) ? new Color32(255, 255, 255, 255) : new Color32(255, 90, 65, 255);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (highlight) ? "ON" : "OFF";
        }

        public void ToggleRotate()
        {
            rotate = !rotate;

            Transform button = this.transform.Find("cornerPanel").Find("slide_panel").Find("rotCamPerTurn_but");
            button.GetComponent<Image>().color = (rotate) ? new Color32(255, 255, 255, 255) : new Color32(255, 90, 65, 255);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (rotate) ? "ON" : "OFF";
        }

        public void PlayGame()
        {
            animator.SetTrigger(triPlayGame);
            Invoke("ToPlayScene", 3f);
        }

        void ToPlayScene()
        {
            Dictionary<string, bool> options = new Dictionary<string, bool>() { { "highlight", highlight }, { "rotate", rotate } };
            SingletonGame.Instance().CreateBoard(8, 8, GameMode.Default, options);
            Hide();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        void Hide()
        {
            CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void SetBrightness (bool _isBright) { isBright = _isBright; }
    }
}
