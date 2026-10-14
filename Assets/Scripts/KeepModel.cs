using UnityEngine;
public class KeepModel : MonoBehaviour
{
    private void Awake()
    {
        // instanceがすでにあったら自分を消去する。
        if (GameObject.FindGameObjectsWithTag("KeepModel").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Scene遷移で破棄されなようにする。      
            DontDestroyOnLoad(this);
        }
    }
}