using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : EnemyAbstract
{
    [SerializeField] protected PathMoving path;
    [SerializeField] protected Transform player;
    [SerializeField] protected Point currentPoint;
    [SerializeField] protected int currentPointIndex = 0;
    [SerializeField] protected float pointDistance = Mathf.Infinity;
    [SerializeField] protected float pointDistanceLimit = 1f;
    [SerializeField] protected float Distance = Mathf.Infinity;

    //[SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected bool isChasingPlayer = false;


    protected virtual void FixedUpdate()
    {
        this.Moving();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPathMoving();
        this.LoadPlayer();
    }

    protected virtual void LoadPathMoving()
    {
        if (this.path != null) return;
        this.path = GameObject.Find("PathMoving_1").GetComponent<PathMoving>();
        Debug.Log(transform.name + ": LoadPathMoving", gameObject);
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindObjectOfType<PlayerCtrl>().transform;
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void Moving()
    {
        this.IsTriggerPlayer();
        //this.LoadMovingStatus();
        this.IsAttack();

        //this.GetNextPoint();
    }

    protected virtual void IsTriggerPlayer()
    {
        if (isChasingPlayer)
        {
            if (player != null)
            {
                this.ctrl.Agent.SetDestination(this.player.transform.position);
                this.ctrl.Agent.speed = 3.5f;
            }
        }
        else
        {
            GetNextPoint();
            this.ctrl.Agent.SetDestination(this.currentPoint.transform.position);
            this.ctrl.Agent.speed = 2.5f;
        }
    }

    protected virtual void IsAttack()
    {
        this.Distance = Vector3.Distance(this.transform.position, player.position);

        if (Distance < this.pointDistanceLimit)
        {
            this.isMoving = this.ctrl.Agent.isStopped; //true
            this.ctrl.Animator.SetBool("isAttack", true);
            //this.ctrl.Animator.SetBool("isMoving", false);
        }
        else
        {
            this.isMoving = !this.ctrl.Agent.isStopped;
            this.ctrl.Animator.SetBool("isAttack", false);
            this.ctrl.Animator.SetBool("isMoving", true);
            //this.LoadMovingStatus();
        }
    }

    // protected virtual void LoadMovingStatus()
    // {
    //     this.isMoving = !this.ctrl.Agent.isStopped;
    //     this.ctrl.Animator.SetBool("isMoving", this.isMoving);
    // }


    protected virtual void OnTriggerEnter(Collider collider)
    {
        PlayerTargetable targetable = collider.GetComponent<PlayerTargetable>();
        if (targetable == null) return;
        this.player = collider.transform;
        isChasingPlayer = true;
        //this.Ctrl.Agent.SetDestination(PlayerCtrl.Instance.GetComponent<PlayerCtrl>().transform.position);
        Debug.Log(transform.name + ":Enter " + collider.name, collider.gameObject);
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        isChasingPlayer = false;
        //this.Ctrl.Agent.SetDestination(PlayerCtrl.Instance.GetComponent<PlayerCtrl>().transform.position);
        Debug.Log(transform.name + ":Exit " + collider.name, collider.gameObject);
    }

    protected virtual void GetNextPoint()
    {
        this.currentPoint = this.path.GetPoint(this.currentPointIndex);
        this.pointDistance = Vector3.Distance(this.currentPoint.transform.position, transform.position);
        if (this.pointDistance < this.pointDistanceLimit)
            this.currentPointIndex = Random.Range(0, path.Points.Count - 1);
        //if (this.currentPointIndex > this.path.Points.Count-1) this.isFinish = true;
    }
}