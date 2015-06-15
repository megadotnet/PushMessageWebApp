namespace WebAuth.Models
{
    using System.Collections.Generic;

    public class PermissionViewModelComparer : IComparer<PermissionViewModel>
    {
        public int Compare(PermissionViewModel x, PermissionViewModel y)
        {
            //id相同，则相等
            if (string.Compare(x.Id, y.Id, true) == 0)
            {
                return 0;
            }
            //controller比较
            var controllerCompareResult = string.Compare(x.Controller, y.Controller, true);
            //action比较
            var actionCompareResult = string.Compare(x.Action, y.Action, true);
            //先比较controller,后比较action
            if (controllerCompareResult != 0)
            {
                return controllerCompareResult;
            }
            else
            {
                return actionCompareResult;
            }
        }
    }
}