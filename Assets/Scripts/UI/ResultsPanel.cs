using Simulation;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultsPanel : MonoBehaviour {

    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Text _elapsedTime;
    [SerializeField] private Text _winColor;


    public void Setup(TimeSpan elapsedTime, int redCount, int blueCount, UnityAction playAgainHandler) {
        _elapsedTime.text = string.Format("{0}h {1}m {2}s", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
        if (redCount == blueCount) {
            _winColor.text = "None";
        }else {
            if (redCount > blueCount) {
                _winColor.text = "Red";
                _winColor.color = Color.red;
            }else {
                _winColor.text = "Blue";
                _winColor.color = Color.blue;
            }
        }
      
        _playAgainButton.onClick.AddListener(()=> {
            playAgainHandler();
            Destroy(gameObject);
        });
    }
}
