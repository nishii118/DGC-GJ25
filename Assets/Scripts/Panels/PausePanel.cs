using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    public void OnClickResumeButton()
    {
        PanelManager.Instance.ClosePanel("PausePanel");
        PanelManager.Instance.ClosePanel("BlurPanel");

        Time.timeScale = 1;
    }

    public void OnClickHomeButton()
    {
        Messenger.Broadcast(EventKey.ONGOHOME);
    }
}
