using System;
using System.Collections.Generic;
using System.Linq;

namespace TSC.AvaloniaUI;

public static class Extensions {
    public static bool StrictAll<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
        if (!source.Any()) return false;

        return source.Count() != 0 && source.All(predicate);
    }
    
    public static bool StrictAll<T>(this IList<T> source, Func<T, bool> predicate) {
        if (!source.Any()) return false;

        return source.Count != 0 && source.All(predicate);
    }
    
    public static bool StrictAll<T>(this List<T> source, Func<T, bool> predicate) {
        if (source.Count == 0) return false;

        return source.Count != 0 && source.All(predicate);
    }
}