using System;
using System.Reflection;
using System.Collections.Generic;

namespace uRedux {
  public static class ObjectExtensions {
    private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

    ///<summary>
    ///Deep clone the object to another object.
    ///</summary>
    public static Object Copy(this Object obj) {
      return InternalCopy(obj, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
    }

    public static bool IsPrimitive(this Type type) {
      if (type == typeof(String)) return true;
      return (type.IsValueType & type.IsPrimitive);
    }

    private static Object InternalCopy(Object obj, IDictionary<Object, Object> visited) {

      // null
      if (obj == null)
        return null;

      // primitive
      var type = obj.GetType();
      if (IsPrimitive(type))
        return obj;

      // the object is already cloned, dont bother cloning again
      // also to avoid loops
      if (visited.ContainsKey(obj))
        return visited[obj];

      // method - skip copying
      if (typeof(Delegate).IsAssignableFrom(type))
        return null;

      // start shallow cloning the object with Object.MemberwiseClone
      var cloned = CloneMethod.Invoke(obj, null);

      // array - manually copy array items
      if (type.IsArray) {
        var arrayType = type.GetElementType();
        if (!IsPrimitive(arrayType)) {
          Array clonedArray = (Array)cloned;
          for (var i = 0; i < clonedArray.Length; ++i) {
            clonedArray.SetValue(InternalCopy(clonedArray.GetValue(i), visited), i);
          }
        }
      }
      visited.Add(obj, cloned);

      // object - manually copy fields
      CopyFields(obj, visited, cloned, type);

      // deep copying
      RecursiveCopyBaseTypePrivateFields(obj, visited, cloned, type);

      return cloned;
    }

    private static void RecursiveCopyBaseTypePrivateFields(object obj, IDictionary<object, object> visited, object cloned, Type type) {
      if (type.BaseType != null) {
        RecursiveCopyBaseTypePrivateFields(obj, visited, cloned, type.BaseType);
        CopyFields(obj, visited, cloned, type.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
      }
    }

    private static void CopyFields(object obj, IDictionary<object, object> visited, object cloned, Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null) {
      foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags)) {
        if (filter != null && filter(fieldInfo) == false) continue;
        if (IsPrimitive(fieldInfo.FieldType)) continue;
        var originalFieldValue = fieldInfo.GetValue(obj);
        var clonedFieldValue = InternalCopy(originalFieldValue, visited);
        fieldInfo.SetValue(cloned, clonedFieldValue);
      }
    }

    public static T Copy<T>(this T obj) {
      return (T)Copy((Object)obj);
    }
  }

  public class ReferenceEqualityComparer : EqualityComparer<Object> {
    public override bool Equals(object x, object y) {
      return ReferenceEquals(x, y);
    }
    public override int GetHashCode(object obj) {
      if (obj == null) return 0;
      return obj.GetHashCode();
    }
  }

}