namespace Saleman.Data.Specifications
{
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionQueryBaseSpecification<T> : ExpressionSpecificationBase<T>
    {

        public ExpressionQueryBaseSpecification() : base()
        {

        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return this.Expression;
        }
    }
}