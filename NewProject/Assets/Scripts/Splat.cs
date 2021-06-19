using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    public enum SplatLocation { Foreground, Background};

    public Color backgroundTint;
    public float minSizeMod = 0.8f;
    public float maxSizeMod = 1.5f;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private SplatLocation splatLocation;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(SplatLocation splatLocation)
    {
        this.splatLocation = splatLocation;
        SetSprite();
        SetSize();
        SetRotation();

        SetLocationProperties();
    }

    private void SetSprite()
    {
        //int randomIndex = Random.Range(0, sprites.Length);
        //spriteRenderer.sprite = sprites[randomIndex];

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void SetSize()
    {
        float sizeMod = Random.Range(minSizeMod, maxSizeMod);
        transform.localScale *= sizeMod;
    }

    private void SetRotation()
    {
        float randomRoatation = Random.Range(-360f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRoatation);
    }

    private void SetLocationProperties()
    {
        switch(splatLocation)
        {
            case SplatLocation.Background:
                spriteRenderer.color = backgroundTint;
                spriteRenderer.sortingOrder = 0;
                break;

            case SplatLocation.Foreground:
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                spriteRenderer.sortingOrder = 3;
                break;
        }
    }
}
