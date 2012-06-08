using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public abstract class BasicPrimitive : Basic
    {
        #region Implemented from Basic

        protected override BoundingSphere GetBoundingSphere()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
