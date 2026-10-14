using UnityEngine;
public class Objects : MonoBehaviour
{
    public Transform parent;
    public void Start()
    {
        // 子オブジェクトを格納する配列作成
        var children = new Transform[parent.childCount];

        // 0～個数-1までの子を順番に配列に格納
        for (var i = 0; i < children.Length; ++i)
        {
            children[i] = parent.GetChild(i);

            Vector3 localScale = children[i].transform.localScale; // ローカル変数に格納
            localScale.z += 0.2f; // ローカル変数に格納した値を上書き
            children[i].transform.localScale = localScale; // ローカル変数を代入
        }
    }
}