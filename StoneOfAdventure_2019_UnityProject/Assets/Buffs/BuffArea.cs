using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.VFX;

public class BuffArea : MonoBehaviour
{
    protected Dictionary<GameObject, List<BaseBuff>> buffedUnits = new Dictionary<GameObject, List<BaseBuff>>();
    [SerializeField] private GameObject visualEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            AddBuffs(collision.gameObject);
        }
    }

    protected virtual void AddBuffs(GameObject _target) { }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            RemoveBuffs(collision.gameObject);
        }
    }

    public virtual void RemoveBuffs(GameObject target)
    {
        if (!buffedUnits.ContainsKey(target)) return;

        foreach (var buff in buffedUnits[target])
        {
            buff.RemoveBuff();
        }
        buffedUnits.Remove(target);
        RemoveEffect(target);
    }
    public virtual void RemoveAllBuffs()
    {
        for (int i = 0; i < buffedUnits.Count; i++)
        {
            var target = buffedUnits.ElementAt(i).Key;
            RemoveBuffs(target);
        }
        buffedUnits.Clear();
    }

    protected virtual void AddEffect(GameObject target)
    {
        if (visualEffect == null) return;
        Instantiate(visualEffect, target.transform.position, Quaternion.identity, target.transform);
    }

    protected virtual void RemoveEffect(GameObject target)
    {
        foreach (var effect in target.GetComponentsInChildren<VisualEffect>())
        {
            if (effect.visualEffectAsset == visualEffect.GetComponent< VisualEffect>().visualEffectAsset)
            {
                Destroy(effect.gameObject);
            }
        }
    }
}
