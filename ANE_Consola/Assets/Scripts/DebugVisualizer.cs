using System.Collections;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class DebugVisualizer : MonoBehaviour
{
    ProfilerRecorder gpuTimeRecorder;
    ProfilerRecorder cpuTimeRecorder;
    ProfilerRecorder frameTimeRecorder;

    float deltaTime;

    public static DebugVisualizer instance;

    [SerializeField] TMP_Text gpuUsageText;
    [SerializeField] TMP_Text cpuUsageText;
    [SerializeField] TMP_Text fpsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
#if DEVELOPMENT_BUILD
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
#elif UNITY_EDITOR

        Destroy(this.gameObject);

#endif
    }

#if DEVELOPMENT_BUILD
    void Start()
    {
        gpuTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "GPU Frame Time");
        cpuTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "CPU Main Thread Frame Time");
        frameTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "CPU Total Frame Time");
        StartCoroutine(DebugLoop());

    }

    private void OnDisable()
    {
        gpuTimeRecorder.Dispose();
        cpuTimeRecorder.Dispose();
        frameTimeRecorder.Dispose();
    }
    IEnumerator DebugLoop()
    {
        while (true)
        {
            long gpuTimeNanoseconds = gpuTimeRecorder.LastValue;
            float gpuTimeSeconds = gpuTimeNanoseconds * 1e-9f;
            long cpuTimeNanoseconds = cpuTimeRecorder.LastValue;
            float cpuTimeSeconds = cpuTimeNanoseconds * 1e-9f;
            long totalFrameNanoseconds = frameTimeRecorder.LastValue;
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
            if (totalFrameNanoseconds > 0)
            {
                float gpuPercentage = (gpuTimeNanoseconds / (float)totalFrameNanoseconds) * 100f;
                float cpuPercentage = (cpuTimeNanoseconds / (float)totalFrameNanoseconds) * 100f;

                gpuUsageText.text = $"GPU Usage: {gpuPercentage:F2}%";
                cpuUsageText.text = $"CPU Usage: {cpuPercentage:F2}%";
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }

    }
#endif
}
