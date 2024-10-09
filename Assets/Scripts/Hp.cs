using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private GameObject hpImage1, hpImage2;
    public Animator animator1, animator2;
    public static Hp instance;
    public GameObject GameOverBoard;
    public static int hp;
    public bool check1, check2;
    void Awake()
    {
        hp = 2;
        check1 = check2 = false;
        if(instance == null)
            instance = this;
    }
    public void Update()
    {
        if(hp == 1 && check1 == false)
        {
            check1 = true;
            animator2.Play("Heart(1)");
        }
        if(hp == 0 && check2 == false) 
        {
            if(check1 == false)
            {
                check1 = true;
                animator2.Play("Heart(1)");
            }
            check2 = true;
            animator1.Play("Heart");
            GameManager.banChoose = true;
            Invoke("GameOver", 1f);
        }
    }
    void GameOver()
    {
        GameOverBoard.SetActive(true);
    }
}
