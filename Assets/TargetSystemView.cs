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
            }
        }
    }

    private class EnemyView
    {
        public GameObject _gameObject;
        public Enemy _enemy;

        public EnemyView(GameObject gameObject, Enemy enemy)
        {
            _gameObject = gameObject;
            _enemy = enemy;
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
        return new EnemyView( view,enemy);
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
