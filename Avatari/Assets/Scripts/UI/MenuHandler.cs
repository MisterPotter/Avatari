using UnityEngine;
using UnityEngine.SceneManagement;
/**
 *  @author: Tyler
 *
 *  A class to handle switching between different scenes through the bottom
 *  toolbar.
 */
public class MenuHandler : MonoBehaviour {

    public void HandleHomePressed() {
        LoadScene("home");
    }

    public void HandleInventoryPressed() {
        LoadScene("inventory");
    }

    public void HandleChallengesPressed() {
        LoadScene("challenges");
    }

    public void HandleBossesPressed() {
        LoadScene("bosses");
    }

    /**
     *  Loads a scene if the scene is not already loaded.
     *
     *  @param sceneName: The name of the scene to be loaded
     *      (specified in the build settings).
     */
    private void LoadScene(string sceneName) {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name != sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }

}
