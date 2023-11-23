﻿using FluentValidation;
using FluentValidation.Results;



namespace Biblioteca.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity() 
        {
            ValidationResult = new ValidationResult();
        }

        public long Id { get; protected set; }
        public virtual bool Excluido { get; set; }
        public virtual DateTimeOffset? ExclusaoData { get; set; }

        public void Excluir()
        {
            Excluido = true;
            ExclusaoData = DateTimeOffset.Now;
        }

        public ValidationResult ValidationResult { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if(ReferenceEquals(this, compareTo)) return true;
            if(compareTo is null) return false;
            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if(a is null && b is null) return true;
            if (a is null || b is null) return false; 
            return a.Equals(b);
        }
        public static bool operator !=(Entity<T> a, Entity<T> b) { return !(a == b); }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]" ;
        }
    }
}
