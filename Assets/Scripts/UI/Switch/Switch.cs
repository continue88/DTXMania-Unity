using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Activity
{
    protected bool mCloseExecuted = false;
    protected Animation mStageAnim;
    protected AnimationState mAnimationState;

    public System.Action OnSwitchMiddleClosed;

    public Switch()
        : base(null)
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();

        mStageAnim = GetComponent<Animation>("Anim");
        mAnimationState = mStageAnim[mStageAnim.clip.name];
        PlayCloseAnim();
    }

    public override void Update()
    {
        base.Update();

        if (!mStageAnim.isPlaying)
        {
            if (mCloseExecuted)
                Close();
            else
                ExecuteSwitch();
        }
    }

    protected virtual void PlayCloseAnim()
    {
        mAnimationState.normalizedTime = 1;
        mAnimationState.speed = -1.0f;
        mStageAnim.Play(mAnimationState.name);
    }

    protected virtual void PlayOpenAnim()
    {
        mAnimationState.speed = 1;
        mStageAnim.Play(mAnimationState.name);
    }

    protected virtual void ExecuteSwitch()
    {
        mCloseExecuted = true;

        OnSwitchMiddleClosed?.Invoke();
        // move to the topmost.
        Transform.SetAsLastSibling();

        PlayOpenAnim();
    }

    public void Close()
    {
        SwitchManager.Instance.Close(this);
    }
}
