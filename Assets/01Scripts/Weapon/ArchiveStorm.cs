using System.Collections;
using UnityEngine;

// 40  60  105  205  400 codec  x 2.5 = 1000
// 60  135  210  285  360 dot    x 2.5 = 450
// 20  40  60  80  100   basic  x 2.5 = 250

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
            pc.GetComponent<Status>().MoveSpeed = 6f * 1.5f;
            pc.GetComponent<Status>().AttackPower = 2.5f;
            pc.GetComponent<PlayerMove>().SetMoveSpeed(5f * 1.5f);

            var prevArchive = pc.archive;
            pc.archive = 0f;
            pc.OnChangeArchive.Invoke(prevArchive, pc.archive, 100);
            for (int i = 0; i < 20; i++)
            {
                wm.AllCoolTimeDecline(0.5f);
                yield return YieldInstructionCache.WaitForSeconds(0.25f);
            }
            pc.GetComponent<Status>().MoveSpeed = 6f;
            pc.GetComponent<Status>().AttackPower = 1f;
            pc.GetComponent<PlayerMove>().SetMoveSpeed(5f);
            UIManager.Instance.SetArchive(false);

            
        }
    }
}
