
using UnityEngine;

public class AITTarget : STANDSelectableMono, IPooling {
    [Header("Identifiers")]
    [SerializeField] bool isRoot;
    [SerializeField] int alliance;
    [SerializeField] bool _autoUpdateTrackingWhenTargetExists = true;
    [SerializeField] string _poolingGroupTag="fillout";
    [SerializeField] bool _isTargetableByOthers = false;

    // properties
    public int Alliance { get => alliance; }
    public bool HasTarget { get => _activeTarget && !_activeTarget.Destroyed; }

    /// <summary>
    /// When target exists, this is automatically updated to target pos.
    /// </summary>
    public Vector2 ActiveTrackPos { get => _activeTargetPos; }
    public string PoolingGroupTag { get => _poolingGroupTag; }
    public bool Destroyed { get => _destroyed; }

    // instance data
    AITTarget _activeTarget;
    Vector2 _activeTargetPos;
    private bool _destroyed;

    protected override void OnIsLockedChange(bool isLocked)
    {
        base.OnIsLockedChange(isLocked);
        if (isLocked)
        {
            if (_isTargetableByOthers)
                this.GetUniqueClass<AITGlobalTracking>().UnRegisterAITTarget(this);
            UnRegisterFromDrag();
        }
        else
        {
            if (_isTargetableByOthers)
                this.GetUniqueClass<AITGlobalTracking>().RegisterAITTarget(this);
            RegisterToDrag();
        }
    }

    protected override void Preloader()
    {
        base.Preloader();
        // first time: on locked change doesn't get triggered on init.
        this.GetUniqueClass<AITGlobalTracking>().RegisterAITTarget(this);
    }

    protected override void OnIsUnlockedUpdate()
    {
        base.OnIsUnlockedUpdate();
        // ensures target pos matches target
        if (_autoUpdateTrackingWhenTargetExists && _activeTarget != null)
            if (_activeTarget.isRoot)
                _activeTargetPos = _activeTarget.LocalPos;
            else _activeTargetPos = _activeTarget.WorldPos;
    }

    public void AssignTarget(AITTarget other)
    {
        this._activeTarget = other;

        if (_activeTarget.isRoot)
            _activeTargetPos = _activeTarget.LocalPos;
        else _activeTargetPos = _activeTarget.WorldPos;
    }

    public void AITMove(Vector2 pos)
    {
        _activeTarget = null;
        _activeTargetPos = pos;
    }

    internal void AutoSeekFirstEnemy()
    {
        this.GetUniqueClass<AITGlobalTracking>().LoadEnemiesAsTracked(Alliance);
        AITTarget target = 
            this.GetUniqueClass<AITGlobalTracking>().GetByIdUnderMask(0);
        if (target)
            AssignTarget(target.SelectionSource.GetComponent<AITTarget>());
        else
        {
            Debug.Log("Auto seek result is null.. All targets are destroyed.");
        }
    }

    public void OnPooledReady()
    {
        // .
        Debug.Log("pool ready "+gameObject, this);
        IsLocked = false;
        _destroyed = false;
    }

    public void OnPooledStandby()
    {
        Debug.Log("pool destroy" + gameObject, this);
        IsLocked = true;
        _destroyed = true;
    }
}
