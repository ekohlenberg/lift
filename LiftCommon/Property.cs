using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiftCommon
{
    public delegate void PropertyChangeDelegate( string name, Type type, object value );

    public class PropertyBase
    {
        public event PropertyChangeDelegate OnChange;
        protected ModelObject owner;

        protected void fireOnChange(object value)
        {
            if (OnChange != null)
            {
                foreach (PropertyChangeDelegate pcd in OnChange.GetInvocationList())
                {
                    pcd(Name, InternalType, value);
                }
            }
        }

        public virtual string Name
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        protected virtual Type InternalType
        {
            get
            {
                return this.GetType();
            }
        }

        

        public virtual ModelObject Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }


    }

    public class Property< T > : PropertyBase
    {
        public string name;

        public Property()
        {
           
        }

        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        protected override Type InternalType
        {
            get
            {
                return typeof(T);
            }
        }

        public virtual T Value
        {
            set
            {
                if (owner != null)
                {
                    owner[Name] = value;
                }
                    fireOnChange(value);
            }
            get
            {
                return (T) owner.safeConvert( Name, typeof(T));
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class IntProperty : Property<int>
    {
        public static implicit operator int(IntProperty p)
        {
            return p.Value;
        }
    }

    public class StringProperty : Property<string>
    {
        public static implicit operator string(StringProperty p)
        {
            return p.Value;
        }
    }

    public class DateTimeProperty : Property<System.DateTime>
    {
        public static implicit operator DateTime(DateTimeProperty p)
        {
            return p.Value;
        }
    }

    public class FloatProperty : Property<float>
    {
        public static implicit operator float(FloatProperty p)
        {
            return p.Value;
        }
    }

    public class BoolProperty : Property<bool>
    {
        public static implicit operator bool(BoolProperty p)
        {
            return p.Value;
        }
    }


}
