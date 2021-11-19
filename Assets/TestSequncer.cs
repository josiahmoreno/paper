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
using System.Threading;

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
        hero = battlerView.Hero;
        sequenceable = new UnitySequenceable(battlerView);
    }

    public void SetMovementTarget(BattlerView battlerView)
    {
        enemy = battlerView.Enemy;
        movementTarget = new UnitySequenceable(battlerView);
    }

    public void executeJumpSequence()
    {
        SetSequenceable(BattlefieldOrderer.BattlerViews[0]);
        SetMovementTarget(BattlefieldOrderer.BattlerViews[1]);
        Attacks.IAttack jump = (hero as Mario).Jumps[0];
        var damageTarget = new DamageTarget(hero, enemy, jump, new UnityQuicktime());
        jumpSequence = new JumpSequence(new PaperLogger());
        jumpSequence.Execute(sequenceable, movementTarget, damageTarget);
    }

    public void executeJump(ISequenceable mario, IPositionable goomba, IDamageTarget damageTarget)
    {
        jumpSequence = new JumpSequence(new PaperLogger());
        jumpSequence.Execute(sequenceable, movementTarget,damageTarget);
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

    public bool GetQuicktimeResult()
    {
        return quicktime.Getter();
    }
}
public class UnitySequenceable : ISequenceable
{

    private BattlerView battlerView;

    public UnitySequenceable(BattlerView battlerView)
    {
        this.battlerView = battlerView;

        //x = battlerView.transform.localPosition.x;
        //y = battlerView.transform.localPosition.y;
        //z = battlerView.transform.localPosition.z;
        
    }

    public float x
    {
        get => battlerView.transform.localPosition.x; set
        {
            Vector3 vector3 = battlerView.transform.localPosition;
            vector3.x = value;
            battlerView.transform.localPosition = vector3;
        }
    }
    public float y
    {
        get => battlerView.transform.localPosition.y; set
        {
            Vector3 vector3 = battlerView.transform.localPosition;
            vector3.y = value;
            battlerView.transform.localPosition = vector3;
        }
    }
    public float z
    {
        get => battlerView.transform.localPosition.z; set
        {
            Vector3 vector3 = battlerView.transform.localPosition;
            vector3.z = value;
            battlerView.transform.localPosition = vector3;
        }
    }

    public Action OnMoveComplete { get; set; }
   
    //{ get; set; }

    public IPositionable CopyPosition()
    {
        Debug.Log($"{this.GetType().Name} - copying position {{{x},{y},{z}}} but transform is {battlerView.transform.localPosition}");
        return new Position(x,y,z);
    }


    public void Jump(IPositionable p, Action jump)
    {
        Debug.Log($"TestSequrncer - 1jumping to {(p)}, from {battlerView.transform.localPosition}");
        Vector3 vec = (p as Position).Vector3();
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

    public void MoveTo(IPositionable p)
    {
        //Debug.Log($"{GetType().Name} {Guid.ToString().Substring(0, 4)} - dotween is moving to {p}");
        var core = battlerView.transform.DOLocalMove(new Vector3(p.x,p.y,p.z),1f);
        core.OnComplete(() => {
               
            //Debug.Log($"{GetType().Name} {Guid.ToString().Substring(0, 4)} dotween oncomplete");
            var OnMoveComplete2 = OnMoveComplete;
            OnMoveComplete = null;
            OnMoveComplete2?.Invoke();
        });
        //core.Complete();
    }


    public override string ToString()
    {
        return $"{{{GetType().Name} - {Guid.ToString().Substring(0, 4)} {{{x},{y},{z}}} ";
    }

    

    public void Post(SendOrPostCallback sendOrPostCallback, object v)
    {
        Debug.Log($"{GetType().Name} - Post");
        try
        {
            IEnumerator ExampleCoroutine()
            {
                //Print the time of when the function is first called.
                Debug.Log("Started Coroutine at timestamp : " + Time.time);

                //yield on a new YieldInstruction that waits for 5 seconds.
                yield return new WaitForSeconds(2);
                sendOrPostCallback.Invoke(v);
                //After we have waited 5 seconds print the time again.
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            battlerView.StartCoroutine(ExampleCoroutine());
        } catch(Exception e)
        {
            Debug.LogError(e);
        }
    
    }

    public void Wait(SendOrPostCallback sendOrPostCallback, object v)
    {
        Debug.Log($"{GetType().Name} - Post");
        try
        {
            IEnumerator ExampleCoroutine()
            {
                //Print the time of when the function is first called.
                Debug.Log("Started Coroutine at timestamp : " + Time.time);

                //yield on a new YieldInstruction that waits for 5 seconds.
                yield return new WaitForSeconds(2);
                sendOrPostCallback.Invoke(v);
                //After we have waited 5 seconds print the time again.
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            battlerView.StartCoroutine(ExampleCoroutine());
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}

/*
internal class UnityPositionFollower : IPositionable
{
    //public UnityMovementTarget(float x, float y, float z)
    //{
    //    this.x = x;
    //    this.y = y;
    //    this.z = z;
    //}
    public UnityPositionFollower(Transform transform)
    {
        this.transform = transform;

       //x= transform.localPosition.x;
       //y= transform.localPosition.y;
       //z= transform.localPosition.z;
        
    }
    //public UnityMovementTarget(Vector3 transform)
    //{

    //    x = transform.x;
    //    y = transform.y;
    //    z = transform.z;
        
    //}

    //public UnityMovementTarget(Tuple<float, float, float> transform)
    //{

    //    x = transform.Item1;
    //    y = transform.Item2;
    //    z = transform.Item3;
        
    //}


    //public static implicit operator Vector3(UnityMovementTarget d) => new Vector3(d.x, d.y, d.z);
    //public static explicit operator UnityMovementTarget(Vector3 b) => new UnityMovementTarget(b);

 

    private Guid Guid = Guid.NewGuid();
    private Transform transform;

    public float x
    {
        get => transform.localPosition.x; set
        {
            Vector3 vector3 = transform.localPosition;
            vector3.x = value;
            transform.localPosition = vector3;
        }
    }
    public float y {
        get => transform.localPosition.y; set
        {
            Vector3 vector3 = transform.localPosition;
            vector3.y = value;
            transform.localPosition = vector3;
        }
    }
    public float z {
        get => transform.localPosition.z; set
        {
            Vector3 vector3 = transform.localPosition;
            vector3.z = value;
            transform.localPosition = vector3;
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name} - {Guid.ToString().Substring(0,4)} {{{x},{y},{z}}}";
    }



    internal Vector3 Vector3()
    {
        return new Vector3(x, y, z);
    }

    public IPositionable CopyPosition()
    {
        return new Position(x, y, z);
    }
}
*/