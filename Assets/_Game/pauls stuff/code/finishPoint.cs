using UnityEngine;

public class finishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (goNextLevel)
            {
                SceneController.instance.NextLevel();
            }
           /* else
            {
                SceneController.instance.LoadScene(levelName);
            }*/
        }
    }
    
}
