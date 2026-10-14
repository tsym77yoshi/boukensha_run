using UnityEngine;
using UnityEngine.UI;
public class StageManage : MonoBehaviour
{
    public static int stagenum;
    static StageManage()
    {
        stagenum = 0;
    }
    public Image[] stageselectbuttons;
    public AudioClip SE;
    public AudioSource audioSource;
    private void Start()
    {
        for (ushort i = 0; i < stageselectbuttons.Length; i++)
        {
            if (i == stagenum) stageselectbuttons[i].color = Color.yellow;
            else stageselectbuttons[i].color = Color.white;
        }

        if (PlayerPrefs.HasKey("SaveStageNum"))
        {
            int StageNumData = PlayerPrefs.GetInt("SaveStageNum");
            Debug.Log("savedata:"+StageNumData);
            for (int i = 0; i < StageNumData + 2; i++)
            {
                if (stageselectbuttons.Length > i)
                {
                    stageselectbuttons[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("SaveStageNum", 0);
            PlayerPrefs.Save();
        }
    }
    public void OnClickStageButton(int num)
    {
        for (ushort i = 0; i < stageselectbuttons.Length; i++)
        {
            if (i == num) stageselectbuttons[i].color = Color.yellow;
            else stageselectbuttons[i].color = Color.white;
        }
        stagenum = num;
        audioSource.PlayOneShot(SE);
    }
}