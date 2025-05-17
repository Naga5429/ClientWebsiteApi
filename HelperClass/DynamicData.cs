using System.Dynamic;

namespace ClientWebsiteAPI.HelperClass
{

    public class DynamicData : DynamicObject
    {
        public IDictionary<string, object> Dictionary { get; set; }

        public DynamicData()
        {
            this.Dictionary = new Dictionary<string, object>();
        }

     //   public int Count { get { return this.Dictionary.Keys.Count; } }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this.Dictionary.ContainsKey(binder.Name))
            {
                result = this.Dictionary[binder.Name];
                return true;
            }
            return base.TryGetMember(binder, out result); //means result = null and return = false
        }

        public void SetMember(string name, object value)
        {
            if (!this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Add(name, value);
            }
            else
                this.Dictionary[name] = value;
        }

        public object GetMember(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                return this.Dictionary[name];
            }
            else
                return null;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!this.Dictionary.ContainsKey(binder.Name))
            {
                this.Dictionary.Add(binder.Name, value);
            }
            else
                this.Dictionary[binder.Name] = value;

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (this.Dictionary.ContainsKey(binder.Name) && this.Dictionary[binder.Name] is Delegate)
            {
                Delegate del = this.Dictionary[binder.Name] as Delegate;
                result = del.DynamicInvoke(args);
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (this.Dictionary.ContainsKey(binder.Name))
            {
                this.Dictionary.Remove(binder.Name);
                return true;
            }

            return base.TryDeleteMember(binder);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (string name in this.Dictionary.Keys)
                yield return name;
        }

        public bool isMemberExists(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                return true;
            }
            else
                return false;
        }

    }

}

