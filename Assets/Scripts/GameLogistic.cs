using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogistic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI courseText;
    private Dictionary<string, int> courseScores = new Dictionary<string, int>();
    private Dictionary<string, bool> courseStatuses = new Dictionary<string, bool>();
    [SerializeField] public GameObject iWinText;
    [SerializeField] public GameObject otherWinText;

    private string _currentCourse = "Earth";

    public string currentCourse
    {
        get { return _currentCourse; }
        set
        {
            _currentCourse = value;
        }
    }

    public bool collisionfound = false;

    public bool iLost = false;

    void Start()
    {
        courseScores["Earth"] = 0;
        courseScores["Mars"] = 0;
        courseScores["Neptune"] = 0;
        courseStatuses["Earth"] = false;
        courseStatuses["Mars"] = false;
        courseStatuses["Neptune"] = false;
    }

    public void addScore()
    {
        courseScores[currentCourse] += 1;
        UpdateScoreText();
    }

    public void resetScore()
    {
        courseScores[currentCourse] = 0;
        UpdateScoreText();
    }

    public void otherLost()
    {
        otherWinText.SetActive(true);
    }

    public void updateCourse(string course)
    {
        currentCourse = course;
        courseScores[course] = 0;
        courseText.text = currentCourse.ToString();
        UpdateScoreText();
    }

    public void moveToNextCourse()
    {
        courseText.text = "Teleport to next course";
    }

    public void updateCourseStatus(string course)
    {
        courseStatuses[course] = true;
        bool allTrue = true;
        foreach (var status in courseStatuses.Values)
        {
            if (!status)
            {
                allTrue = false;
                break;
            }
        }
        if (allTrue)
        {
            int totalScore = GetTotalScore();
            iWinText.SetActive(true);
            iWinText.GetComponent<TextMeshProUGUI>().text = "Game Over\nTotal Score: " + totalScore;
        }
    }

    private int GetTotalScore()
{
    int totalScore = 0;
    foreach (var kvp in courseScores)
    {
        totalScore += kvp.Value;
    }
    return totalScore;
}

    void UpdateScoreText()
    {
        string scoreString = "";
        foreach (var kvp in courseScores)
        {
            string line = kvp.Key + ": " + kvp.Value;
            if (kvp.Key == currentCourse)
            {
                line += " (current)";
            }
            scoreString += line + "\n";
        }
        scoreText.text = scoreString;
    }
}
