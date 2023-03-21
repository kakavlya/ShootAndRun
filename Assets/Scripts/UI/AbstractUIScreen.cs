using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractUIScreen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button Button;

    public void Close()
    {
        gameObject.SetActive(false);
        CanvasGroup.alpha = 0;
        Button.interactable = false;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        CanvasGroup.alpha = 1f;
        Button.interactable = true;
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}
