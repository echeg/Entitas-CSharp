namespace Entitas {

    public partial class Matcher<TEntity> {

        public static IAllOfMatcher<TEntity> AllOf(params int[] indices) {
            var matcher = new Matcher<TEntity>();
            matcher._allOfIndices = distinctIndices(indices);
            return matcher;
        }

        public static IAllOfMatcher<TEntity> AllOf(params IMatcher<TEntity>[] matchers) {
            var allOfMatcher = (Matcher<TEntity>)AllOf(mergeIndices(matchers));
            setComponentNames(allOfMatcher, matchers);
            return allOfMatcher;
        }

        public static IAnyOfMatcher<TEntity> AnyOf(params int[] indices) {
            var matcher = new Matcher<TEntity>();
            matcher._anyOfIndices = distinctIndices(indices);
            return matcher;
        }

        public static IAnyOfMatcher<TEntity> AnyOf(params IMatcher<TEntity>[] matchers) {
            var anyOfMatcher = (Matcher<TEntity>)AnyOf(mergeIndices(matchers));
            setComponentNames(anyOfMatcher, matchers);
            return anyOfMatcher;
        }
    }
}

