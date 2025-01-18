using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool destroyOnClose = true;
    [SerializeField] private bool isDynamicPanel = true;
    public virtual void Open()
    {
        gameObject.SetActive(true);

        if(isDynamicPanel) {
            Messenger.Broadcast(EventKey.StopPlaying);
        }
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
        if (destroyOnClose)
        {
            PanelManager.Instance.RemovePanel(name);
            Destroy(gameObject);
        }

        if (isDynamicPanel) {
            Messenger.Broadcast(EventKey.ResumePlaying);
        }
    }

    public virtual void ClickButton() {
        Messenger.Broadcast(EventKey.ONCLICKBUTTON, "Click");
    }
}