using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CatHandAnimator : MonoBehaviour
{
    Animator m_Animator;
    public bool considerGrabAnimations;

    bool somethingInHand;

    [SerializeField] string walkingFloatString = "Speed";
    [SerializeField] string strafeFloatString = "Strafe";
    [SerializeField] string strafeBoolString = "IsStrafing";
    [SerializeField] string jumpingBoolString = "IsJumping";
    [SerializeField] string scratchString = "Scratch";
    [SerializeField] string grabString = "Grab";
    [SerializeField] string throwString = "Throw";
    [SerializeField] string resetString = "Reset";

    [SerializeField] FirstPersonController FirstPersonController;
    [SerializeField] GameObject scratchUiCat;
    [SerializeField] float timeToDisableScratchUi;
    public AudioClip footSound;
    public AudioClip hitSound;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();

        FirstPersonController.OnJump += OnJump;
        FirstPersonController.OnLandOnGround += OnLand;
        CatScratch.OnScratchButtonClickedEvent += CatScratch_OnScratchButtonClickedEvent;

        if (considerGrabAnimations)
            ForGrabAndThrow();
    }

    private void OnJump()
    {
        if (m_Animator)
            m_Animator.SetBool(jumpingBoolString, true);
    }

    private void OnLand()
    {
        if (m_Animator)
            m_Animator.SetBool(jumpingBoolString, false);
    }

    private void CatScratch_OnScratchButtonClickedEvent()
    {
        if (m_Animator && !somethingInHand)
            m_Animator.SetTrigger(scratchString);
    }

    private void OnDestroy()
    {
        FirstPersonController.OnJump -= OnJump;
        FirstPersonController.OnLandOnGround -= OnLand;
        CatScratch.OnScratchButtonClickedEvent -= CatScratch_OnScratchButtonClickedEvent;

        if (considerGrabAnimations)
        {
            PickableObject.CatGrabObject -= OnGrabObject;
            ObjectThrower.CatThrowObject -= OnThrowObject;
        }
    }

    void ForGrabAndThrow()
    {
        PickableObject.CatGrabObject += OnGrabObject;
        ObjectThrower.CatThrowObject += OnThrowObject;
    }

    private void OnGrabObject()
    {
        if (m_Animator)
        {
            m_Animator.SetBool(grabString, true);
            m_Animator.SetBool(throwString, false);
            somethingInHand = true;
        }
    }

    private void OnThrowObject()
    {
        if (m_Animator)
        {
            m_Animator.SetBool(grabString, false);
            m_Animator.SetBool(throwString, true);
            somethingInHand = false;
        }
    }

    private void Update()
    {
        if (m_Animator)
        {
            m_Animator.SetFloat(walkingFloatString, FirstPersonController.GetVerticalInput());
            m_Animator.SetFloat(strafeFloatString, FirstPersonController.GetHorizontalInput());
        }
    }

    public void EnableScratchUi()
    {
        scratchUiCat.SetActive(true);
        SFX_Manager.PlaySound(SFX_Manager.Instance.scratchSound, 1f);
        Invoke(nameof(DisableScratchUi), timeToDisableScratchUi);
    }

    void DisableScratchUi()
    {
        scratchUiCat.SetActive(false);
    }

    public void ResetAfterThrow()
    {
        if (m_Animator)
        {
            m_Animator.SetTrigger(resetString);
            m_Animator.SetBool(throwString, false);
        }

    }


    //Called through animation event
    public void PlayFootSound()
    {
        SFX_Manager.PlaySound(footSound, 0.25f);
    }
    public void PlayHitSound()
    {
        SFX_Manager.PlaySound(hitSound, 1f);
    }
}
