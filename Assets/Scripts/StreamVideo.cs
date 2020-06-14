using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    void Update()
    {
        bool stop = Input.GetKeyDown("enter") || Input.GetKeyDown("return") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        if (stop)
        {
            StopVideo(videoPlayer);
        }
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
        videoPlayer.loopPointReached += StopVideo;
    }

    private void StopVideo(VideoPlayer vp)
    {
        vp.Stop();
        Destroy(rawImage.gameObject);
    }
}
