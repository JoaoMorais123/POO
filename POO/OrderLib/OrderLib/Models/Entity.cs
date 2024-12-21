using System;

namespace OrderLib.Models
{
    /// <summary>
    /// Base class representing a general entity with common properties.
    /// </summary>
    public abstract class Entity
    {
        public int ID { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public abstract string DisplayInfo();

        protected Entity(int id)
        {
            ID = id;
            CreatedDate = DateTime.Now;
        }
    }
}