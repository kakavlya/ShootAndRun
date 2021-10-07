using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _shootEffect;
    [SerializeField] GameObject _firePoint;
    [SerializeField] GameObject _vfx;

    public void MakeShootEffect()
    {
        //SpawnVfx();
        ShowMuzzle();
    }

    private void SpawnVfx()
    {
        GameObject effect;
        effect = Instantiate(_vfx, _firePoint.transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
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
