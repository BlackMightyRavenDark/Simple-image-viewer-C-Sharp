using System.Collections.Generic;

namespace Simple_image_viewer_C_Sharp
{
    public class ImageFileList
    {
        private readonly List<string> FileList = new List<string>();
        private int _positionIndex = -1;
        public int Position { get { return _positionIndex; } set { SetPosition(value); } }
        public int Count => FileList.Count;
        public string this[int number]
        {
            get
            {
                return FileList[number];
            }
        }

        public delegate void ItemAddedDelegate(object sender, string itemString);
        public delegate void ItemRemovedDelegate(object sender, int index, string itemString);
        public delegate void PositionIndexChangedDelegate(object sender, int position);
        public delegate void ClearedDelegate(object sender);
        public ItemAddedDelegate ItemAdded;
        public ItemRemovedDelegate ItemRemoved;
        public PositionIndexChangedDelegate PositionIndexChanged;
        public ClearedDelegate Cleared;

        public void Add(string item)
        {
            FileList.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void AddRange(IEnumerable<string> items)
        {
            foreach (string str in items)
            {
                Add(str);
            }
        }

        public void RemovveAt(int itemIndex)
        {
            string str = FileList[itemIndex];
            FileList.RemoveAt(itemIndex);
            ItemRemoved?.Invoke(this, itemIndex, str);
        }

        public void Clear()
        {
            FileList.Clear();
            _positionIndex = -1;
            Cleared?.Invoke(this);
        }

        private void SetPosition(int position)
        {
            if (_positionIndex != position)
            {
                _positionIndex = position;
                PositionIndexChanged?.Invoke(this, position);
            }
        }
    }
}
