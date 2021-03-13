using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using JakeUtils;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
  [Tooltip("Degrees per second")]
  public float turnSpeed = 45;

  [Tooltip("Units per second")]
  public float topSpeed = 5;

  [Tooltip("Newtons")]
  public float deceleration = -5;

  [Tooltip("Newtons")]
  public float acceleration = 2;

  public int projectileLimit = 5;

  public GameObject cannonShellPrefab;

  public GameObject cannonExit;

  public GameObjectVariable playerVariable;

  public GameObject explosionPrefab;

  public GameObjectListVariable bulletsVariable;

  ParticleSystem cannonExitParticle;

  Rigidbody rb;

  GunController gunController;

  /// <summary>
  ///   Raised when the tank it hit by a cannon shell.
  ///   Getting hit always destroys the tank, but
  /// </summary>
  public event Action<TankController> Hit;

  /// <summary>
  ///   The active projectiles of this tank.
  /// </summary>
  public List<CannonShell> projectiles = new List<CannonShell>();

  void Awake() {
    rb = GetComponent<Rigidbody>();
    Debug.Assert(
      null != rb,
      "TankController requires a rigidbody.");

    gunController = GetComponentInChildren<GunController>();
    Debug.Assert(
      null != gunController,
      "TankController must have a GunController on a child.");

    Debug.Assert(
      null != cannonExit,
      "A cannon exit must be specified.");

    Debug.Assert(
      null != cannonShellPrefab,
      "A prefab for cannon shells must be specified.");

    cannonExitParticle = cannonExit.GetComponent<ParticleSystem>();
    Debug.Assert(
      null != cannonExitParticle,
      "The cannon exit must have a particle system");
  }

  GameObject InstantiateProjectile() {
    return GameObject.Instantiate(
      cannonShellPrefab,
      cannonExit.transform.position,
      cannonExit.transform.rotation);
  }

  /// <summary>
  ///   Decides whether a projectile can be fired.
  /// </summary>
  public bool CanShoot() => projectiles.Count < projectileLimit;

  /// <summary>
  ///   Tries to shoot a projectile.
  /// </summary>
  /// <returns>
  ///   The CannonShell component of the instantiated GameObject.
  ///   `null` if the projectile cannot be created.
  /// </returns>
  public CannonShell Shoot() {
    if(!CanShoot())
      return null;

    var obj = InstantiateProjectile();
    var shell = obj.GetComponent<CannonShell>();
    Debug.Assert(
      null != shell,
      "Instantiated projectile must have the CannonShell component.");
    shell.Destroyed += OnProjectileDestroyed;
    projectiles.Add(shell);
    bulletsVariable.Value.Add(obj);
    cannonExitParticle.Emit(4);
    return shell;
  }

  void OnProjectileDestroyed(CannonShell shell) {
    projectiles.Remove(shell);
    bulletsVariable.Value.Remove(shell.gameObject);
  }

  void FixedUpdate() {
    LimitSpeed();
  }

  void OnCollisionEnter(Collision collision) {
    var obj = collision.gameObject;

    {
      var shell = obj.GetComponent<CannonShell>();
      if(null != shell) {
        Hit?.Invoke(this);
        Explode();
        GameObject.Destroy(gameObject);
      }
    }
  }

  void Explode() {
    var obj = GameObject.Instantiate(explosionPrefab);
    obj.transform.position = transform.position;
  }

  public void AimAt(Vector3 floorPoint) {
    gunController.AimAt(floorPoint);
  }

  public void Forward() {
    Accelerate(acceleration);
  }

  public void Backward() {
    Accelerate(deceleration);
  }

  public void Right() {
    rb.AddTorque(0, turnSpeed, 0);
  }

  public void Left() {
    rb.AddTorque(0, -turnSpeed, 0);
  }

  void Accelerate(float amount) {
    rb.AddRelativeForce(Vector3.forward * amount);
  }

  void LimitSpeed() {
    rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
  }
}
