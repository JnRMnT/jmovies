using JMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JMovies.Utilities.Unity
{
    public class SingletonUnity
    {
        private static IUnityContainer activeContainer;
        public static IUnityContainer ActiveContainer
        {
            get
            {
                if(activeContainer == null)
                {
                    activeContainer = new UnityContainer();
                }
                return activeContainer;
            }
            set
            {
                activeContainer = value;
            }
        }

        public static T Resolve<T>()
        {
            return ActiveContainer.Resolve<T>();
        }
    }
}
