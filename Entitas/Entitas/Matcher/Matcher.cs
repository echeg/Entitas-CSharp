using System;
using System.Collections.Generic;

namespace Entitas {

    public partial class Matcher<TEntity> : IAllOfMatcher<TEntity>, IAnyOfMatcher<TEntity>, INoneOfMatcher<TEntity> where TEntity : class, IEntity, new() {

        public int[] indices {
            get {
                if (_indices == null) {
                    _indices = mergeIndices();
                }
                return _indices;
            }
        }

        public int[] allOfIndices { get { return _allOfIndices; } }
        public int[] anyOfIndices { get { return _anyOfIndices; } }
        public int[] noneOfIndices { get { return _noneOfIndices; } }

        int[] _indices;
        int[] _allOfIndices;
        int[] _anyOfIndices;
        int[] _noneOfIndices;

        Matcher() {
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params int[] indices) {
            _anyOfIndices = distinctIndices(indices);
            _indices = null;
            return this;
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params IMatcher<TEntity>[] matchers) {
            return ((IAllOfMatcher<TEntity>)this).AnyOf(mergeIndices(matchers));
        }

        public INoneOfMatcher<TEntity> NoneOf(params int[] indices) {
            _noneOfIndices = distinctIndices(indices);
            _indices = null;
            return this;
        }

        public INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers) {
            return NoneOf(mergeIndices(matchers));
        }

        public bool Matches(TEntity entity) {
            var matchesAllOf = _allOfIndices == null || entity.HasComponents(_allOfIndices);
            var matchesAnyOf = _anyOfIndices == null || entity.HasAnyComponent(_anyOfIndices);
            var matchesNoneOf = _noneOfIndices == null || !entity.HasAnyComponent(_noneOfIndices);
            return matchesAllOf && matchesAnyOf && matchesNoneOf;
        }

        int[] mergeIndices() {
            var indicesList = EntitasCache.reusableIntList;
            if (_allOfIndices != null) {
                indicesList.AddRange(_allOfIndices);
            }
            if (_anyOfIndices != null) {
                indicesList.AddRange(_anyOfIndices);
            }
            if (_noneOfIndices != null) {
                indicesList.AddRange(_noneOfIndices);
            }

            return distinctIndices(indicesList);
        }

        static int[] mergeIndices(IMatcher<TEntity>[] matchers) {
            var indices = new int[matchers.Length];
            for (int i = 0; i < matchers.Length; i++) {
                var matcher = matchers[i];
                if (matcher.indices.Length != 1) {
                    throw new MatcherException<TEntity>(matcher);
                }
                indices[i] = matcher.indices[0];
            }

            return indices;
        }

        static string[] getComponentNames(IMatcher<TEntity>[] matchers) {
            for (int i = 0; i < matchers.Length; i++) {
                var matcher = matchers[i] as Matcher<TEntity>;
                if (matcher != null && matcher.componentNames != null) {
                    return matcher.componentNames;
                }
            }

            return null;
        }

        static void setComponentNames(Matcher<TEntity> matcher, IMatcher<TEntity>[] matchers) {
            var componentNames = getComponentNames(matchers);
            if (componentNames != null) {
                matcher.componentNames = componentNames;
            }
        }

        static int[] distinctIndices(IEnumerable<int> indices) {
            var indicesSet = EntitasCache.reusableIntHashSet;
            foreach(var index in indices) {
                indicesSet.Add(index);
            }
            var uniqueIndices = new int[indicesSet.Count];
            indicesSet.CopyTo(uniqueIndices);
            Array.Sort(uniqueIndices);
            return uniqueIndices;
        }
    }

    public class MatcherException<TEntity> : Exception where TEntity : class, IEntity, new() {
        public MatcherException(IMatcher<TEntity> matcher) :
            base("matcher.indices.Length must be 1 but was " + matcher.indices.Length) {
        }
    }
}

