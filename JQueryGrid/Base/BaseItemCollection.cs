using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;
using System.Web.UI;
using System.Collections;

namespace Trirand.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public abstract class BaseItemCollection<OwnerType, ItemType> : StateManagedCollection
    {
        private OwnerType _owner;
        private static readonly Type[] _knownTypes = new Type[] { typeof(ItemType) };

        public BaseItemCollection()
        {
        }

        public BaseItemCollection(OwnerType owner)
        {
            _owner = owner;
        }

        public int Add(ItemType item)
        {
            return ((IList)this).Add(item);
        }

        public bool Contains(ItemType item)
        {
            return ((IList)this).Contains(item);
        }

        public void CopyTo(ItemType[] array, int index)
        {
            ((IList)this).CopyTo(array, index);
        }

        protected override abstract object CreateKnownType(int index);


        protected override Type[] GetKnownTypes()
        {
            return _knownTypes;
        }

        public int IndexOf(ItemType value)
        {
            return ((IList)this).IndexOf(value);
        }

        public void Insert(int index, ItemType item)
        {
            ((IList)this).Insert(index, item);
        }

        protected override void OnClear()
        {
            base.OnClear();
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
        }

        public void Remove(ItemType item)
        {
            this.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        protected override void SetDirtyObject(object o)
        {
            if (o is BaseItem)
            {
                ((BaseItem)o).SetDirty();
            }
        }

        public ItemType this[int i]
        {
            get
            {
                return (ItemType)((IList)this)[i];
            }
            set
            {
                this[i] = value;
            }
        }

        int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }

      


    }
}
