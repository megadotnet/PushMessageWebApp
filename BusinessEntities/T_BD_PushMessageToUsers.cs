using System;

namespace BusinessEntities
{

    public partial class T_BD_PushMessageToUsers 
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public Nullable<int> Userid { get; set; }
        public bool IsRead { get; set; }
        public System.DateTime SendingTime { get; set; }


        public virtual T_BD_PushMessage T_BD_PushMessage { get; set; }
    }
}