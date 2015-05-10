using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    //得分
    public int m_score = 0;

    //主角
    private GameObject m_player;

    // 背景音乐
    public AudioClip m_musicClip;

    // 声音源
    protected AudioSource m_Audio;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        m_Audio = this.GetComponent<AudioSource>();

        // 获取主角
		m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // 循环播放背景音乐
        if (!m_Audio.isPlaying)
        {
            m_Audio.clip = m_musicClip;
            m_Audio.Play();
        }
	}

    void OnGUI()
    {
        if (m_player == null)
        {
            // 放大字体
            GUI.skin.label.fontSize = 70;

            // 显示游戏失败
            GUI.skin.label.alignment = TextAnchor.LowerCenter;
            GUI.Label(new Rect(0, Screen.height * 0.4f, Screen.width, 120), "GAME OVER");

            GUI.skin.label.fontSize = 20;

            // 显示按钮
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.7f, 100, 30), "Try Again"))
            {
                // 读取当前关卡
                Application.LoadLevel(Application.loadedLevelName);
            }
        }

        GUI.skin.label.fontSize = 40;

        // 显示最高分
        GUI.skin.label.alignment = TextAnchor.LowerCenter;

        // 显示当前得分
        GUI.Label(new Rect(0, 25, Screen.width, 60), "KILL: " + m_score);
      
    }

    // 增加分数
    public void AddScore( int point )
    {
        m_score += point;
    }
}
