using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverPanel : Panel
{
    
   public void OnClickRestartButton() {
       PanelManager.Instance.ClosePanel("GameoverPanel");
       PanelManager.Instance.ClosePanel("BlurPanel");

       Messenger.Broadcast(EventKey.RestartGame);
   }
}
