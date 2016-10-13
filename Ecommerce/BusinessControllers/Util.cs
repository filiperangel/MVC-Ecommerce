using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.BusinessControllers
{
    public class Util
    {
        public enum OrderStatus { Open = 1, Shipped = 2, Delivered = 3, Closed = 4, Canceled = 5 };
    }
}