using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VersionChecker : MonoBehaviour
{
    private string currentVersion;
    private string versionUrl = "https://raw.githubusercontent.com/arslanng/AppVersionControl/refs/heads/main/SmokerLung";
    public GameObject updatePanel;
    private VersionData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentVersion = Application.version;

        updatePanel.SetActive(false);
        Debug.Log("Current Unity Version: " + currentVersion);
        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        UnityWebRequest request = UnityWebRequest.Get(versionUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching version info: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            data = JsonUtility.FromJson<VersionData>(json);
            Debug.Log("Latest Version: " + data.latestVersion);
            if (currentVersion != data.latestVersion)
            {
                Debug.LogWarning("A new version is available! Please update to the latest version.");
                updatePanel.SetActive(true);
            }
            else
            {
                Debug.Log("You are using the latest version.");
            }
        }
    }
    public void UpdateBtn()
    {
        Application.OpenURL(data.updateUrl);
    }

}

[System.Serializable]
public class VersionData
{
    public string latestVersion;
    public string updateUrl;
}
