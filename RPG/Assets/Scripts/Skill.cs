using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public float animationDuration;
    public bool selfInflicted;
    public GameObject effectPrfb;
    protected Fighter emisor;
    protected Fighter receptor;
    protected Queue<string> messages;
    void Awake()
    {
         this.messages=new Queue<string>();
    }
    private void Animate()
    {
        var go = Instantiate(this.effectPrfb, this.receptor.transform.position, Quaternion.identity);
        Destroy(go, this.animationDuration);
    }

    public void Run()
    {
        if (this.selfInflicted)
        {
            this.receptor = this.emisor;
        }
        this.Animate();
        this.OnRun();
    }

    public void TomarEmisorAndReceptor(Fighter _emisor,Fighter _receptor)
    {
        this.emisor = _emisor;
        this.receptor = _receptor;
    }
    public string TomarSiguienteMensaje()
    {
        if (this.messages.Count != 0)
        {
            return this.messages.Dequeue();
        }
        else
        {
            return null;
        }
    }
    protected abstract void OnRun();
}
