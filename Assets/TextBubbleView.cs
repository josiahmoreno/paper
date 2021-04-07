using Battle;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextBubbleView : MonoBehaviour
{
    public GameBattleProvider Provider;
    public Text Text;

    public PlayerInput PlayerInput;

    private ITextBubbleSystem _battleTextBubbleSystem;

    // Start is called before the first frame update
    private void Start()
    {
        _battleTextBubbleSystem = Provider.Battle.TextBubbleSystem;
        _battleTextBubbleSystem.OnShowing += OnShowing;
        _battleTextBubbleSystem.OnText += OnText;

        OnText(_battleTextBubbleSystem.CurrentText);
        OnShowing(_battleTextBubbleSystem.Showing);
      
    }

    

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnText(string obj)
    {
        Text.text = obj;
    }

    private void OnShowing(bool obj)
    {
        gameObject.SetActive(obj);
    }
}