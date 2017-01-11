using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace BeHappy
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumTitleAttribute: Attribute
    {
        private string text;
        private object tag;

        public string Text
        {
            get { return this.text; }
        }
        public object Tag
        {
            get { return this.tag; }
        }
        public override string ToString()
        {
            return Text;
        }
        public EnumTitleAttribute(string text, object tag)
        {
            this.text = text;
            this.tag = tag;
        }

        public EnumTitleAttribute(string text):this(text, null)
        {
        }

        
    }
    
    class EnumProxy
    {
        private object realValue;
        private static readonly Dictionary<object, EnumProxy> cache = new Dictionary<object, EnumProxy>();
        private static readonly object lockObject = new object();
        private EnumTitleAttribute attribute;
        
        public static EnumProxy Create(object v)
        {
            lock (lockObject)
            {
                if (cache.ContainsKey(v))
                    return cache[v];
                else
                {
                    EnumProxy p = new EnumProxy(v);
                    cache.Add(v, p);
                    return p;
                }
            }
        }
        
        private EnumProxy(object v)
        {
            this.realValue = v;
            System.Type t = v.GetType();
            FieldInfo fi = t.GetField(v.ToString());
            object[] attr = fi.GetCustomAttributes(typeof(EnumTitleAttribute), false);
            if (null == attr || attr.Length == 0)
                this.attribute = new EnumTitleAttribute(v.ToString());
            else
                this.attribute = attr[0] as EnumTitleAttribute;
        }

        public override string ToString()
        {
            return this.attribute.Text;
        }
        
        public object RealValue
        {
            get
            {
                return this.realValue;
            }
        }

        public object Tag
        {
            get
            {
                return this.attribute.Tag;
            }
        }
       
        
        public static EnumProxy[] CreateArray(System.Collections.IList list)
        {
            EnumProxy[] arr = new EnumProxy[list.Count];
            for(int i=0; i<arr.Length; i++)
            {
                arr[i] = Create(list[i]);
            }
            return arr;
        }

        public static EnumProxy[] CreateArray(System.Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            if (!enumType.IsEnum)
                throw new ArgumentException("enumType must be Enum", "enumType");
            FieldInfo[] enumItems = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            EnumProxy[] arr = new EnumProxy[enumItems.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Create( enumItems[i].GetValue(null) );
            }
            return arr;
        }
        
        public static int IndexOf(System.Collections.IList values, object valueToFind)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if ((int)valueToFind == (int)(values[i] as EnumProxy).RealValue)
                    return i;
            }
            return -1;            
        }
        
        //static void Main(string[] arg)
        //{
        //    object[] a = EnumProxy.CreateArray(new object[] { MyTestEnum.x, MyTestEnum.y, MyTestEnum.z });
        //    foreach (object o in a)
        //        Console.WriteLine(o.ToString());
        //}
    }
    
    //public enum MyTestEnum
    //{
    //    [EnumTitle("Title 1")]
    //    x,
    //    [EnumTitle("Title 2 - dimzon is the best")]
    //    y,
    //    z
    //}
}
