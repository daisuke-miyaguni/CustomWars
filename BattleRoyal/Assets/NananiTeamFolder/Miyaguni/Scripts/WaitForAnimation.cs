﻿using UnityEngine;

public class WaitForAnimation : CustomYieldInstruction
{
    Animator m_animator;
    int m_lastStateHash = 0;
    int m_layerNo = 0;

    public WaitForAnimation(Animator animator, int layerNo)
    {
        Init(animator, layerNo, animator.GetCurrentAnimatorStateInfo(layerNo).fullPathHash);
    }

    void Init(Animator animator, int layerNo, int hash)
    {
        m_layerNo = layerNo;
        m_animator = animator;
        m_lastStateHash = hash;
    }

    public override bool keepWaiting
    {
        get
        {
            AnimatorStateInfo currentAnimatorState = m_animator.GetCurrentAnimatorStateInfo(m_layerNo);
            return currentAnimatorState.fullPathHash == m_lastStateHash && (currentAnimatorState.normalizedTime < 1);
        }
    }
}