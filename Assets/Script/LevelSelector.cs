using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	public SceneFader fader;
	public Transform buttonContent;
	public Button[] levelButtons;
	public Sprite locked;
	public static LevelSelector instance;
    private void Awake()
    {
		instance = this;
    }
    void Start()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);
		levelButtons = new Button[buttonContent.childCount];
		for (int i = 0; i < buttonContent.childCount; i++)
		{
			levelButtons[i] = buttonContent.GetChild(i).GetComponent<Button>();
			levelButtons[i].name = "Level0" + (i + 1).ToString();
			levelButtons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
		}
		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
			{
				levelButtons[i].interactable = false;
				levelButtons[i].GetComponentInChildren<Image>().sprite = locked;
			}
		}
	}
	public void Select(string levelName)
	{
		fader.FadeTo(levelName);
	}
}
