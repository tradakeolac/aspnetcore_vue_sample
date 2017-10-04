namespace Saleman.Data.Specifications
{
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionSpecificationBase<T> : CompositeSpecification<T>
    {
        public abstract Expression<Func<T, bool>> Expression { get; }

        public override bool IsSatisfiedBy(T candidate)
        {
            return this.Expression.Compile().Invoke(candidate);
        }
    }
}