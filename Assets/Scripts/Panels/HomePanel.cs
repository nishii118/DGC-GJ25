using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : Panel
{
    public void OnClickStartButton() {
        // PanelManager.Instance.ClosePanel("HomePanel");
        Messenger.Broadcast(EventKey.STARTGAME);
    }
}
