using UnityEngine;
using GameSystem.GameSceneManagement;


namespace GameSystem
{
    public class PlayerUltimate : MonoBehaviour, ISceneObject
    {
        private CircleCollider2D coll;
        private Rigidbody2D rigid;
        private Animation anim;


        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            coll = GetComponentInChildren<CircleCollider2D>(true);
            anim = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            if (!LevelManager.Instance || anim.isPlaying) return;

            anim.Play();
            GameSceneLoader.OnStartLoadingNextScene += DisableObj;
        }

        private void Start()
        {
            gameObject.layer= LayerMask.NameToLayer("PlayerUltimate"); //PlayerUltimate Layer
            coll.gameObject.tag = "PlayerUltimate";

            rigid.bodyType = RigidbodyType2D.Static;

            coll.isTrigger = true;
        }

        private void OnDisable()
        {
            GameSceneLoader.OnStartLoadingNextScene -= DisableObj;
        }

        public void DisableObj()
        {
            gameObject.SetActive(false);
        }
    }
}