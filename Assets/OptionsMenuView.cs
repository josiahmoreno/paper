using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameBattleProvider Provider;
    public GameObject Content;
    void Start()
    {
        Provider.Battle.OptionsListMenu.OnShowing += OnHide;
        OnHide(Provider.Battle.OptionsListMenu.Showing);
    }

    private void OnHide(bool obj)
    {
        this.gameObject.SetActive(obj);
        foreach (Transform child in Content.transform) {
            GameObject.Destroy(child.gameObject);
        }

        if (obj)
        {
            for (int i = 0; i < Provider.Battle.OptionsListMenu.Items.Length; i++)
            {
                var item = Provider.Battle.OptionsListMenu.Items[i];
                
                var gameObject = new GameObject();
                var rect = gameObject.AddComponent<RectTransform>();
                var text = gameObject.AddComponent<Text>();
                if (item == Provider.Battle.OptionsListMenu.Active)
                {
                    //var image = gameObject.AddComponent<Image>();
                   //image.color = Color.black;
                   text.fontSize = 20;
                   text.fontStyle = FontStyle.BoldAndItalic;
                }
                text.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
                text.text = item.GetType().Name;
                gameObject.transform.SetParent(Content.transform);
                var oldPos = rect.localPosition;
                oldPos.z = 0;
                rect.localPosition = oldPos;
                rect.localScale = Vector3.one;
                rect.sizeDelta = new Vector2(70, 20);

            }
        }
        
          
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
