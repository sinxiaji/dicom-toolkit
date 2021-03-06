﻿using System.Collections.Generic;
using System.Windows;

namespace SimpleDICOMToolkit.Helpers
{
    public static class VisualTreeHelper
    {
        public static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                if (System.Windows.Media.VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);

                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 利用visualtreehelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualChild<T>(DependencyObject obj) where T : FrameworkElement
        {
            int count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj);

            List<T> TList = new List<T>();
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    TList.Add((T)child);
                }
                else
                {
                    List<T> childOfChildren = FindVisualChild<T>(child);
                    if (childOfChildren != null)
                    {
                        TList.AddRange(childOfChildren);
                    }
                }
            }

            return TList;
        }

        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualParent<T>(DependencyObject obj) where T : FrameworkElement
        {
            List<T> TList = new List<T> { };

            DependencyObject parent = System.Windows.Media.VisualTreeHelper.GetParent(obj);
            if (parent != null && parent is T)
            {
                TList.Add((T)parent);
                List<T> parentOfParent = FindVisualParent<T>(parent);
                if (parentOfParent != null)
                {
                    TList.AddRange(parentOfParent);
                }
            }

            return TList;
        }
    }
}
