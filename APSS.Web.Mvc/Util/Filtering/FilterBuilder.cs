using System.Linq.Expressions;

using APSS.Domain.Entities;
using APSS.Domain.Repositories;

namespace APSS.Web.Mvc.Util.Filtering;

public class FilterBuilder<TArgs, TModel> where TModel : AuditableEntity
{
    private readonly TArgs _args;
    private IQueryBuilder<TModel> _query;

    public FilterBuilder(TArgs args, IQueryBuilder<TModel> query)
    {
        _args = args;
        _query = query;
    }

    public FilterBuilder<TArgs, TModel> Filter(Func<TArgs, Expression<Func<TModel, bool>>> expr)
    {
        _query = _query.Where(expr(_args));

        return this;
    }

    public IQueryBuilder<TModel> Build() => _query;
}