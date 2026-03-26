using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public PlayerController stopPlayer;
    public Health health;
    

    public TextMeshProUGUI timer;
    public TextMeshProUGUI bustFaster;
    public GameObject TimesUpScreen;
    public GameObject player;
    

    public float time = 90f;

    
    
    

    void Start()
    {
        bustFaster.gameObject.SetActive(false);

    }


    void Update()
    {
        timer.text = time.ToString("0");

        if (time > 0)
        {
            time -= Time.deltaTime;

        }

        else
        {
            time = 0;
            timer.text = "0";
        }

        if (time <= 45 && time > 0)
        {
            bustFaster.gameObject.SetActive(true);

        }

        else
        {

            bustFaster.gameObject.SetActive(false);

        }

        if (time <= 0)
        {
            bustFaster.gameObject.SetActive(false);
            timer.gameObject.SetActive(false);
            Invoke(nameof(TimesUp), 0.2f);
            

        }
    }

    private void TimesUp()
    {
        TimesUpScreen.SetActive(true);
        
        if (player != null)
        {
            stopPlayer.enabled = false;
           health.enabled = false;
           

        }
    }


}
