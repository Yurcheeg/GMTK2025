using UnityEngine;

public class RandomResourceSpriteLoader : RandomResourceLoader<Sprite, SpriteRenderer>
{
    protected override void Set(Sprite item, SpriteRenderer subject) => subject.sprite = item;
}
