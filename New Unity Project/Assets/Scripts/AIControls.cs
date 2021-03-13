using System;
using System.Collections;
using System.Linq;
using UnityEngine;

using JakeUtils;

[RequireComponent(typeof(TankController), typeof(Interval))]
public class AIControls : MonoBehaviour {
  TankController tank;
  Interval interval;

  Transform target;
  GameObject player;

  Coroutine checkRadiusCoroutine;

  public float safetyRadiusSqr = 5;

  void Awake() {
    tank = GetComponent<TankController>();
    Debug.Assert(
      null != tank,
      "AIControls requires TankController");

    interval = GetComponent<Interval>();
    Debug.Assert(
      null != interval,
      "AIControls requires Interval");
  }

  void OnEnable() {
    tank.playerVariable.Changed += OnPlayerChanged;
    interval.Elapsed += OnIntervalElapsed;
  }

  void OnDisable() {
    tank.playerVariable.Changed -= OnPlayerChanged;
    interval.Elapsed -= OnIntervalElapsed;
  }

  /// <summary>
  ///   Decides whether there is an unobstructed straight-line path
  ///   from the nozzle to the target.
  /// </summary>
  bool IsShotClear {
    get {
      RaycastHit hit; 
      var t = tank.cannonExit.transform;
      return
        Physics.Raycast(
          t.position + t.forward * 0.25f,
          t.forward,
          out hit)
        && hit.collider.transform == target;
    }
  }

  void CheckSafetyRadius() {
    target = null;

    foreach(var t in tank.bulletsVariable.Value.Select(obj => obj.transform)) {
      // confirm that the bullet is close to us
      var d = t.position - transform.position;
      if(d.sqrMagnitude >= safetyRadiusSqr)
        continue;
      
      // check that it's heading towards us
      RaycastHit hit;
      if(!Physics.Raycast(
           t.position + t.forward * 0.25f,
           t.forward,
           out hit)
        || hit.collider.gameObject != gameObject)
        continue;

      target = t;
      break;
    }

    if(target == null)
      target = player.transform;
  }

  IEnumerator CheckRadiusCoroutine() {
    while(true) {
      CheckSafetyRadius();
      yield return new WaitForSeconds(0.25f);
    }
  }

  void Start() {
    checkRadiusCoroutine = StartCoroutine(CheckRadiusCoroutine());
  }
      

  void OnIntervalElapsed(Interval interval) {
    // before shooting, we're gonna check that we have a clear shot to
    // the target
    // if(IsShotClear)
      tank.Shoot();
  }

  void OnPlayerChanged(GameObject player) {
    this.player = player;
    target = player.transform;
    interval.enabled = target != null;
  }

  Vector3 ProjectTargetPosition(Transform target) {
    var rb = target.GetComponent<Rigidbody>();
    if(rb == null) {
      Debug.Log("Can't project target position because it does not have a rigidbody.");
      return target.position;
    }

    var dp = transform.position - target.position;
    var v = rb.velocity;

    // if the velocity is too small, we can't reliably project it.
    if(v.sqrMagnitude < 0.1)
      return target.position;

    // var t = - (dp.x * dp.x + dp.z * dp.z) / 2f / (v.x * dp.x + v.z * dp.z);
    var t = transform.position.sqrMagnitude / (2 * Vector3.Dot(transform.position, v));
    if(t < 0)
      return target.position;
    
    var p = target.position + rb.velocity * t;

    Debug.DrawLine(transform.position, p);
    return p;
  }
  
  void Update() {
    if(null == target)
      return;
    
    tank.AimAt(ProjectTargetPosition(target));
  }
}
