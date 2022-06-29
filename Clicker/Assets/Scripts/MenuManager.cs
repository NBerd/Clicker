using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _recordsMenu;

    public void EnableMenu(bool enable) 
    {
        _menu.SetActive(enable);
    }

    public void EnableRecords(bool enable) 
    {
        _recordsMenu.SetActive(enable);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}