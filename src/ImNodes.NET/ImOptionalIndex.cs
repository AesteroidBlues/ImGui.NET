using System;
namespace imnodesNET
{
    public unsafe struct ImOptionalIndex
    {
        const int INVALID_INDEX = -1;

        private int _Index;

        public ImOptionalIndex(int index)
        {
            _Index = index;
        }

        // Observers
        bool HasValue() { return _Index != INVALID_INDEX; }

        int Value()
        {
            return _Index;
        }

        public void Reset() { _Index = INVALID_INDEX; }

        public static bool operator ==(ImOptionalIndex lhs, ImOptionalIndex rhs) { return lhs._Index == rhs._Index; }

        public static bool operator ==(ImOptionalIndex lhs, int rhs) { return lhs._Index == rhs; }

        public static bool operator !=(ImOptionalIndex lhs, ImOptionalIndex rhs) { return lhs._Index != rhs._Index; }

        public static bool operator !=(ImOptionalIndex lhs, int rhs) { return lhs._Index != rhs; }
    }
}
