using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Animator animator;

    public GameObject star;
    public float waitToShowExit;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    public enum BossPhase
    {
        INTRO, PHASE1, PHASE2, PHASE3, PHASE4, END
    }

    public BossPhase currentPhase = BossPhase.INTRO;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.isRespawning)
        {
            currentPhase = BossPhase.INTRO;

            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Phase3", false);
            animator.SetBool("Phase4", false);

            BossActivator.Instance.gameObject.SetActive(true);
            BossActivator.Instance.theBoss.SetActive(false);
            BossActivator.Instance.entrance.SetActive(true);

            AudioManager.instance.StopMusic(bossMusic);
            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        }
    }

    void OnEnable()
    {
        AudioManager.instance.StopMusic(AudioManager.instance.levelMusicToPlay);
        AudioManager.instance.PlayMusic(bossMusic);
    }

    public void DamageBoss()
    {
        AudioManager.instance.PlaySFX(bossHit);

        currentPhase++;

        if (currentPhase != BossPhase.END)
        {
            animator.SetTrigger("Hurt");
        }

        switch (currentPhase)
        {
            case BossPhase.PHASE1:
                animator.SetBool("Phase1", true);
                break;
            case BossPhase.PHASE2:
                animator.SetBool("Phase2", true);
                animator.SetBool("Phase1", false);
                break;
            case BossPhase.PHASE3:
                animator.SetBool("Phase3", true);
                animator.SetBool("Phase2", false);
                break;
            case BossPhase.PHASE4:
                animator.SetBool("Phase4", true);
                animator.SetBool("Phase3", false);
                break;
            case BossPhase.END:
                animator.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;
        }
    }

    private IEnumerator EndBoss()
    {
        AudioManager.instance.StopMusic(bossMusic);
        AudioManager.instance.PlaySFX(bossDeath);
        AudioManager.instance.PlaySFX(bossDeathShout);

        yield return new WaitForSeconds(waitToShowExit);
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        star.SetActive(true);

    }
}
