using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using enemy;
using boss;

public class Game : MonoBehaviour
{
    public string[] stagenames;
    private Rigidbody gameavatarpos;
    private Transform avatarpos;
    private Animator ModelAnimator;
    private bool IsEnd = true;

    public AudioClip DefenseSE;
    public AudioClip PunchHitSE;
    public AudioClip PunchMissSE;
    public AudioClip WinSE;
    public AudioClip LoseSE;
    AudioSource audioSource;
    private void Start()
    {
        gameavatarpos = GameObject.Find("GameAvatarPos").gameObject.GetComponent<Rigidbody>();
        avatarpos = this.transform;

        GameObject AvatarposObj = GameObject.FindWithTag("KeepModel");
        DontDestroyOnLoad(gameavatarpos);
        AvatarposObj.transform.SetParent(this.transform);
        ModelAnimator = AvatarposObj.transform.GetChild(0).GetComponent<Animator>();
        StageStart();

        audioSource = AvatarposObj.GetComponent<AudioSource>();
    }
    private void Update()
    {
        //move
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (avatarpos.position.x <= 0)
            {
                avatarpos.position += new Vector3(1.5f, 0, 0);
                audioSource.PlayOneShot(DefenseSE);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (avatarpos.position.x >= 0)
            {
                avatarpos.position += new Vector3(-1.5f, 0, 0);
                audioSource.PlayOneShot(DefenseSE);
            }
        }
        //atack
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            Invoke(nameof(attackHantei), 0.2f);
            ModelAnimator.SetTrigger("Punch");
        }
        //end
        if (!IsEnd && gameavatarpos.position.z >= 27.5f)
        {
            gameavatarpos.velocity = new Vector3(0, 0, 0);
            ModelAnimator.SetTrigger("Win");
            audioSource.PlayOneShot(WinSE);
            IsEnd = true;

            // データ保存
            int StageNumData = PlayerPrefs.GetInt("SaveStageNum");
            if (StageNumData < StageManage.stagenum)
            {
                PlayerPrefs.SetInt("SaveStageNum", StageManage.stagenum);
                PlayerPrefs.Save();
            }

            GameEnd();
            //Invoke(nameof(StageStart), 5f);
        }
    }
    private void FixedUpdate()
    {
        if (!IsEnd)
        {
            RaycastHit hit;
            LayerMask layerMask = 1 << 19;
            if (!Physics.Raycast(this.transform.position, new Vector3(0, -1, 0), out hit, 1.5f, layerMask))
            {
                Debug.Log("Lose");
                gameavatarpos.velocity = new Vector3(0, 0, 0);
                ModelAnimator.SetTrigger("Lose");
                audioSource.PlayOneShot(LoseSE);
                IsEnd = true;
                Invoke(nameof(GameEnd), 4.2f);
            }
        }
    }
    private void attackHantei()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + new Vector3(0, 0.2f, 0), new Vector3(0, 0, 1), out hit, 2.5f))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>() != null) hit.collider.gameObject.GetComponent<Enemy>().hitted();
            else hit.collider.gameObject.GetComponent<Boss>().hitted();
            audioSource.PlayOneShot(PunchHitSE);
        }
        else
        {
            audioSource.PlayOneShot(PunchMissSE);
        }
    }
    void StageStart()
    {
        SceneManager.sceneLoaded += StageSceneLoaded;
        SceneManager.LoadScene(stagenames[StageManage.stagenum], LoadSceneMode.Additive);
    }
    private void StageSceneLoaded(Scene stagescene, LoadSceneMode mode)
    {
        GameObject.Find("GameCamera").transform.SetParent(gameavatarpos.transform);
        Destroy(gameavatarpos.transform.Find("MainCamera").gameObject);
        gameavatarpos.AddForce(new Vector3(0, 0, 1.5f), ForceMode.Impulse);
        gameavatarpos.transform.position = new Vector3(0, 0, -27.5f);

        SceneManager.sceneLoaded -= StageSceneLoaded;
        IsEnd = false;
    }
    void GameEnd()
    {
        GameObject avatarPosObject = this.transform.Find("avatarpos").gameObject;
        avatarPosObject.transform.SetParent(null);

        Scene GameScene = SceneManager.GetSceneByName("MainScene");
        SceneManager.MoveGameObjectToScene(gameavatarpos.gameObject, GameScene);

        avatarPosObject.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene("VRMSelectScene");
    }
}
