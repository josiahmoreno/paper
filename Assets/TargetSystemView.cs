using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class TargetSystemView : MonoBehaviour
{
    public GameBattleProvider Provider;

    private float width;
    private Dictionary<Enemy, EnemyView> map;
    private EnemyView _targetedEnemy;
    private GameObject _littleBlock;

    // Start is called before the first frame update
    void Start()
    {
        this.map = new Dictionary<Enemy, EnemyView>();
        Provider.Battle.TargetSystem.OnShowing += OnShowing;
        Provider.Battle.TargetSystem.ActiveChanged += ActiveChanged;
        OnShowing(Provider.Battle.TargetSystem.Showing);
        if (Provider.Battle.TargetSystem.Actives != null)
        {
            ActiveChanged(Provider.Battle.TargetSystem.Actives);
        }
        
        var enemiesCount =  Provider.Battle.Enemies.Count;
        this.width = this.GetComponent<RectTransform>().rect.width;
        
        for (int i = 0; i < enemiesCount; i++)
        {
            
            map.Add( Provider.Battle.Enemies[i],CreateEnemy(Provider.Battle.Enemies[i], i));
        }
    }

    private void ActiveChanged(Enemy[] obj)
    {
        if (_littleBlock != null)
        {
            Destroy(_littleBlock);
        }
        foreach (Enemy enemy in obj)
        {
            if (map.ContainsKey(enemy))
            {
                var gameObject = map[enemy]._gameObject;
                var littleBlock = new GameObject();
                var scale = littleBlock.transform.localScale;
                var rect = littleBlock.AddComponent<RectTransform>();
                littleBlock.AddComponent<Image>();
                
                rect.sizeDelta = new Vector2(5, 5);
                littleBlock.transform.SetParent(gameObject.transform);
                rect.localScale = scale;
                var pos =  new Vector2(0,70);
                rect.localPosition = pos;
                _targetedEnemy = map[enemy];
                _littleBlock = littleBlock;
                break;
            }
        }
    }

    private class EnemyView
    {
        public GameObject _gameObject;
        public Enemy _enemy;
        public Text text;

        public EnemyView(GameObject gameObject, Enemy enemy, Text text)
        {
            _gameObject = gameObject;
            _enemy = enemy;
            this.text = text;
            enemy.Health.OnHealthChange += (sender, i) =>
            {
                this.text.text = $"{enemy.ToString()}";
            };
        }
        
    }
    private EnemyView CreateEnemy(Enemy enemy, int index)
    {
        float spacing = width / Provider.Battle.Enemies.Count;
        var view = new GameObject();
        Vector3 scale = view.transform.localScale;
        view.name = enemy.GetType().Name;
        var rect = view.AddComponent<RectTransform> ();
        view.transform.SetParent(this.transform,false);
        rect.sizeDelta = new Vector2(100,100);
        var positon = (-width/2) + (spacing * (index + 1));
        //Debug.Log($"TargetSystemView {positon}, spacing {spacing}, index {{{index}}}, width {{{width}}}");
        rect.localPosition = new Vector3(positon,0,0);
        rect.transform.localScale = scale;
        var image = view.AddComponent<Image>();
        image.color = Color.white;
        var textObj = new GameObject();
        var text = textObj.AddComponent<Text>();
        text.font = Font.CreateDynamicFontFromOSFont("Arial",14);
        text.color = Color.black;
        text.text = enemy.ToString();
        textObj.transform.SetParent(view.transform);
        textObj.transform.localScale = Vector3.one;
        textObj.transform.localPosition = new Vector3(0, 0, 0);
        return new EnemyView( view,enemy,text);
    }

    private void OnShowing(bool obj)
    {
        this.gameObject.SetActive(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
