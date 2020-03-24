using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class GameUIWinner : MonoBehaviour
{
	private Text text;

    // Start is called before the first frame update
    void Start()
    {
		text = GetComponent<Text>();
		text.color = new Color(1, 1, 1, Mathf.Sin(Time.time * 5f));
	}

    // Update is called once per frame
    void Update()
    {
		text.color = new Color(1, 1, 1, Mathf.Sin(Time.time * 5f));
    }
}
