using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public PlayerController stopPlayer;
    public Health health;

    public AudioSource panic;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI bustFaster;
    public GameObject TimesUpScreen;
    public GameObject player;
    public GameObject music;
    

    public float time = 90f;
    bool hasEnded = false;
    
    
    
    // Text unactive at start
    void Start()
    {
        bustFaster.gameObject.SetActive(false);

    }

    
    void Update()
    {

        timer.text = time.ToString("0");

        // Time decreasing overtime
        if (time > 0)
        {
            time -= Time.deltaTime;

        }

        
        // Game Over
        else if (!hasEnded)
        {
            time = 0;
            timer.text = "0";

            hasEnded = true;

            bustFaster.gameObject.SetActive(false);
            timer.gameObject.SetActive(false);

            Invoke(nameof(TimesUp), 0.2f);
            

        }

        // Text coming when 50 seconds is remaining
        if (time <= 50 && time > 0)
        {
            bustFaster.gameObject.SetActive(true);
            music.SetActive(false);
        }

        // Over 50 seconds remaining
        else
        {
            bustFaster.gameObject.SetActive(false);

        }

    }



    private void TimesUp()
    {
        TimesUpScreen.SetActive(true);

        if (player != null)
        {
            health.Die();
        }
        
    }


}
