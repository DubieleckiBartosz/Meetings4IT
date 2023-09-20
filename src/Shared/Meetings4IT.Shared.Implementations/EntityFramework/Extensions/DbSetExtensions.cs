﻿using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;

namespace Meetings4IT.Shared.Implementations.EntityFramework.Extensions;

public static class DbSetExtensions
{
    public static IQueryable<TEntity> IncludePaths<TEntity>(this IQueryable<TEntity> source,
            params string[] navigationPaths) where TEntity : class
    {
        if (!(source.Provider is EntityQueryProvider))
        {
            return source;
        }

        return source.Include(string.Join(".", navigationPaths));
    }
}