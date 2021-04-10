using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class EnemyViewHolder
{
    public GameObject _gameObject;
    public Enemy _enemy;
    public Text text;

    public EnemyViewHolder(GameObject gameObject, Enemy enemy, Text text)
    {
        _gameObject = gameObject;
        _enemy = enemy;
        this.text = text;
    }
}