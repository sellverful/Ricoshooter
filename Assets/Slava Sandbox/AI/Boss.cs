using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace RunningRiot
{
    public class Boss : MonoBehaviour
    {
        public float health = 300;
        public float currentHealth;
        private float myWidth;
        [HideInInspector]
        public enum State { Patrol, Chase, Hit, Stand, Flying };
        [HideInInspector]
        public enum HitStates { Dash, Getsuga };
        [HideInInspector]
        public enum Phase { First, Second, Third };

        public State currState = State.Stand;
        public Phase currPhase = Phase.First;
        public HitStates currHitState = HitStates.Dash;
        private Transform player;
        private NavMeshAgent agent;
        private Animator anim;
        public bool facingRight;
        public float damage = 50f;
        private WeaponHit weapon;
        [SerializeField]
        private bool lockHit = false;
        public float aggroDistance = 20f;
        public float aggroHeightDistance = 10f;
        private bool isGrounded = true;
        [SerializeField]
        private string currentTrigger = "";
        public GameObject particleSystem;
        private bool animationOnce = false;
        private bool onceDashLongWait;
        public int multiplier = 1;
        public int howManyDashes = 3;
        public GameObject getsugaGameobject;
        public float basicMovementSpeed = 3.5f;
        public float fastMovementSpeed = 15f;

        int hp = 5;

        // Use this for initialization
        void Start()
        {
            currentHealth = health;
            particleSystem.SetActive(false);
            //this.tag = "Enemy";
            weapon = GetComponentInChildren<WeaponHit>();
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            if (transform.rotation.y < 0)
            {
                facingRight = false;
            }
            else
            {
                facingRight = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!gotHit)
                IsGrounded();
            AnimationStates();
            SwitchPhases();
            Phases();
            Damaging();
        }
        void Damaging()
        {
            if (Input.GetButtonUp("Fire1"))
            {
                //  RecieveDamageFromPlayer(50f);
            }
        }
        private void SwitchPhases()
        {
            if (currentHealth > health / 2)
            {
                currPhase = Phase.First;
            }
            else if (currentHealth < health / 2 && currPhase != Phase.Third)
            {
                currPhase = Phase.Second;
            }
        }
        bool burnTheGroundOnce = false;
        void Phases()
        {
            switch (currPhase)
            {
                case Phase.First:
                    States();
                    break;
                case Phase.Second:
                    agent.enabled = false;
                    if (!burnTheGroundOnce)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
                        BurnTheGround();
                        burnTheGroundOnce = true;
                    }
                    break;
                case Phase.Third:
                    agent.enabled = true;
                    States();
                    break;
            }
        }
        void BurnTheGround()
        {
            GetComponent<SpawnRainFire>().enabled = true;
        }
        void IsGrounded()
        {
            if (Physics.Raycast(transform.position, -Vector3.up, 0.1f) && currState != State.Hit)
            {
                agent.speed = basicMovementSpeed;
                isGrounded = true;
                particleSystem.SetActive(false);
            }
            else
            {
                agent.speed = fastMovementSpeed * multiplier;
                //isGrounded = false;
                particleSystem.SetActive(true);
            }
        }
        State currentStateIf;
        private bool gotHit;

        void AnimationStates()
        {
            if (currState != currentStateIf)
            {
                currentStateIf = currState;
                switch (currState)
                {
                    case State.Stand:
                        SetTrigger("Idle");
                        break;
                    case State.Chase:
                        SetTrigger("Run");
                        break;
                }
            }

        }
        void States()
        {
            switch (currState)
            {
                case State.Stand:
                    if (lockHit) currState = State.Hit;
                    agent.isStopped = true;
                    if (Vector3.Distance(transform.position, player.position) < aggroDistance)
                    {
                        currState = State.Chase;
                        agent.isStopped = false;
                    }
                    break;
                case State.Chase:
                    agent.stoppingDistance = 1f;
                    agent.SetDestination(player.position);
                    if (Vector3.Distance(transform.position, player.position) < aggroDistance && isGrounded && !lockHit)
                    {
                        currState = State.Hit;
                    }
                    break;
                case State.Hit:
                    if (!lockHit)
                    {
                        lockHit = true;
                        switch (currHitState)
                        {
                            case HitStates.Dash:
                                StartCoroutine(HitPlayer());
                                break;
                            case HitStates.Getsuga:
                                StartCoroutine(Getsuga());
                                break;

                        }
                    }
                    transform.LookAt(player);
                    break;
            }
        }
        void SetTrigger(string trigger)
        {
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Attack");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(trigger)) return;
            anim.SetTrigger(trigger);
        }
        bool CheckReachablePoint(Vector3 destination)
        {
            return NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, new NavMeshPath());
        }
        IEnumerator HitPlayer()
        {
            onceDashLongWait = false;
            for (int i = 0; i < howManyDashes; i++)
            {
                StartCoroutine(Dash());
                yield return Dash();
            }
            agent.speed = basicMovementSpeed;
            currState = State.Chase;
            yield return new WaitForSeconds(5f / multiplier);
            //currHitState = HitStates.Getsuga;
            lockHit = false;
        }
        IEnumerator Dash()
        {
            SetTrigger("Idle");
            agent.speed = 0f;
            agent.isStopped = true;
            agent.SetDestination(player.position);
            if (!onceDashLongWait)
            {
                yield return new WaitForSeconds(1f);
                onceDashLongWait = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

            agent.isStopped = false;
            SetTrigger("Attack");
            agent.speed = fastMovementSpeed * multiplier;
            agent.SetDestination(player.position);

            yield return new WaitForSeconds(1f);
        }
        IEnumerator Getsuga()
        {

            for (int i = 0; i < howManyDashes; i++)
            {
                SetTrigger("Idle");
                agent.speed = 0f;
                agent.isStopped = true;
                yield return new WaitForSeconds(1f);
                agent.SetDestination(player.position);
                SetTrigger("Attack");
                StartCoroutine(ThrowGetsuga());
                yield return ThrowGetsuga();
            }
            yield return new WaitForSeconds(1f);
            agent.isStopped = false;
            agent.speed = basicMovementSpeed;
            currState = State.Chase;
            yield return new WaitForSeconds(5f / multiplier);
            currHitState = HitStates.Dash;
            lockHit = false;
        }
        IEnumerator ThrowGetsuga()
        {
            if (getsugaGameobject == null)
            {
                GameObject placeholder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                placeholder.AddComponent<Getsuga>();
                placeholder.transform.parent = null;
                placeholder.transform.position = transform.position;
                placeholder.transform.rotation = transform.rotation;
            }
            else
            {
                Instantiate(getsugaGameobject, transform, true);

            }
            yield return new WaitForSeconds(1f);
        }
        void TurnOnHitbox()
        {
            weapon.GetComponent<Collider>().enabled = true;
            Debug.Log("------------EnabledCollider");
        }
        void TurnOffHitbox()
        {
            weapon.GetComponent<Collider>().enabled = false;
            Debug.Log("------------DisanabledCollider");
        }
        void Damage()
        {
            player.SendMessage("RecieveDamageFromEnemy", 25);
            weapon.GetComponent<Collider>().enabled = false;
        }
        void RecieveDamageFromPlayer(float damage)
        {

            currentHealth -= damage;
            StartCoroutine(GotHit());
            //Destroy(this.gameObject);
        }
        IEnumerator GotHit()
        {
            gotHit = true;
            float currAgentSpeed = agent.speed;
            agent.speed = 0f;
            yield return new WaitForSeconds(2f);
            agent.speed = currAgentSpeed;
            gotHit = false;
        }
    }

}
