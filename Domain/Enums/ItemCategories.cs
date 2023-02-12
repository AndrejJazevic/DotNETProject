using System;

namespace Domain.Enums
{
    [Flags]
    public enum ItemCategories
    {
        None = 0,
        Clothing = 1,
        Electronics = 2,
        Food = 4,
        Furniture = 8
    }
}