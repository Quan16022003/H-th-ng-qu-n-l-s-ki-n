using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Ultils
{
    public class ReferenceCollection<T> : ICollection<Expression<Func<T, object>>> where T : class
    {
        private readonly List<Expression<Func<T, object>>> expressionIncludes = new ();

        public int Count => expressionIncludes.Count;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public bool IsReadOnly => false;

        public ReferenceCollection()
        {

        }

        public void Add(Expression<Func<T, object>> item)
        {
            expressionIncludes.Add(item);
        }

        public void AddMultiple(IEnumerable<Expression<Func<T, object>>> items)
        {
            expressionIncludes.AddRange(items);
        }

        void ICollection<Expression<Func<T, object>>>.Clear()
        {
            expressionIncludes.Clear();
        }

        public bool Contains(Expression<Func<T, object>> item)
        {
            return expressionIncludes.Contains(item);
        }

        public void CopyTo(Expression<Func<T, object>>[] array, int index)
        {
            foreach (var item in expressionIncludes)
            {
                array.SetValue(item, index);
                index++;
            }
        }

        public bool Remove(Expression<Func<T, object>> item)
        {
            return expressionIncludes.Remove(item);
        }

        public void RemoveAt(int index)
        {
            expressionIncludes.RemoveAt(index);
        }

        public IEnumerator<Expression<Func<T, object>>> GetEnumerator()
        {
            return expressionIncludes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return expressionIncludes.GetEnumerator();
        }
    }
}
