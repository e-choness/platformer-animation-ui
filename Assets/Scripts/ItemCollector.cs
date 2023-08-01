using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _cherries;
    [SerializeField] private AudioSource collectingSoundEffect;
    [SerializeField] private TextMeshProUGUI text;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectingSoundEffect.Play();
            Destroy(other.gameObject);
            _cherries++;
            text.text = $"Cherries Count: {_cherries.ToString()}";
#if UNITY_EDITOR
            Debug.Log($"Cherries: {_cherries.ToString()}");
#endif
        }
    }
}
