using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinScreen : AbstractUIScreen
{
    public event UnityAction NextButtonClick;

    protected override void OnButtonClick()
    {
        NextButtonClick?.Invoke();
    }
}
