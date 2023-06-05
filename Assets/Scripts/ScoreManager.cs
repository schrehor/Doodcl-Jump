using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    private float _topScore = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = Mathf.Round(_topScore).ToString(CultureInfo.InvariantCulture);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > _topScore)
        {
            _topScore = transform.position.y;
        }
        
        scoreText.text = Mathf.Round(_topScore).ToString(CultureInfo.InvariantCulture);;
    }
}
