using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaperLib.Sequence;
using Scenes.BattlefieldOrderer;
using System;
using DG.Tweening;
using Heroes;
using Enemies;
using Attacks;

public class TestSequncer : MonoBehaviour
{
    private IEntity hero;
    private ISequenceable sequenceable;
    private IEntity enemy;
    private ISequenceable movementTarget;
    private JumpSequence jumpSequence;
    [SerializeField]
    BattlefieldOrderer BattlefieldOrderer;

    // Start is called before the first frame update
    void Start()
    {
        //jumpSequence = new JumpSequence(new Logger());
       
    }

    public class PaperLogger : PaperLib.ILogger
    {
        public void Log(string v)
        {
            Debug.Log(v);
        }
    }
    public void SetSequenceable(BattlerView battlerView)
    {
        hero = battlerView.Enemy;
        sequenceable = new UnitySequenceable(battlerView);
    }

    public void SetMovementTarget(BattlerView battlerView)
    {
        enemy = battlerView.Hero;
        movementTarget = new UnitySequenceable(battlerView);
    }

    public void executeJumpSequence()
    {
        SetSequenceable(BattlefieldOrderer.BattlerViews[1]);
        SetMovementTarget(BattlefieldOrderer.BattlerViews[0]);
        Attacks.IAttack jump = (hero as NewGoomba).Moves[0];
        var damageTarget = new DamageTarget(hero, enemy, jump, new UnityQuicktime());
        jumpSequence = new JumpSequence(new PaperLogger(), damageTarget);
        jumpSequence.Execute(sequenceable, movementTarget);
    }

    public void executeJump(ISequenceable mario, IMovementTarget goomba, IDamageTarget damageTarget)
    {
        jumpSequence = new JumpSequence(new PaperLogger(), damageTarget);
        jumpSequence.Execute(sequenceable, movementTarget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class DamageTarget : IDamageTarget
{
    public IEntity hero { get; }
    public IEntity target { get; }
    public IAttack Attack { get; }
   
    private IQuicktime quicktime { get; }

    public Func<bool> successfulQuicktime => quicktime.Getter;

    public DamageTarget(IEntity hero, IEntity enemy, IAttack jump, IQuicktime unityQuicktime)
    {
        this.hero = hero;
        this.target = enemy;
        this.Attack = jump;
        this.quicktime = unityQuicktime;
    }
}
public class UnitySequenceable : ISequenceable
{
    private BattlerView battlerView;

    public UnitySequenceable(BattlerView battlerView)
    {
        this.battlerView = battlerView;
        Position = new Tuple<float, float, float>(
        battlerView.transform.localPosition.x,
        battlerView.transform.localPosition.y,
        battlerView.transform.localPosition.z
        );
    }

    public Tuple<float, float, float> Position { get; set; }

    public Action OnMoveComplete { get; set; }

    public IMovementTarget CopyPosition()
    {
        return new UnityMovementTarget(battlerView.transform);
    }


    public void Jump(IMovementTarget p, Action jump)
    {
        Debug.Log($"TestSequrncer - 1jumping to {(p as UnityMovementTarget)}, from {battlerView.transform.localPosition}");
        Vector3 vec = (p as UnityMovementTarget).Vector3();
        Debug.Log($"TestSequrncer - 2jumping to {vec}"); 
        var seq = battlerView.transform.DOLocalJump(vec, 100f, 1, 1);
      
        seq.OnComplete(() =>
        {
            try
            {

            jump();
            } catch(Exception e)
            {
                Debug.LogError(e);
            }
        });
    }

   

    private Guid Guid = Guid.NewGuid();

    public void MoveTo(IMovementTarget p)
    {
        Debug.Log($"{GetType().Name} {Guid.ToString().Substring(0, 4)} - dotween is moving to {p}");
        var core = battlerView.transform.DOLocalMove(new Vector3(p.Position.Item1,p.Position.Item2,p.Position.Item3),1f);
        core.OnComplete(() => {
               
            Debug.Log($"{GetType().Name} {Guid.ToString().Substring(0, 4)} dotween oncomplete");
            var OnMoveComplete2 = OnMoveComplete;
            OnMoveComplete = null;
            OnMoveComplete2?.Invoke();
        });
        //core.Complete();
    }

    public void StartAnimation()
    {
        throw new NotImplementedException();
    }

    public IMovementTarget Copy()
    {
        return CopyPosition();
    }

    public override string ToString()
    {
        return $"{{{GetType().Name} - {Guid.ToString().Substring(0, 4)} {Position.Item1},{Position.Item2},{Position.Item3}}}";
    }
}

internal class UnityMovementTarget : IMovementTarget
{
    //public Vector3 transform;

    public UnityMovementTarget(Transform transform)
    {
       
        this.Position = new Tuple<float, float, float>(
        transform.localPosition.x,
        transform.localPosition.y,
        transform.localPosition.z
        );
    }
    public UnityMovementTarget(Vector3 transform)
    {
        this.Position = new Tuple<float, float, float>(
        transform.x,
        transform.y,
        transform.z
        );
    }


    public static implicit operator Vector3(UnityMovementTarget d) => new Vector3(d.Position.Item1, d.Position.Item2, d.Position.Item3);
    public static explicit operator UnityMovementTarget(Vector3 b) => new UnityMovementTarget(b);

    public Tuple<float, float, float> Position { get;  set; }
 

    private Guid Guid = Guid.NewGuid();
    public override string ToString()
    {
        return $"{GetType().Name} - {Guid.ToString().Substring(0,4)} {Position.Item1},{Position.Item2},{Position.Item3}";
    }

    public IMovementTarget Copy()
    {
        return new UnityMovementTarget(new Vector3(Position.Item1, Position.Item2, Position.Item3));
    }

    internal Vector3 Vector3()
    {
        return new Vector3(Position.Item1, Position.Item2, Position.Item3);
    }
}