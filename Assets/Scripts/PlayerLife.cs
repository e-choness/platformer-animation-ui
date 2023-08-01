using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private static readonly int Death = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        deathSoundEffect.Play();
        _animator.SetTrigger(Death);
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
