using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    private const int NUM_QUESTIONS = 10;
    private const int NUM_ANSWERS = 5;

    private VideoPlayer videoPlayer;
    private List<List<string>> videoClips = new List<List<string>>();

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        List<string> clips1 = new List<string>();
        clips1.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "1_0.mov"));
        clips1.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "1_1.mov"));
        clips1.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "1_2.mov"));
        clips1.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "1_3.mov"));
        List<string> clips2 = new List<string>();
        clips2.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "2_0.mov"));
        clips2.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "2_1.mov"));
        clips2.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "2_2.mov"));
        clips2.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "2_3.mov"));
        List<string> clips3 = new List<string>();
        clips3.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "3_0.mov"));
        clips3.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "3_1.mov"));
        clips3.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "3_2.mov"));
        clips3.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "3_3.mov"));
        List<string> clips4 = new List<string>();
        clips4.Add("");
        clips4.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "4_1.mov"));
        clips4.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "4_2.mov"));
        clips4.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "4_3.mov"));
        clips4.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "4_4.mov"));
        List<string> clips5 = new List<string>();
        clips5.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "5_0.mov"));
        clips5.Add(System.IO.Path.Combine(""));
        clips5.Add(System.IO.Path.Combine(""));
        List<string> clips6 = new List<string>();
        clips6.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "6_0.mov"));
        clips6.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "6_1.mov"));
        clips6.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "6_2.mov"));
        List<string> clips7 = new List<string>();
        clips7.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "7_0.mov"));
        clips7.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "7_1.mov"));
        clips7.Add("");
        clips7.Add("");
        List<string> clips8 = new List<string>();
        List<string> clips9 = new List<string>();
        clips9.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "9_0.mov"));
        clips9.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "9_1.mov"));
        clips9.Add("");
        clips9.Add("");
        List<string> clips10 = new List<string>();
        clips10.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "10_0.mov"));
        clips10.Add("");
        clips10.Add(System.IO.Path.Combine(Application.streamingAssetsPath, "10_2.mov"));
        clips10.Add("");
        videoClips.Add(clips1);
        videoClips.Add(clips2);
        videoClips.Add(clips3);
        videoClips.Add(clips4);
        videoClips.Add(clips5);
        videoClips.Add(clips6);
        videoClips.Add(clips7);
        videoClips.Add(clips8);
        videoClips.Add(clips9);
        videoClips.Add(clips10);
    }

    // Start is called before the first frame update
    void Start()
    {
        string clipName = videoClips[GameManager.currentQuestion][GameManager.videoIndex];
        if (clipName.Equals(""))
        {
            StartQuiz();
        }
        else
        {
            videoPlayer.url = clipName;
            videoPlayer.Prepare();
            videoPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuiz()
    {
        videoPlayer.Stop();
        if (GameManager.videoIndex == 1)
        {
            GameManager.currentQuestion += 1;
            GameManager.videoIndex = 0;
            if (GameManager.currentQuestion == 7)
            {
                SceneManager.LoadScene("ShockwaveMinigame");
            }
            else if (GameManager.currentQuestion == 10)
            {
                SceneManager.LoadScene("Quiz");
            }
            else
            {
                SceneManager.LoadScene("Video");
            }
        }
        else
        {
            SceneManager.LoadScene("Quiz");
        }
    }

}
