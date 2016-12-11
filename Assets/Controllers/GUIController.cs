using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public GameObject containerPanel;
    Dictionary<string, string> buttons;

    public Color buttonPressedColor;


    GameObject activeButton = null;

    // Use this for initialization
	void Start () {
        SetPanelWidth();

        loadButtons();
        loadGUI();
	}

    private void loadButtons()
    {
        buttons = new Dictionary<string, string>();
        buttons.Add("Bulldoze", "bulldoze");
        buttons.Add("Wall", "wall");
    }

    


    private void loadGUI()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/button");

        foreach (KeyValuePair<string,string> item in buttons)
        {
            GameObject button = Instantiate<GameObject>(prefab);
            button.name = item.Key;
            button.transform.SetParent(containerPanel.transform);
            button.GetComponentInChildren<Text>().text = item.Key;
            string value = item.Value;
            button.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetBuildType(button, value); });
        }

    }



    private void SetBuildType(GameObject button,string type)
    {
        if (button==null || type==null)
        {
            Debug.Log("GUIController::SetBuildType - Button or type is null");
            return;
        }


       

        if (activeButton != null || activeButton == button)
        {
            activeButton.GetComponent<Image>().color = Helper.HexToColor("#ffffff");
            activeButton.GetComponentInChildren<Text>().color = Helper.HexToColor("#323232");
            activeButton = null;
        }
        else
        {
            activeButton = button;
            activeButton.GetComponent<Image>().color = buttonPressedColor;
            activeButton.GetComponentInChildren<Text>().color = Helper.HexToColor("#ffffff");
        }
        


        BuildController.Instance.SetBuildType(type);
    }

    // Update is called once per frame
    private void OnGUI()
    {
        
    }

    [ExecuteInEditMode]
    public void SetPanelWidth()
    {
        RectTransform rt = containerPanel.GetComponent<RectTransform>();
        Vector2 dimension = rt.sizeDelta;
        dimension.x = Screen.width;
        dimension.y = 120;
        rt.sizeDelta = dimension;
                
    }
}
