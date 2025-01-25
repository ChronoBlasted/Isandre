using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToolsPopup : Popup
{
    [SerializeField] Image icoArrow;
    bool isOpen = false;

    public override void OpenPopup()
    {
        transform.DOMoveX(736, .2f).SetEase(Ease.OutBack);

        icoArrow.transform.DORotate(new Vector3(0, 0, 0), .2f);
    }

    public override void ClosePopup()
    {
        transform.DOMoveX(0, .2f).SetEase(Ease.InBack);

        icoArrow.transform.DORotate(new Vector3(0, 0, 180), .2f);
    }

    public void HandleOpenCloseButton()
    {
        isOpen = !isOpen;

        if (isOpen) OpenPopup();
        else ClosePopup();
    }
}
