using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _shootEffect;
    [SerializeField] GameObject _firePoint;
    [SerializeField] GameObject _vfx;

    public void MakeShootEffect()
    {
        ShowMuzzle();
    }

    private void ShowMuzzle()
    {
        if(_shootEffect.isPlaying)
        {
            _shootEffect.Stop();
        }
        _shootEffect.Play();
    }
}
