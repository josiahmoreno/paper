using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MenuData;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameBattleProvider Provider;
    public GameObject Content;
    private List<OptionItemView> listOfGameObjects;
    private OptionItemView selectedView;

    void Start()
    {
        Provider.Battle.OptionsListMenu.OnShowing += OnHide;
        Provider.Battle.OptionsListMenu.OnActiveChanged += OptionsListMenuOnOnActiveChanged;
        OnHide(Provider.Battle.OptionsListMenu.Showing);
    }

    private void OptionsListMenuOnOnActiveChanged(object sender, IOption e)
    {
        Debug.Log($"OptionListMenu active change");
        if (!this.gameObject.activeSelf)
        {
            return;
        }
        if (selectedView != null)
        {
            selectedView.text.fontSize = 14;
            selectedView.text.fontStyle = FontStyle.Normal;
            selectedView.selected = false;
        }

        if (e == null)
        {
            return;
        }
            var newOptionView =  this.listOfGameObjects.First(option => option.option == e);
            newOptionView.selected = true;
            newOptionView.text.fontSize = 20;
            newOptionView.text.fontStyle = FontStyle.BoldAndItalic;
            selectedView = newOptionView;



    }

    private void OnHide(bool obj)
    {
        this.gameObject.SetActive(obj);
        foreach (Transform child in Content.transform) {
            GameObject.Destroy(child.gameObject);
        }

        this.listOfGameObjects = new List<OptionItemView>();
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
                text.text = item.Name;
                gameObject.transform.SetParent(Content.transform);
                var oldPos = rect.localPosition;
                oldPos.z = 0;
                rect.localPosition = oldPos;
                rect.localScale = Vector3.one;
                rect.sizeDelta = new Vector2(160, 20);
                var OptionView = new OptionItemView(gameObject, text,item == Provider.Battle.OptionsListMenu.Active, item);
                if (item == Provider.Battle.OptionsListMenu.Active)
                {
                    this.selectedView = OptionView;
                }
                listOfGameObjects.Add(OptionView);
            }
        }
        
          
        
    }

    private class OptionItemView
    {
        public GameObject obj;
        public Text text;
        public bool selected = false;
        public readonly IOption option;

        public OptionItemView(GameObject obj, Text text, bool selected, IOption option)
        {
            this.obj = obj;
            this.text = text;
            this.selected = selected;
            this.option = option;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
