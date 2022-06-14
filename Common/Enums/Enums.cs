using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    /// <summary>
    /// მომხმარებლის სტატუსის ვარიანტები
    /// </summary>
    public enum UserStatus
    {
        Active = 0,
        Block = 1,
        Delete = 2
    }
    /// <summary>
    /// მომხმაებლის უფლებები
    /// </summary>
    [Flags]
    public enum Permission
    {
        None = 0
    }
    /// <summary>
    /// მომხმარებლის როლები
    /// </summary>
    public enum UserRole
    {
        Parrent = 0,
        Child = 1
    }
}
