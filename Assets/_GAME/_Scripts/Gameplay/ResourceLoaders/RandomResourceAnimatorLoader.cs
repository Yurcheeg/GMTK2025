using UnityEngine;

public class RandomResourceAnimatorLoader : RandomResourceLoader<Animator, Animator>
{
    protected override void Set(Animator item, Animator subject) => subject.runtimeAnimatorController = item.runtimeAnimatorController;
}
