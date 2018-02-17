using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Transform _player;
	private Text _text;
	public static int _score = 0;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		_text.text = _score.ToString();
	}
}
