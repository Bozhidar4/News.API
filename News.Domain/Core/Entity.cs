﻿namespace News.Domain.Core
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}
