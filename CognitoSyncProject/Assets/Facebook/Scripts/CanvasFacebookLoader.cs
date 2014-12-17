using UnityEngine;
using System.Collections;

namespace Facebook
{
    public class CanvasFacebookLoader : FB.RemoteFacebookLoader
    {
        protected override string className
        {
            get { return "CanvasFacebook"; }
        }
    }
}