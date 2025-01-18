using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : Panel
{
    public void OnClickPauseButton() {
        Time.timeScale = 0;
        PanelManager.Instance.OpenPanel("BlurPanel");
        PanelManager.Instance.OpenPanel("PausePanel");
    }
}
