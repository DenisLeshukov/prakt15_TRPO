using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prakt15_Leshukov_TRPO.Models;

public partial class Category:ObservableObject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int _id ;
    public int Id { get => _id; set => SetProperty(ref _id, value); }

    private string _name = null!;
    public string Name { get => _name; set => SetProperty(ref _name, value); }

    public virtual ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();
}
