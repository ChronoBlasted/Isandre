using BaseTemplate.Behaviours;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Canvas _mainCanvas;

    [SerializeField] MenuView _menuView;
    [SerializeField] GameView _gameView;
    [SerializeField] EndView _endGameView;
    [SerializeField] LoadingView _loadingView;

    [Header("Black Shade Ref")]
    [SerializeField] Button _blackShadeButton;
    [SerializeField] Image _blackShadeImg;

    //[Header("Popup Ref")]
    //[SerializeField] InventoryPopup _inventoryPopup;

    View _currentView;

    public Canvas MainCanvas { get => _mainCanvas; }
    public MenuView MenuView { get => _menuView; }
    public GameView GameView { get => _gameView; }
    public EndView EndGameView { get => _endGameView; }
    public LoadingView LoadingView { get => _loadingView; }

    Tweener _blackShadeTweener;

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        InitView();
    }

    public void InitView()
    {
        _menuView.Init();
        _gameView.Init();
        _endGameView.Init();
        _loadingView.Init();

        HideBlackShade();
    }

    #region View

    public void ChangeView(View newPanel, bool _instant = false)
    {
        if (newPanel == _currentView) return;

        if (_currentView != null)
        {
            CloseView(_currentView);
        }

        _currentView = newPanel;

        _currentView.gameObject.SetActive(true);

        if (_instant) _currentView.OpenView(_instant);
        else _currentView.OpenView();

    }

    public void ChangeView(View newPanel)
    {
        if (newPanel == _currentView) return;

        if (_currentView != null)
        {
            CloseView(_currentView);
        }

        _currentView = newPanel;

        _currentView.gameObject.SetActive(true);
        _currentView.OpenView();
    }

    void CloseView(View newPanel)
    {
        newPanel.CloseView();
    }
    #endregion

    #region Popup

    public void AddPopup(Popup newPopup)
    {
        newPopup.OpenPopup();
    }
    #endregion

    #region GameState

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                HandleMenu();
                break;
            case GameState.GAME:
                HandleGame();
                break;
            case GameState.END:
                HandleEnd();
                break;
            case GameState.WAIT:
                HandleWait();
                break;
            default:
                break;
        }
    }

    void HandleMenu()
    {
        ChangeView(_menuView);
    }
    void HandleGame()
    {
        ChangeView(_gameView);
    }
    void HandleEnd()
    {
        ChangeView(_endGameView);
    }
    void HandleWait()
    {
    }


    #endregion

    public void ShowBlackShade(UnityAction _onClickAction)
    {
        if (_blackShadeTweener.IsActive()) _blackShadeTweener.Kill();

        _blackShadeTweener = _blackShadeImg.DOFade(.5f, .1f);

        _blackShadeImg.raycastTarget = true;

        _blackShadeButton.onClick.AddListener(_onClickAction);
    }

    public void HideBlackShade(bool _instant = true)
    {
        if (_blackShadeTweener.IsActive()) _blackShadeTweener.Kill();

        if (_instant) _blackShadeTweener = _blackShadeImg.DOFade(0f, 0);
        else _blackShadeTweener = _blackShadeImg.DOFade(0f, .1f);

        _blackShadeImg.raycastTarget = false;

        _blackShadeButton.onClick.RemoveAllListeners();
    }

    public static void ScrollToItem(ScrollRect scrollRect, Transform content, int indexToScroll)
    {
        float step = 1f / (content.childCount - 1);

        scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, step * indexToScroll);
    }

    public static string GetFormattedInt(float amount)
    {
        return amount.ToString("#,0");
    }
}
