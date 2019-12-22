using System;
using UnityEngine;

namespace TEDinc.Utils
{ 
    public class TypeConstraintAttribute : PropertyAttribute
    {
        public Type type { get; private set; }

        public TypeConstraintAttribute(Type type)
        {
            this.type = type;
        }
    }
}