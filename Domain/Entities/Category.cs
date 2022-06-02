using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        public Category(Guid id,string name)
        {
            this.Id = id;
            this.Name= name;
        }      
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Category UpdateCategoryName(string name)
        {
            return new Category(this.Id, name ?? this.Name);
        }
    }
}
