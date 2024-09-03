using UnityEngine;
using TMPro;
using System.Collections;

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public bool isReloading = false;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }

    public void Reload(float v)
    {
        if (currentAmmo == maxAmmo || isReloading)
        {
            return;
        }

        isReloading = true;
        StartCoroutine(ReloadCoroutine(v));
    }

    private IEnumerator ReloadCoroutine(float delay)
    {
        if (ammoText != null)
        {
            ammoText.text = "Reloading...";
        }

        AudioManager.instance.Play("Reload_Sound1");

        yield return new WaitForSeconds(delay);
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        isReloading = false;
    }
}
