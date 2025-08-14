using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public static EnemyHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Preferences")]
    [SerializeField] float maxTimeGrannyChaseCat = 15f;
    [SerializeField] float stopDistanceBeforeCat = 1.5f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float maxBodyConstraintToCat = 1f;
    [SerializeField] private float soundInterval = 5f;
    [SerializeField] float maxDistance = 2f;

    [Header("Components")]
    [SerializeField] NewObjectiveManager newobjecttiveManager;
    [SerializeField] GameObject WanderingChildGameObject;
    [SerializeField] Transform catTransform;
    [SerializeField] AimConstraint headAimConstraint;
    [SerializeField] RotationConstraint bodyRotationConstraint;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject catDeathAnimGameobject;
    [SerializeField] GameObject DogDeathAnimGameobject;
    [SerializeField] GameObject dangerMusicWhenChasingCat;
    [SerializeField] GameObject mainMusicGameObject;
    [SerializeField] GameObject bloodOverlayUI;
    [SerializeField] RuntimeAnimatorController oldController;
    [SerializeField] EnemyWandering enemyWandering;
    [SerializeField] GameObject[] grannyBats;

    [Header("Animation Parameter Strings")]
    [SerializeField] string animAttackBool = "isAttack";
    [SerializeField] string animAngerTrigger = "Anger";
    [SerializeField] string animReturnTrigger = "Return";
    [SerializeField] string animAfterSpecial = "NowAnger";
    [SerializeField] string cameraAnimShakeTrigger = "Shake";

    [Header("Events")]
    public UnityEvent OnAttackStart;
    public static event Action OnGrannyNear;
    public static event Action GrannyAboutToAttack;

    private Animator m_Animator;
    public NavMeshAgent m_Agent;
    public bool isChasingCat;
    private float chaseTimer;
    public static bool canAttackCat;


    int selectedIndexForCat;
    int selectedIndexForGranny;

    int roundOffIntForChaseTime ;

    [Space(10)]
    [SerializeField] Text safeText;

    #region UNITY FUNCTIONS

    private void OnEnable()
    {
        selectedIndexForCat = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        selectedIndexForGranny = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
    }

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        FirstObjective.OnChangeChannel += TriggerAngerGranny;
        PickableObject.OnObjectHitGranny += TriggerAngerGranny;
        PunchingGlove.OnPunchGranny += PunchedGranny;
        CurrentBullet.OnCurrentBulletHitGranny += CurrentBullet_OnCurrentBulletHitGranny;
        FireBullet.OnFireBulletHitGranny += FireBullet_OnFireBulletHitGranny;
        BeeBullet.OnBeeBulletHitGranny += BeeBullet_OnBeeBulletHitGranny;

        

        // AJ
        m_Animator = this.gameObject.GetComponent<Animator>();
        m_Agent = this.gameObject.GetComponent<NavMeshAgent>();


        if (WanderingChildGameObject != null)
        {
            Debug.Log("wandering gameobject is preset");
        }
        else
        {
            Debug.LogWarning("Wandering gameobject is not present");
        }

    }

    

    private void OnDestroy()
    {
        FirstObjective.OnChangeChannel -= TriggerAngerGranny;
        PickableObject.OnObjectHitGranny -= TriggerAngerGranny;
        PunchingGlove.OnPunchGranny -= PunchedGranny;
        CurrentBullet.OnCurrentBulletHitGranny -= CurrentBullet_OnCurrentBulletHitGranny;
        FireBullet.OnFireBulletHitGranny -= FireBullet_OnFireBulletHitGranny;
        BeeBullet.OnBeeBulletHitGranny -= BeeBullet_OnBeeBulletHitGranny;
    }



    private void Update()
    {
        if (isChasingCat)
        {
            if (dangerMusicWhenChasingCat.activeSelf == false)
            {
                dangerMusicWhenChasingCat.SetActive(true);
                mainMusicGameObject.SetActive(false);
            }
            ChaseCat();
        }
        // Check if Granny is within 5 units of the cat
        if (Vector3.Distance(transform.position, catTransform.position) <= detectionRadius)
        {
            OnGrannyNear?.Invoke();  // Invoke the event if Granny is near the cat
        }
        HeadConstraint();
    }

    #endregion

    #region HELPER FUNCTIONS

    void HeadConstraint()
    {
        if (catTransform == null || headAimConstraint == null)
            return;

        float distance = Vector3.Distance(transform.position, catTransform.position);

        // Calculate weight based on distance (closer = higher weight)
        float weight = Mathf.Clamp01(1f - (distance / maxDistance));

        headAimConstraint.weight = weight;
    }

    

    public void TriggerAngerGranny()
    {
        //Debug.Log("Granny is angry");
        m_Animator.runtimeAnimatorController = oldController;
        StartChasingCat();
    }

    public void PunchedGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Punch");
    }

    private void CurrentBullet_OnCurrentBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Current");
        m_Agent.isStopped = true;
    }

    private void FireBullet_OnFireBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Fire");
        m_Agent.isStopped = true;
    }

    private void BeeBullet_OnBeeBulletHitGranny()
    {
        m_Animator.runtimeAnimatorController = oldController;
        m_Animator.SetTrigger("Bee");
        m_Agent.isStopped = true;
    }
    public void Btn_call()
    {
        TriggerAngerGranny();
    }

    public void StartChasingCat()
    {
        NewObjectiveManager.Instance.GranniesBats(true);
        m_Animator.ResetTrigger(animAfterSpecial);
        m_Animator.SetBool("Wander", false);
        SFX_Manager.PlaySound(SFX_Manager.Instance.angryTalkGranny);
        SFX_Manager.PlaySound(SFX_Manager.Instance.OnDangerSounds, 0.5f);
        isChasingCat = true;
        if (m_Agent.enabled && m_Agent.isOnNavMesh)
        {
            m_Agent.isStopped = false;
        }
        chaseTimer = 0f;
        if (m_Animator != null)
        {
            m_Animator.SetTrigger(animAngerTrigger);
        }
        else
        {
            Debug.Log("Animator is missing");
        }
        if (WanderingChildGameObject != null)
        {
            WanderingChildGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Wandering gameobject is not present");
        }
    }



    private void ChaseCat()
    {
        if (!isChasingCat) return;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(catTransform.position, out hit, 5f, NavMesh.AllAreas))
        {
            m_Agent.SetDestination(hit.position);
        }
        else
        {
            m_Agent.ResetPath();
        }

        chaseTimer += Time.deltaTime;

        float distanceToCat = Vector3.Distance(transform.position, catTransform.position);

        if (distanceToCat <= stopDistanceBeforeCat)
        {
            canAttackCat = true;
            GrannyAboutToAttack?.Invoke();
            AttackCat();
        }
        else if (chaseTimer >= maxTimeGrannyChaseCat)
        {
            NewObjectiveManager.Instance.GranniesBats(false);
            ResetState();
            WanderingChildGameObject.SetActive(true);
        }
    }


    private void AttackCat()
    {
        NewObjectiveManager.Instance.GranniesBats(true);
        SFX_Manager.PlaySound(SFX_Manager.Instance.GrannyAngerNewspaper, 1f);

        bloodOverlayUI.SetActive(true);
       
       
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        isChasingCat = false;
        m_Agent.isStopped = true;
        OnAttackStart?.Invoke();
        bodyRotationConstraint.constraintActive = true;
        bodyRotationConstraint.weight = maxBodyConstraintToCat;


        if (canAttackCat)
        {
            if (IsCatInDetectionRadius())
            {
                m_Animator.SetBool(animAttackBool, true);
                if (AdmobAdsManager.Instance)
                {
                    if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                    {
                        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level " + newobjecttiveManager.CurrentLevel() + " Fail" );
                        Debug.Log("Level " + newobjecttiveManager   .CurrentLevel() + " Fail");
                    }
                }

            }
        }

        StartCoroutine(FailCoroutine());

    }

    IEnumerator FailCoroutine()
    {
        yield return new WaitForSeconds(4f);

        GameStateManager.Instance.MissionFailed();

        Debug.Log("Mission Failed is fired");
    }

    private bool IsCatInDetectionRadius()
    {
        return Vector3.Distance(transform.position, catTransform.position) <= detectionRadius;
    }



    public void ResetState()
    {
        Debug.Log("Reset state activated");
        dangerMusicWhenChasingCat.SetActive(false);
        mainMusicGameObject.SetActive(true);
        chaseTimer = maxTimeGrannyChaseCat;
        isChasingCat = false;
        canAttackCat = false;
        bodyRotationConstraint.constraintActive = false;
        bodyRotationConstraint.weight = 0;
        if (m_Agent.enabled && m_Agent.isOnNavMesh && m_Agent)
        {
            m_Agent.ResetPath();
            m_Agent.isStopped = false;
        }

        m_Animator.SetBool("Attack", false);
        m_Animator.SetBool("Wander", true);
    }


    #endregion

    #region ANIMATION EVENTS

    public void ReturnToWalkingAnimation()
    {
        m_Animator.SetTrigger(animReturnTrigger);
    }

    public void AngerAfterSpecialAttack()
    {
        m_Animator.SetTrigger(animAfterSpecial);
        StartChasingCat();
    }

    public void GrannyAttacked()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.catCrySound);
        SFX_Manager.PlaySound(SFX_Manager.Instance.catHitSound);
        SFX_Manager.PlayRandomSound(SFX_Manager.Instance.OnDieSounds, 0.5f);
        cameraAnimator.SetTrigger(cameraAnimShakeTrigger);
    }

    private bool IsCatSelected()
    {
        if (selectedIndexForCat == 0 || selectedIndexForCat == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsGrannySelected()
    {
            return true;
    }


    public void EnableCatDeath()
    {
       
            catDeathAnimGameobject.SetActive(true);
    }

    public void PunchSound()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.punchSound);
        SFX_Manager.PlaySound(SFX_Manager.Instance.grannyOhNo);

    }

    
    #endregion  

    public void SetRotationConstrainToZero()
    {
        bodyRotationConstraint.constraintActive = false;
        bodyRotationConstraint.weight = 0;
    }
}
