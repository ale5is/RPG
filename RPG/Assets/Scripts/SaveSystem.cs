using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private Controller_Player playerScr;
    public static SaveSystem Save;
    private void Awake()
    {
        playerScr=FindObjectOfType<Controller_Player>();
        if (Save == null)
        {
            Save = this;
            DontDestroyOnLoad(Save);
        }
        else
        {
            Destroy(Save);
        }
    }
    public void SavePosition()
    {
        Vector3 playerPos = playerScr.Position();
        PlayerPrefs.SetFloat("posX", playerPos.x);
        PlayerPrefs.SetFloat("posZ", playerPos.z);
        Debug.Log("a");
    }
    public void LoadPosition()
    {
        Vector3 playerPos = new Vector3(PlayerPrefs.GetFloat("posX"), 0, PlayerPrefs.GetFloat("posZ"));
        playerScr.SetPosition(playerPos);
;    }
}
