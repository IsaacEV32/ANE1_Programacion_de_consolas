using System.Collections;
using TMPro;
using UnityEngine;

public class DisconectionOfTheDevice : MonoBehaviour
{
    [SerializeField] GameObject notificationPanel;
    [SerializeField] TMP_Text notificationText;

    Coroutine notificationRoutine = null;

    public static DisconectionOfTheDevice instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void DisconnectDevice()
    {
        if (notificationRoutine != null)
        {
            StopCoroutine(notificationRoutine);
            notificationRoutine = null;
        }
        // Implement the logic to disconnect the device here
        notificationPanel.SetActive(true);
        notificationRoutine = StartCoroutine(DeviceHandlerNotification("Device disconnected."));
    }
    public void ConnectDevice()
    {
        if (notificationRoutine != null)
        {
            StopCoroutine(notificationRoutine);
            notificationRoutine = null;
        }
        notificationPanel.SetActive(true);
        notificationRoutine = StartCoroutine(DeviceHandlerNotification("Device connected."));
    }
    IEnumerator DeviceHandlerNotification(string _notificationText)
    {
        notificationText.text = _notificationText;
        float timer = 0f;
        while (timer < 3f)
        {
            timer += 0.5f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        notificationPanel.SetActive(false);
        notificationRoutine = null;
    }
}
