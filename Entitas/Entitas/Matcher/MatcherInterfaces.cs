namespace Entitas {

    public interface ICompoundMatcher<TEntity> : IMatcher<TEntity> where TEntity : class, IEntity, new() {
        int[] allOfIndices { get; }
        int[] anyOfIndices { get; }
        int[] noneOfIndices { get; }
    }

    public interface IAllOfMatcher<TEntity> : ICompoundMatcher<TEntity> where TEntity : class, IEntity, new() {
        IAnyOfMatcher<TEntity> AnyOf(params int[] indices);
        IAnyOfMatcher<TEntity> AnyOf(params IMatcher<TEntity>[] matchers);
        INoneOfMatcher<TEntity> NoneOf(params int[] indices);
        INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers);
    }

    public interface IAnyOfMatcher<TEntity> : ICompoundMatcher<TEntity> where TEntity : class, IEntity, new() {
        INoneOfMatcher<TEntity> NoneOf(params int[] indices);
        INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers);
    }

    public interface INoneOfMatcher<TEntity> : ICompoundMatcher<TEntity> where TEntity : class, IEntity, new() {
    }
}

