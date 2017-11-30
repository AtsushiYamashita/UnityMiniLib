using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class LinqUtil
{
    static string ToStr<TSource>(this IEnumerable<TSource> source)
    {
        string[] strs = source as string[];
        if (strs != null) { return ""; }
        return "{" + string.Join(", ", strs) + "}";
    }

    static string ToStr<TKey, TSource>(this IEnumerable<IGrouping<TKey, TSource>> source)
    {
        return source.Select((group) =>
        {
            return string.Format("Key={0}, Source={1}", group.Key, group.ToStr());
        }).ToStr();
    }

    public static void ForEach<T>(this IEnumerable<T> src, Action<T, int> func)
    {
        var selected = src.Select((x, i) => new { Value = x, Index = i });
        foreach (var item in selected) { func(item.Value, item.Index); }
    }
    public static void ForEach<T>(this IEnumerable<T> src, Action<T> func)
    {
        foreach (var item in src) { func(item); }
    }
}
