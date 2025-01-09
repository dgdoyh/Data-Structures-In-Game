using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerStateMachine : StateMachine
{
    //[SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    // How fast player rotates
    [field: SerializeField] public float RotationDamping { get; private set; }
    // Weapon info
    [field: SerializeField] public WeaponDamage WeaponManager { get; private set; }
    // Different attacks (combo) of player
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public InputReader InputReader { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public Animator Animator { get; private set; }
    public Health Health { get; private set; }

    public Transform MainCameraTransform { get; private set; }


    private void Awake()
    {
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<Health>();

        MainCameraTransform = Camera.main.transform;

        // First (default) state is PlayerFreeLookState
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage(int damage)
    {
        //SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        //SwitchState(new PlayerDeadState(this));
    }
}
