public abstract class ProjectileDecorator : IProjectileBehaviour
{
    protected IProjectileBehaviour decoratedBehaviour;

    public ProjectileDecorator(IProjectileBehaviour decoratedBehaviour)
    {
        this.decoratedBehaviour = decoratedBehaviour;
    }

    public virtual void OnHit()
    {
        // Par défaut, délégation vers le comportement décoré.
        decoratedBehaviour.OnHit();
    }
}