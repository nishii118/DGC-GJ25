using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public Dictionary<string, Panel> panels = new();
    private void Start()
    {
        var list = GetComponentsInChildren<Panel>();
        foreach (var panel in list)
        {
            // Debug.Log(panel.name);
            panels[panel.name] = panel;
        }
    }
    public Panel GetPanel(string name)
    {
        if (IsExisted(name))
        {
            return panels[name];
        }
        //Load panel len tu resources
        // Debug.Log("get panel");
        Panel panel = Resources.Load<Panel>("Panel/" + name);
        // Debug.Log("panel: " + panel.name);
        Panel newPanel = Instantiate(panel, transform);
        newPanel.name = name;
        newPanel.gameObject.SetActive(false);
        
        panels[name] = newPanel;
        return newPanel;
    }
    public void RemovePanel(string name)
    {
        if (IsExisted(name))
        {
            panels.Remove(name);
        }
    }
    public void OpenPanel(string name)
    {
        Panel panel = GetPanel(name);
        panel.Open();
    }
    public void ClosePanel(string name)
    {
        Panel panel = GetPanel(name);
        panel.Close();
    }
    public void CloseAll()
    {
        List<Panel> panelList = new List<Panel>(panels.Values);

        foreach (var panel in panelList)
        {
            panel.Close();
        }

    }
    private bool IsExisted(string name)
    {
        return panels.ContainsKey(name);
    }

}