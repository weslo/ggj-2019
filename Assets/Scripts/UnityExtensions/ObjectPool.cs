using System;
using System.Collections.Generic;
using System.Linq;
using Game.CSharpExtensions;

namespace Game.UnityExtensions
{
    public sealed class ObjectPool<ObjectType, DataType>
        where ObjectType : UnityEngine.MonoBehaviour
    {
        public int Count
            => spawned.Count;

        private ObjectType objectPrefab;

        private Action<ObjectType, DataType, int> applyAction;

        private Stack<ObjectType> spawned
            = new Stack<ObjectType>();

        public ObjectPool(
            ObjectType prefab,
            Action<ObjectType, DataType, int> apply = null)
        {
            objectPrefab = prefab;
            applyAction = apply;
        }

        public void SetData(IEnumerable<DataType> collection)
        {
            int incomingCount = collection.Count();

            while(spawned.Count > incomingCount)
            {
                ObjectType obj = spawned.Pop();
                if(obj != null)
                {
                    UnityEngine.Object.Destroy(obj.gameObject);
                }
            }

            while(spawned.Count < incomingCount)
            {
                spawned.Push(UnityEngine.Object.Instantiate(objectPrefab));
            }

            if(applyAction != null)
            {
                Functions.Repeat(incomingCount, i =>
                {
                    ObjectType obj = spawned.ElementAt(i);
                    DataType data = collection.ElementAt(i);
                    applyAction(obj, data, i);
                });
            }
        }
    }
}