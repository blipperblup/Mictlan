using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject Menu3;

    public Animator transition;

    public void StartMenu()
    {
        SceneManager.LoadScene("Lvl1");
        transition.SetTrigger("Start");
    }

    public void LevelMenu()
    {
        Menu1.SetActive(false);
        Menu2.SetActive(true);
        Menu3.SetActive(false);
    }
    public void ControlMenu()
    {
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(true);
    }
    public void Return()
    {
        Menu1.SetActive(true);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
    }
    public void lvl1()
    {
        SceneManager.LoadScene("Lvl1");
        transition.SetTrigger("Start");
    }
    public void lvl2()
    {
        SceneManager.LoadScene("Lvl2");
        transition.SetTrigger("Start");
    }
    public void lvl3()
    {
        SceneManager.LoadScene("Lvl3");
        transition.SetTrigger("Start");
    }
    public void lvl4()
    {
        SceneManager.LoadScene("Lvl4");
        transition.SetTrigger("Start");
    }
    public void lvl5()
    {
        SceneManager.LoadScene("Lvl5");
        transition.SetTrigger("Start");
    }
    public void lvl6()
    {
        SceneManager.LoadScene("Lvl6");
        transition.SetTrigger("Start");
    }
    public void lvl7()
    {
        SceneManager.LoadScene("Lvl7");
        transition.SetTrigger("Start");
    }
    public void lvl8()
    {
        SceneManager.LoadScene("Lvl8");
        transition.SetTrigger("Start");
    }
    public void lvl9()
    {
        SceneManager.LoadScene("Lvl9");
        transition.SetTrigger("Start");
    }
    public void lvl10()
    {
        SceneManager.LoadScene("Lvl10");
        transition.SetTrigger("Start");
    }
    public void lvl11()
    {
        SceneManager.LoadScene("Lvl11");
        transition.SetTrigger("Start");
    }
    public void lvl12()
    {
        SceneManager.LoadScene("Lvl12");
        transition.SetTrigger("Start");
    }
    public void lvl13()
    {
        SceneManager.LoadScene("Lvl13");
        transition.SetTrigger("Start");
    }
    public void lvl14()
    {
        SceneManager.LoadScene("Lvl14");
        transition.SetTrigger("Start");
    }
    public void lvl15()
    {
        SceneManager.LoadScene("Lvl15");
        transition.SetTrigger("Start");
    }
    public void demolvl()
    {
        SceneManager.LoadScene("Lvl11");
        transition.SetTrigger("Start");
    }

}
