#undef DO_NOT_INCLUDE

#if DO_NOT_INCLUDE
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.Statistics
{
    //TODO
    public abstract class Summary_Number_Animation : MonoBehaviour
    {
        protected int number;
        protected Text scoretxt = null;

        private void Awake()
        {
            Utility.Util.Log_NotImplemented();

            scoretxt = GetComponent<Text>();
        }

        private void OnEnable()
        {
            StartCoroutine(Score_Routine());
        }

        protected abstract IEnumerator Score_Routine();
    }


    ////TODO
    //public class Summary_Minion_Killed_Animation : Summary_Number_Animation
    //{
    //    protected override IEnumerator Score_Routine()
    //    {
    //        PlayerData inst = LevelManager.Instance.PlayerLevelData;

    //        while (number < inst.GetData().BossesDestroyed)
    //        {
    //            yield return null;
    //        }
    //    }
    //}
}
#endif