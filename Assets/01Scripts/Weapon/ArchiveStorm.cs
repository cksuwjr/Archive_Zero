using System.Collections;
using UnityEngine;

public class ArchiveStorm : Skill
{
    [SerializeField] private AudioClip archieveSound;
    protected override IEnumerator Cast_()
    {
        var pc = GameManager.Instance.Player.GetComponent<PlayerController>();
        var wm = pc.GetComponent<WeaponManager>();
        if (pc.archive > 99)
        {
            if (archieveSound)
                SoundManager.Instance.PlaySound(archieveSound);

            UIManager.Instance.SetArchive(true);
            pc.GetComponent<Status>().MoveSpeed = 5f * 1.3f;
            pc.GetComponent<PlayerMove>().SetMoveSpeed(5f * 1.3f);
            for (int i = 0; i < 10; i++)
            {
                wm.AllCoolTimeDecline(1f);
                yield return YieldInstructionCache.WaitForSeconds(0.5f);
            }
            pc.GetComponent<Status>().MoveSpeed = 5f;
            pc.GetComponent<PlayerMove>().SetMoveSpeed(5f);
            UIManager.Instance.SetArchive(false);

            pc.archive = 0f;
        }
    }
}
