using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using HyzenApi.DTO;
using HyzenApi.Infrastructure;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

namespace HyzenApi.Models;

[Table("base_table")]
public class BaseModel
{
    private BaseModel() { }
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id", TypeName = "INT")] 
    public int Id { get; set; }
    
    [Column("name", TypeName = "VARCHAR(255)"), MaxLength(255), Required] 
    public string? Name { get; set; }
    
    [Column("updated_at", TypeName = "DATETIME"), DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
    public DateTime? UpdatedAt { get; set; }
    
    [Column("created_at", TypeName = "DATETIME"), DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
    public DateTime? CreatedAt { get; set; }
    
    
    public static async Task<BaseModel> CreateAsync(BaseDto @base)
    {
        var baseCreate = new BaseModel
        {
            Name = @base.Name,
        };
        
        await DatabaseContext.Get().BaseSet.AddAsync(baseCreate);
        return baseCreate;
    }
    
    public static async Task<BaseModel?> GetAsync(int id)
    {
        return await DatabaseContext.Get().BaseSet.FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public static async Task DeleteAsync(int id)
    {
        var result = await DatabaseContext.Get().BaseSet.FirstOrDefaultAsync(s => s.Id == id);
        
        if (result == null)
            throw new InvalidOperationException("ID não encontrado.");

        DatabaseContext.Get().BaseSet.Remove(result);
    }
}