public class TvObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Remote.OnInteract += Remote_OnInteract;
        NewObjectiveManager.Instance.ChangeAnimatorToSit();
        NewObjectiveManager.Instance.GrannyWanderingState(false);
    }

    private void Remote_OnInteract()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.ChangeAnimatorToWandering();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
    }

    private void OnDestroy()
    {
        Remote.OnInteract += Remote_OnInteract;
    }
    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToSit();
        NewObjectiveManager.Instance.GrannyWanderingState(false);
    }
}
