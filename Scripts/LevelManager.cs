using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel(string nameLevel)
    {
        DoubleLaser.doubleFire = false;
        FormationController.numberOfRounds = 0;
        SceneManager.LoadScene(nameLevel);
    }

	public void QuitRequest()
    {
		//Debug.Log ("Quit requested");
		Application.Quit ();
	}
}