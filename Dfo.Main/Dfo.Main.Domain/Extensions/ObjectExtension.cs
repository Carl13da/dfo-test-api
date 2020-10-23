using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dfo.Main.Domain.Extensions
{
    public static class ObjectExtension
    {
        public static object MergeToDestination<TSource>(
            this TSource sourceObject,
            object destinationObject,
            params string[] ignoreProps)
        {
            return _MergeToDestination(sourceObject, destinationObject, ignoreProps);
        }

        public static TDestination MergeToDestination<TSource, TDestination>(
            this TSource sourceObject,
            TDestination destinationObject,
            params string[] ignoreProps)
        {
            return (TDestination)_MergeToDestination(sourceObject, destinationObject, ignoreProps);
        }

        public static TDestination MergeToDestination<TDestination>(
            this object sourceObject,
            params string[] ignoreProps)
        {
            var newDestObject = Activator.CreateInstance<TDestination>();

            return sourceObject.MergeToDestination(newDestObject, ignoreProps);
        }

        private static object _MergeToDestination<TSource>(
            TSource sourceObject,
            object destinationObject,
            params string[] ignoreProps)
        {
            var destObjType = destinationObject.GetType();

#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (sourceObject == null) return null;
#pragma warning restore RECS0017 // Possible compare of value type with 'null'

            if (destObjType.IsGenericType &&
                destObjType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var reflectedList = (IList)sourceObject;

                var newList = Activator.CreateInstance(destObjType);

                if (reflectedList != null)
                {
                    foreach (var item in reflectedList)
                    {
                        var newObj = Activator.CreateInstance(destObjType.GetGenericArguments()[0]);
                        var convertedItem = item.MergeToDestination(newObj, ignoreProps);

                        destObjType
                            .GetMethod("Add")
                            .Invoke(newList, new[] { convertedItem });
                    }
                }

                return newList;
            }

            var destProps = destinationObject.GetType().GetProperties();
            var sourceProps = sourceObject.GetType().GetProperties();

            if (!sourceProps.Any())
            {
                return null;
            }

            foreach (var destProp in destProps)
            {
                if (!destProp.CanWrite) continue;

                var sourceProp = sourceProps.FirstOrDefault(t => t.Name == destProp.Name &&
                    !ignoreProps.Contains(destProp.Name));

                if (sourceProp == null) continue;

                var destPropType = destProp.PropertyType;

                if (destPropType.IsGenericType && destPropType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var reflectedList = (IList)sourceProp.GetValue(sourceObject);

                    var newList = Activator.CreateInstance(destPropType);

                    if (reflectedList != null)
                    {
                        foreach (var item in reflectedList)
                        {
                            var t = destPropType.GetGenericArguments()[0];

                            if (t.IsPrimitive || t == typeof(decimal) || t == typeof(string))
                            {
                                destPropType
                                    .GetMethod("Add")
                                    .Invoke(newList, new[] { item });
                            }
                            else
                            {
                                var newObj = Activator.CreateInstance(destPropType.GetGenericArguments()[0]);
                                var convertedItem = item.MergeToDestination(newObj, ignoreProps);

                                destPropType
                                    .GetMethod("Add")
                                    .Invoke(newList, new[] { convertedItem });
                            }
                        }
                    }
                    else
                    {
                        newList = null;
                    }

                    destProp.SetValue(
                        destinationObject,
                        newList
                    );

                    continue;
                }

                if (sourceProp.PropertyType != destProp.PropertyType)
                {
                    if (destProp.PropertyType == typeof(bool))
                    {
                        destProp.SetValue(
                            destinationObject,
                            Convert.ToBoolean(sourceProp.GetValue(sourceObject))
                        );

                        continue;
                    }

                    if (destProp.PropertyType == typeof(int))
                    {
                        destProp.SetValue(
                            destinationObject,
                            Convert.ToInt32(sourceProp.GetValue(sourceObject))
                        );

                        continue;
                    }

                    if (destProp.PropertyType == typeof(string) &&
                        sourceProp.PropertyType == typeof(Guid))
                    {
                        destProp.SetValue(
                            destinationObject,
                            Convert.ToString(sourceProp.GetValue(sourceObject))
                        );

                        continue;
                    }

                    if (sourceProp.GetValue(sourceObject) == null)
                        continue;

                    var newObjClass = Activator.CreateInstance(destPropType);

                    var newConvertedClass = sourceProp.GetValue(sourceObject).MergeToDestination(newObjClass);

                    destProp.SetValue(
                        destinationObject,
                        newConvertedClass
                    );

                    continue;
                }

                destProp.SetValue(
                    destinationObject,
                    sourceProp.GetValue(sourceObject)
                );
            }

            return destinationObject;
        }
    }
}